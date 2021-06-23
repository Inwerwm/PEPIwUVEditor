using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.DirectX.DrawElement
{
    class MovingController : DrawElement<VectorOffset>, IDrawElement
    {
        public Vector3 Center { get; set; }
        public Vector2 ScreenSize { get; set; }

        public float ArrowShaftLength { get; set; }
        public float ArrowShaftWidth { get; set; }
        public float ArrowHeadWidth { get; set; }
        public float ArrowHeadLength { get; set; }
        public float CenterSquareEdgeLength { get; set; }

        public Color4 ArrowShaftColor { get; set; }
        public Color4 XAxisHeadColor { get; set; }
        public Color4 YAxisHeadColor { get; set; }
        public Color4 CenterSquareColor { get; set; }

        public MovingController(Device device, Effect effect, RasterizerState drawMode) : base(device, effect.GetTechniqueByName("VectorOffsetTechnique").GetPassByName("DrawScalingControllerPass"), drawMode)
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
            Vector2 aspectCorrection = new Vector2(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1
            );

            var arrowShaftX = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(0, ArrowShaftWidth / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(0, -ArrowShaftWidth / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength, ArrowShaftWidth / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength, -ArrowShaftWidth / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                }
            };

            var arrowShaftY = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftWidth / 2, 0, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-ArrowShaftWidth / 2, 0, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-ArrowShaftWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = ArrowShaftColor
                }
            };

            var centerSquare = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(CenterSquareEdgeLength / 2, CenterSquareEdgeLength / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = CenterSquareColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(CenterSquareEdgeLength / 2, -CenterSquareEdgeLength / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = CenterSquareColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-CenterSquareEdgeLength / 2, CenterSquareEdgeLength / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = CenterSquareColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-CenterSquareEdgeLength / 2, -CenterSquareEdgeLength / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = CenterSquareColor
                }
            };

            var arrowHeadX = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength + ArrowHeadLength, 0, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = XAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength, ArrowHeadWidth / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = XAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowShaftLength, -ArrowHeadWidth / 2, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = XAxisHeadColor
                }
            };

            var arrowHeadY = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(0, ArrowShaftLength + ArrowHeadLength, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = YAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(ArrowHeadWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = YAxisHeadColor
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = new Vector3(-ArrowHeadWidth / 2, ArrowShaftLength, 0).ElementProduct(aspectCorrection.ToVector3()),
                    Color = YAxisHeadColor
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
