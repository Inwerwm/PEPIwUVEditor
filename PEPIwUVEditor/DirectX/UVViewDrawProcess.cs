using DxManager;
using DxManager.Camera;
using IwUVEditor.Command;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.Manager;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.RawInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SelectionMode = IwUVEditor.Command.SelectionMode;

namespace IwUVEditor.DirectX
{
    class UVViewDrawProcess : DxProcess
    {
        private Color4 colorInDefault;
        private Color4 colorInSelected;

        public InputManager Current { get; }

        RasterizerStateProvider Rasterize { get; set; }

        private Matrix TransMatrix => Camera.GetMatrix() * Matrix.Translation(ShiftOffset) * Matrix.Scaling(Scale.Scale, Scale.Scale, 1);
        private Matrix InvertTransMatrix
        {
            get
            {
                Matrix iViewMat = Matrix.Invert(Camera.CreateViewMatrix());
                Matrix iPrjMat = Matrix.Invert((Camera as DxCameraOrthographic).CreateProjectionMatrix());
                Matrix iOfstMat = Matrix.Invert(Matrix.Translation(ShiftOffset));
                Matrix iSclMat = Matrix.Invert(Matrix.Scaling(Scale.Scale, Scale.Scale, 1));
                return iSclMat * iOfstMat * iPrjMat * iViewMat;
            }
        }

        internal Vector3 ShiftOffset { get; set; }
        internal ScaleManager Scale { get; } = new ScaleManager()
        {
            DeltaOffset = -10000,
            Amplitude = 200000,
            Offset = 50f,
            Step = 0.1f,
            Gain = 1,
            LowerLimit = -10000,
            UpperLimit = 12000,
        };

        TexturePlate TexturePlate { get; set; }
        SelectionRectangle SelectionRectangle { get; set; }

        Dictionary<Material, ShaderResourceView> TextureCache { get; } = new Dictionary<Material, ShaderResourceView>();
        ShaderResourceView CurrentTexture { get; set; }

        Dictionary<Material, UVMesh> UVMeshCache { get; } = new Dictionary<Material, UVMesh>();
        UVMesh CurrentUVMesh { get; set; }

        Dictionary<Material, PositionSquares> PositionSquareCache { get; } = new Dictionary<Material, PositionSquares>();
        PositionSquares CurrentPositionSquares;

        public Color4 ColorInDefault
        {
            get => colorInDefault;
            set
            {
                colorInDefault = value;
                foreach (var mesh in UVMeshCache.Values)
                {
                    mesh.LineColor = value;
                }
                foreach (var sq in PositionSquareCache.Values)
                {
                    sq.ColorInDefault = value;
                }
            }
        }

        public Color4 ColorInSelected
        {
            get => colorInSelected;
            set
            {
                colorInSelected = value;
                foreach (var sq in PositionSquareCache.Values)
                {
                    sq.ColorInSelected = value;
                }
            }
        }

        public UVViewDrawProcess(InputManager inputManager)
        {
            Current = inputManager;

            Current.MaterialIsChanged += (value) =>
            {
                Material material = value as Material;

                if (Context is null)
                    return;
                if (!TextureCache.Keys.Contains(material))
                {
                    TextureCache.Add(material, LoadTexture(material));
                    UVMeshCache.Add(material, new UVMesh(Context.Device, Effect, Rasterize.Wireframe, material, ColorInDefault));
                    PositionSquareCache.Add(material, new PositionSquares(Context.Device, Effect, Rasterize.Solid, material, Current.RadiusOfPositionSquare, ColorInDefault, colorInSelected));
                }

                CurrentTexture = TextureCache[material];
                CurrentUVMesh = UVMeshCache[material];
                CurrentPositionSquares = PositionSquareCache[material];
            };

            Current.RadiusOfPosSqIsChanged += (value) =>
            {
                float radius = (float)value;
                foreach (var sq in PositionSquareCache.Values)
                {
                    sq.Radius = radius;
                }
            };
        }

        public override void Init()
        {
            CurrentTexture = LoadTexture(null);

            Rasterize = new RasterizerStateProvider(Context.Device) { CullMode = CullMode.None };

            TexturePlate = new TexturePlate(Context.Device, Effect, Rasterize.Solid) { InstanceParams = (10, 0.5f) };
            SelectionRectangle = new SelectionRectangle(Context.Device, Effect, Rasterize.Solid, new Color4(0.5f, 1, 1, 1));
        }

