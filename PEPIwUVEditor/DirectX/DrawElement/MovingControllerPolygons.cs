using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System.Linq;

namespace IwUVEditor.DirectX.DrawElement
{
    class MovingControllerPolygons : DrawElement<VectorOffset>, IDrawElement
    {
        private Vector3 center;
        private Vector2 screenSize;
        private Elements selectedElement;

        public Vector3 Center
        {
            get => center;
            set
            {
                center = value;
                UpdateVertices();
            }
        }
        public Vector2 ScreenSize
        {
            get => screenSize;
            set
            {
                screenSize = value;
                UpdateVertices();
            }
        }
        private Vector3 AspectRatio => new Vector3(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1,
                0
            );

        public float ArrowShaftLength { get; set; }
        public float ArrowShaftWidth { get; set; }
        public float ArrowHeadWidth { get; set; }
        public float ArrowHeadLength { get; set; }
        public float CenterSquareEdgeLength { get; set; }

        public Color4 ArrowShaftColor { get; set; }
        public Color4 XAxisHeadColor { get; set; }
        public Color4 YAxisHeadColor { get; set; }
        public Color4 CenterSquareColor { get; set; }

        public (Vector3 A, Vector3 B, Vector3 C) XAxisHeadVertices
        {
            get
            {
                Vector3 aspectRatio = AspectRatio;
                return (
                            new Vector3(ArrowShaftLength + ArrowHeadLength, 0, 0).ElementProduct(aspectRatio),
                            new Vector3(ArrowShaftLength, ArrowHeadWidth / 2, 0).ElementProduct(aspectRatio),
                            new Vector3(ArrowShaftLength, -ArrowHeadWidth / 2, 0).ElementProduct(aspectRatio)
                        );
            }
        }
        public (Vector3 A, Vector3 B, Vector3 C) YAxisHeadVertices
        {
            get
            {
                Vector3 aspectRatio = AspectRatio;
                return (
                            new Vector3(0, ArrowShaftLength + ArrowHeadLength, 0).ElementProduct(aspectRatio),
                            new Vector3(ArrowHeadWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectRatio),
                            new Vector3(-ArrowHeadWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectRatio)
                        );
            }
        }

        internal Elements SelectedElement
        {
            get => selectedElement;
            set
            {
                selectedElement = value;
                UpdateVertices();
            }
        }
        public Color4 SelectionColor { get; }

        public MovingControllerPolygons(Device device, Effect effect, RasterizerState drawMode) : base(device, effect.GetTechniqueByName("VectorOffsetTechnique").GetPassByName("DrawScalingControllerPass"), drawMode)
        {
            ArrowShaftLength = 0.1f;
            ArrowHeadWidth = 0.005f;
            ArrowHeadLength = 0.05f;
            ArrowHeadWidth = 0.025f;
            CenterSquareEdgeLength = 0.025f;

            ArrowShaftColor = new Color4(0, 0, 0);
            XAxisHeadColor = new Color4(1, 0, 0);
            YAxisHeadColor = new Color4(0, 1, 0);
            CenterSquareColor = new Color4(0, 0, 0);

            SelectionColor = new Color4(1, 0, 1);

            Initialize();
        }

        protected override void DrawToDevice()
        {
            Device.ImmediateContext.DrawIndexed(4, 0, 0);
            Device.ImmediateContext.DrawIndexed(4, 0, 4);
            Device.ImmediateContext.DrawIndexed(4, 0, 8);
            Device.ImmediateContext.DrawIndexed(3, 0, 12);
            Device.ImmediateContext.DrawIndexed(3, 0, 15);
        }

        protected override VectorOffset[] CreateVertices()
        {
            Color4 xAxisHeadColor = SelectedElement == Elements.X ? SelectionColor : XAxisHeadColor;
            Color4 yAxisHeadColor = SelectedElement == Elements.Y ? SelectionColor : YAxisHeadColor;

            Vector3 aspectRatio = AspectRatio;

            var arrowShaftX = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(0, ArrowShaftWidth / 2, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(0, -ArrowShaftWidth / 2, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength, ArrowShaftWidth / 2, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength, -ArrowShaftWidth / 2, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                }
            };

            var arrowShaftY = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftWidth / 2, 0, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-ArrowShaftWidth / 2, 0, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-ArrowShaftWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectRatio),
                    Color = ArrowShaftColor
                }
            };

            var centerSquare = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(CenterSquareEdgeLength / 2, CenterSquareEdgeLength / 2, 0).ElementProduct(aspectRatio),
                    Color = CenterSquareColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(CenterSquareEdgeLength / 2, -CenterSquareEdgeLength / 2, 0).ElementProduct(aspectRatio),
                    Color = CenterSquareColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-CenterSquareEdgeLength / 2, CenterSquareEdgeLength / 2, 0).ElementProduct(aspectRatio),
                    Color = CenterSquareColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-CenterSquareEdgeLength / 2, -CenterSquareEdgeLength / 2, 0).ElementProduct(aspectRatio),
                    Color = CenterSquareColor
                }
            };

            (var xa, var xb, var xc) = XAxisHeadVertices;
            var arrowHeadX = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = xa,
                    Color = xAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = xb,
                    Color = xAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = xc,
                    Color = xAxisHeadColor
                }
            };

            (var ya, var yb, var yc) = YAxisHeadVertices;
            var arrowHeadY = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = ya,
                    Color = yAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = yb,
                    Color = yAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = yc,
                    Color = yAxisHeadColor
                }
            };

            return arrowShaftX.Concat(arrowShaftY).Concat(centerSquare).Concat(arrowHeadX).Concat(arrowHeadY).ToArray();
        }

        protected override uint[] CreateIndices()
        {
            return Enumerable.Range(0, CreateVertices().Length).Select(i => (uint)i).ToArray();
        }

        internal enum Elements
        {
            None,
            Center,
            X,
            Y,
        }
    }
}
