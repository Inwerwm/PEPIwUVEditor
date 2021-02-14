using DxManager;
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
using Buffer = SlimDX.Direct3D11.Buffer;

namespace IwUVEditor.DirectX
{
    class UVViewDrawProcess : DxProcess
    {
        private Material currentMaterial;

        private float radiusOfPositionSquare;

        Editor Editor { get; set; }

        RasterizerStateProvider Rasterize { get; set; }

        Vector3 ShiftOffset { get; set; }
        ScaleManager Scale { get; } = new ScaleManager()
        {
            DeltaOffset = -10000,
            Amplitude = 200000,
            Offset = 50f,
            Step = 0.1f,
            Gain = 1,
            LowerLimit = -10000,
            UpperLimit = 12000,
        };
        private Matrix TransMatrix => Camera.GetMatrix() * Matrix.Translation(ShiftOffset) * Matrix.Scaling(Scale.Scale, Scale.Scale, 1);


        public bool IsActive { get; set; } = true;
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };
        public Dictionary<MouseButtons, bool> IsClicking { get; } = new Dictionary<MouseButtons, bool>
        {
            { MouseButtons.Left, false },
            { MouseButtons.Middle, false },
            { MouseButtons.Right, false },
        };

        public DragManager LeftDrag { get; set; } = new DragManager();

        /// <summary>
        /// <para>現在のマウスポインタの座標</para>
        /// <para>set : form側での描画位置</para>
        /// <para>get : 描画されている空間におけるXY座標</para>
        /// </summary>
        public Vector2 CurrentMousePos
        {
            get => currentMousePos;
            set
            {
                Vector2 normalizedPos = new Vector2(2 * value.X / Context.TargetControl.Width - 1, 1 - 2 * value.Y / Context.TargetControl.Height);

                Matrix vprtMat = Matrix.Identity;
                vprtMat.M11 = Context.TargetControl.ClientSize.Width / 2;
                vprtMat.M14 = Context.TargetControl.ClientSize.Width / 2;
                vprtMat.M22 = -Context.TargetControl.ClientSize.Height / 2;
                vprtMat.M24 = Context.TargetControl.ClientSize.Height / 2;


                Matrix iViewMat = Matrix.Invert(Camera.CreateViewMatrix());
                Matrix iPrjMat = Matrix.Invert((Camera as DxManager.Camera.DxCameraOrthographic).CreateProjectionMatrix());
                Matrix iVprtMat = Matrix.Invert(vprtMat);
                Matrix iOfstMat = Matrix.Invert(Matrix.Translation(ShiftOffset));
                Matrix iSclMat = Matrix.Invert(Matrix.Scaling(Scale.Scale, Scale.Scale, 1));
                Vector4 worldPos = Vector4.Transform(new Vector4(normalizedPos, 0, 1), iSclMat * iOfstMat * iPrjMat * iViewMat);
                currentMousePos = new Vector2(worldPos.X, worldPos.Y);
            }
        }

        TexturePlate TexturePlate { get; set; }
        SelectionRectangle SelectionRectangle { get; set; }

        Dictionary<Material, ShaderResourceView> TextureCache { get; } = new Dictionary<Material, ShaderResourceView>();
        ShaderResourceView CurrentTexture { get; set; }

        Dictionary<Material, UVMesh> UVMeshCache { get; } = new Dictionary<Material, UVMesh>();
        UVMesh CurrentUVMesh { get; set; }

        Dictionary<Material, PositionSquares> PositionSquareCache { get; } = new Dictionary<Material, PositionSquares>();
        PositionSquares CurrentPositionSquares;
        private Color4 colorInDefault;
        private Color4 colorInSelected;
        private Vector2 currentMousePos;

        public Material CurrentMaterial
        {
            get => currentMaterial;
            set
            {
                if (Context is null)
                    return;
                currentMaterial = value;
                if (!TextureCache.Keys.Contains(value))
                {
                    TextureCache.Add(value, LoadTexture(value));
                    UVMeshCache.Add(value, new UVMesh(Context.Device, Effect, Rasterize.Wireframe, value, ColorInDefault));
                    PositionSquareCache.Add(value, new PositionSquares(Context.Device, Effect, Rasterize.Solid, value, RadiusOfPositionSquare, ColorInDefault, colorInSelected));
                }

                CurrentTexture = TextureCache[value];
                CurrentUVMesh = UVMeshCache[value];
                CurrentPositionSquares = PositionSquareCache[value];
            }
        }

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

        public float RadiusOfPositionSquare
        {
            get => radiusOfPositionSquare;
            set
            {
                radiusOfPositionSquare = value;
                foreach (var sq in PositionSquareCache.Values)
                {
                    sq.Radius = radiusOfPositionSquare;
                }
            }
        }

        public UVViewDrawProcess(Editor editor)
        {
            Editor = editor;
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
            switch (Editor.CurrentTool)
            {
                case Tool.RectangleSelection:
                    if(LeftDrag.IsDragging)
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

        protected override void MouseInput(object sender, MouseInputEventArgs e)
        {
            if (!IsActive)
                return;

            float modifier = (IsPress[Keys.ShiftKey] ? 4f : 1f) / (IsPress[Keys.ControlKey] ? 4f : 1f);

            switch (e.ButtonFlags)
            {
                case MouseButtonFlags.None:
                    break;
                case MouseButtonFlags.MouseWheel:
                    Scale.WheelDelta += e.WheelDelta * modifier;
                    break;
                case MouseButtonFlags.Button5Up:
                    break;
                case MouseButtonFlags.Button5Down:
                    break;
                case MouseButtonFlags.Button4Up:
                    break;
                case MouseButtonFlags.Button4Down:
                    break;
                case MouseButtonFlags.MiddleUp:
                    IsClicking[MouseButtons.Middle] = false;
                    break;
                case MouseButtonFlags.MiddleDown:
                    IsClicking[MouseButtons.Middle] = true;
                    break;
                case MouseButtonFlags.RightUp:
                    IsClicking[MouseButtons.Right] = false;
                    break;
                case MouseButtonFlags.RightDown:
                    IsClicking[MouseButtons.Right] = true;
                    break;
                case MouseButtonFlags.LeftUp:
                    IsClicking[MouseButtons.Left] = false;
                    break;
                case MouseButtonFlags.LeftDown:
                    IsClicking[MouseButtons.Left] = true;
                    break;
                default:
                    break;
            }

            if (IsClicking[MouseButtons.Middle])
                ShiftOffset += modifier * new Vector3(1f * e.X / Context.TargetControl.Width, -1f * e.Y / Context.TargetControl.Height, 0) / Scale.Scale;

            LeftDrag.ReadState(CurrentMousePos, IsClicking[MouseButtons.Left]);
            if (LeftDrag.IsStartingJust)
            {
                SelectionRectangle.StartPos = LeftDrag.Start;
            }

            if (LeftDrag.IsDragging)
            {
                SelectionRectangle.EndPos = LeftDrag.Current;
            }

            if (LeftDrag.IsEndDrag)
            {
                LeftDrag.Reset();
            }
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