        public override void Draw()
        {
            Current.FPS = CurrentFPS;

            // 背景を灰色に
            Context.Device.ImmediateContext.ClearRenderTargetView(Context.RenderTarget, new Color4(1.0f, 0.3f, 0.3f, 0.3f));
            // 深度バッファ
            Context.Device.ImmediateContext.ClearDepthStencilView(Context.DepthStencil, DepthStencilClearFlags.Depth, 1, 0);
            // テクスチャを読み込み
            Effect.GetVariableByName("diffuseTexture").AsResource().SetResource(CurrentTexture);

            // テクスチャ板を描画
            TexturePlate.Prepare();

            // メッシュを描画
            CurrentUVMesh?.Prepare();

            // 頂点位置に四角を描画
            CurrentPositionSquares?.Prepare();

            // ツール固有の描画処理を実行
            switch (Current.Tool)
            {
                case Tool.RectangleSelection:
                    if(Current.MouseLeft.IsDragging)
                        SelectionRectangle.Prepare();
                    break;
                default:
                    break;
            }

            // 描画内容を反映
            Context.SwapChain.Present(0, PresentFlags.None);
        }

        protected override void UpdateCamera()
        {
            Effect.GetVariableByName("ViewProjection").AsMatrix().SetMatrix(TransMatrix);
        }

        public void ResetCamera()
        {
            ShiftOffset = Vector3.Zero;
            Scale.WheelDelta = 0;
        }


        public void ChangeResolution()
        {
            Current.ScreenSize = new Vector2(Context.TargetControl.ClientSize.Width, Context.TargetControl.ClientSize.Height);

            (Camera as DxCameraOrthographic).ViewVolumeSize = (Current.ScreenSize.X, Current.ScreenSize.Y);

            foreach (var ps in PositionSquareCache.Values)
            {
                ps.ScreenSize = Current.ScreenSize;
            }
        }

        public Vector2 ScreenPosToWorldPos(Vector2 screenPos)
        {
            Vector2 normalizedPos = new Vector2(2 * screenPos.X / Context.TargetControl.Width - 1, 1 - 2 * screenPos.Y / Context.TargetControl.Height);
            Vector4 worldPos = Vector4.Transform(new Vector4(normalizedPos, 0, 1), InvertTransMatrix);
            return new Vector2(worldPos.X, worldPos.Y);
        }

        private Texture2D TextureFromBitmap(Bitmap bitmap)
        {
            using (LockedBitmap lockedBitmap = new LockedBitmap(bitmap))
            using (DataStream dataStream = new DataStream(lockedBitmap.BitmapData.Scan0, Math.Abs(lockedBitmap.BitmapData.Stride) * lockedBitmap.BitmapData.Height, true, false))
                return new Texture2D(
                    Context.Device,
                    new Texture2DDescription
                    {
                        BindFlags = BindFlags.ShaderResource,
                        CpuAccessFlags = CpuAccessFlags.None,
                        Format = Format.B8G8R8A8_UNorm,
                        OptionFlags = ResourceOptionFlags.None,
                        MipLevels = 1,
                        Usage = ResourceUsage.Immutable,
                        Width = bitmap.Width,
                        Height = bitmap.Height,
                        ArraySize = 1,
                        SampleDescription = new SampleDescription(1, 0)
                    },
                    new DataRectangle(lockedBitmap.BitmapData.Stride, dataStream)
                );
        }

        private ShaderResourceView LoadTexture(Material material)
        {
            if ((material is null) || string.IsNullOrWhiteSpace(material.Tex))
                return new ShaderResourceView(Context.Device, TextureFromBitmap(Properties.Resources.White));

            if (material.TexExt.ToLower() == ".tga")
            {
                using (var tgaMap = new TGASharpLib.TGA(material.TexFullPath).ToBitmap())
                using (var tex = new Bitmap(tgaMap))
                {
                    return new ShaderResourceView(Context.Device, TextureFromBitmap(tex));
                }
            }

            return ShaderResourceView.FromFile(Context.Device, material.TexFullPath);
        }

        protected override void Dispose(bool disposing)
        {
            TexturePlate?.Dispose();
            CurrentTexture = null;
            foreach (var resource in TextureCache.Values)
            {
                resource?.Dispose();
            }
            foreach (var mesh in UVMeshCache.Values)
            {
                mesh?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
