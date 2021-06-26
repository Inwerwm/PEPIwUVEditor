using DxManager;
using DxManager.Camera;
using IwUVEditor.DirectX.DrawElement;
using IwUVEditor.Manager;
using IwUVEditor.StateContainer;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Drawing;

namespace IwUVEditor.DirectX
{
    internal delegate void ScreenSizeChangedEventHandler(Vector2 screenSize);

    class UVViewDrawProcess : DxProcess
    {
        #region フィールド
        private float radiusOfPositionSquare;
        private Color4 colorInDefault;
        private Color4 colorInSelected;
        #endregion

        #region イベント
        internal event CatchExceptionEventHandler CatchException;
        internal event ScreenSizeChangedEventHandler ScreenSizeChanged;
        #endregion

        public EditorStates Current { get; }

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
            DeltaOffset = -6600,
            Amplitude = 200000,
            Offset = 50f,
            Step = 0.1f,
            Gain = 1,
            LowerLimit = -10000,
            UpperLimit = 12000,
        };

        TexturePlate TexturePlate { get; set; }
        ShaderResourceView SignTexture { get; set; }

        internal GenerableMap<Material, ShaderResourceView> Textures { get; set; }
        internal GenerableMap<Material, UVMesh> UVMeshes { get; set; }
        internal GenerableMap<Material, PositionSquares> PositionSquares { get; set; }

        public RasterizerStateProvider Rasterize { get; private set; }

        public Vector2 ScreenSize => new Vector2(Context.TargetControl.ClientSize.Width, Context.TargetControl.ClientSize.Height);

        public float RadiusOfPositionSquare
        {
            get => radiusOfPositionSquare;
            set
            {
                radiusOfPositionSquare = value;
                if (PositionSquares is null)
                    return;
                foreach (var sq in PositionSquares.Values)
                {
                    sq.Radius = radiusOfPositionSquare;
                }
            }
        }

        public Color4 ColorInDefault
        {
            get => colorInDefault;
            set
            {
                colorInDefault = value;
                if (UVMeshes is null)
                    return;
                foreach (var mesh in UVMeshes.Values)
                {
                    mesh.LineColor = value;
                }
                foreach (var sq in PositionSquares.Values)
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
                if (PositionSquares is null)
                    return;
                foreach (var sq in PositionSquares.Values)
                {
                    sq.ColorInSelected = value;
                }
            }
        }

        public Color4 BackgroundColor { get; set; }

        public UVViewDrawProcess(EditorStates inputManager)
        {
            Current = inputManager;
            BackgroundColor = new Color4(1.0f, 0.3f, 0.3f, 0.3f);
        }

        public override void Init()
        {
            Rasterize = new RasterizerStateProvider(Context.Device) { CullMode = CullMode.None };

            TexturePlate = new TexturePlate(Context.Device, Effect, Rasterize.Solid) { InstanceParams = (10, 0.5f) };
            SignTexture = new ShaderResourceView(Context.Device, TextureFromBitmap(Properties.Resources.CenterSign));

            Textures = new GenerableMap<Material, ShaderResourceView>(LoadTexture);
            UVMeshes = new GenerableMap<Material, UVMesh>((material) => new UVMesh(Context.Device, Effect, Rasterize.Wireframe, material, ColorInDefault));
            PositionSquares = new GenerableMap<Material, PositionSquares>((material) => new PositionSquares(Context.Device, Effect, Rasterize.Solid, material, RadiusOfPositionSquare, ColorInDefault, colorInSelected));
        }

        public override void Draw()
        {
            try
            {
                // 背景色を描画
                Context.Device.ImmediateContext.ClearRenderTargetView(Context.RenderTarget, BackgroundColor);
                // 深度バッファ
                Context.Device.ImmediateContext.ClearDepthStencilView(Context.DepthStencil, DepthStencilClearFlags.Depth, 1, 0);
                // テクスチャを読み込み
                Effect.GetVariableByName("diffuseTexture").AsResource().SetResource(Textures[Current.Material]);
                Effect.GetVariableByName("signTexture").AsResource().SetResource(SignTexture);

                // テクスチャ板を描画
                TexturePlate.Prepare();

                // メッシュを描画
                UVMeshes[Current.Material].Prepare();

                // 頂点位置に四角を描画
                PositionSquares[Current.Material].Prepare();

                // ツール固有の描画処理を実行
                Current.Tool?.PrepareDrawing();

                // 描画内容を反映
                Context.SwapChain.Present(0, PresentFlags.None);
            }
            catch (Exception ex)
            {
                CatchException?.Invoke(ex);
            }
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
            (Camera as DxCameraOrthographic).ViewVolumeSize = (ScreenSize.X, ScreenSize.Y);

            if (PositionSquares is null)
                return;
            foreach (var ps in PositionSquares.Values)
            {
                ps.ScreenSize = ScreenSize;
            }

            ScreenSizeChanged?.Invoke(ScreenSize);
        }

        public void UpdateDrawingVertices()
        {
            UVMeshes[Current.Material].UpdateVertices();
            PositionSquares[Current.Material].UpdateVertices();
        }

        public Vector2 ScreenPosToWorldPos(Vector2 screenPos)
        {
            Vector2 normalizedPos = new Vector2(2 * screenPos.X / Context.TargetControl.Width - 1, 1 - 2 * screenPos.Y / Context.TargetControl.Height);
            Vector4 worldPos = Vector4.Transform(new Vector4(normalizedPos, 0, 1), InvertTransMatrix);
            return new Vector2(worldPos.X, worldPos.Y);
        }

        public Vector2 WorldPosToScreenPos(Vector2 worldPos)
        {
            // 描画領域内での割合座標 [0,1]
            Vector2 rationalPos = (Vector2.TransformCoordinate(worldPos, TransMatrix) + new Vector2(1, 1)) / 2;
            return new Vector2((int)Math.Round(rationalPos.X * ScreenSize.X, MidpointRounding.AwayFromZero), (int)Math.Round((1 - rationalPos.Y) * ScreenSize.Y, MidpointRounding.AwayFromZero));
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
            if ((material is null) || string.IsNullOrWhiteSpace(material.TexFullPath))
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
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    TexturePlate?.Dispose();
                    SignTexture?.Dispose();
                    foreach (var resource in Textures.Values)
                    {
                        resource?.Dispose();
                    }
                    foreach (var mesh in UVMeshes.Values)
                    {
                        mesh?.Dispose();
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}
