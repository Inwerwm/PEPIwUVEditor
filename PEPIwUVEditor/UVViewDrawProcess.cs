using DxManager;
using IwUVEditor.DrawElement;
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

namespace IwUVEditor
{
    class UVViewDrawProcess : DxProcess
    {
        private Material currentMaterial;

        private bool isViewMoving = false;

        RasterizerStateProvider Rasterize { get; set; }

        public Vector3 ShiftOffset { get; set; } = Vector3.Zero; //publicなのはデバッグ用
        public ScaleManager Scale { get; } = new ScaleManager() //publicなのはデバッグ用
        {
            DeltaOffset = -8000,
            Amplitude = 1000,
            Offset = 0,
            Step = 0.1f,
            Gain = 1,
        };

        public bool IsActive { get; set; } = true;
        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>()
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };

        TexturePlate TexturePlate { get; set; }

        Dictionary<Material, ShaderResourceView> TextureCache { get; } = new Dictionary<Material, ShaderResourceView>();
        ShaderResourceView CurrentTexture { get; set; }

        Dictionary<Material, UVMesh> UVMeshCache { get; } = new Dictionary<Material, UVMesh>();
        UVMesh CurrentUVMesh { get; set; }

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
                    UVMeshCache.Add(value, new UVMesh(Context.Device, Effect, Rasterize.Wireframe, value));
                }

                CurrentTexture = TextureCache[value];
                CurrentUVMesh = UVMeshCache[value];
            }
        }

        public override void Init()
        {
            CurrentTexture = LoadTexture(null);

            Rasterize = new RasterizerStateProvider(Context.Device);

            TexturePlate = new TexturePlate(Context.Device, Effect, Rasterize.Solid) { InstanceParams = (10, 0.5f) };
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

            // 描画内容を反映
            Context.SwapChain.Present(0, PresentFlags.None);
        }

        protected override void UpdateCamera()
        {
            var scale = Scale.Scale;
            Matrix transMatrix = Camera.GetMatrix() * Matrix.Translation(ShiftOffset) * Matrix.Scaling(scale, scale, 1);
            Effect.GetVariableByName("ViewProjection").AsMatrix().SetMatrix(transMatrix);
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

            bool isViewScaling = false;

            switch (e.ButtonFlags)
            {
                case MouseButtonFlags.None:
                    break;
                case MouseButtonFlags.MouseWheel:
                    isViewScaling = true;
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
                    isViewMoving = false;
                    break;
                case MouseButtonFlags.MiddleDown:
                    isViewMoving = true;
                    break;
                case MouseButtonFlags.RightUp:
                    break;
                case MouseButtonFlags.RightDown:
                    break;
                case MouseButtonFlags.LeftUp:
                    break;
                case MouseButtonFlags.LeftDown:
                    break;
                default:
                    break;
            }

            float modifier = (IsPress[Keys.ShiftKey] ? 4f : 1f) / (IsPress[Keys.ControlKey] ? 4f : 1f);
            if (isViewMoving)
                ShiftOffset += modifier * new Vector3(1f * e.X / Context.TargetControl.Width, -1f * e.Y / Context.TargetControl.Height, 0) / Scale.Scale;
            if (isViewScaling)
                Scale.WheelDelta += e.WheelDelta * modifier;
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
