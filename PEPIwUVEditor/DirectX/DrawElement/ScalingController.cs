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
    class ScalingController : DrawElement<VectorOffset>, IDrawElement
    {
        private Vector2 screenSize;
        private Vector3 center;
        private Elements selectedElement;

        internal Elements SelectedElement
        {
            get => selectedElement;
            set
            {
                selectedElement = value;
                UpdateVertices();
            }
        }
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

        // 描画するコントロールの形状設定パラメータ

        /// <summary>
        /// 制御端子全体の大きさ
        /// </summary>
        float Size { get; }
        /// <summary>
        /// 制御端子何個分間隔を開けるか
        /// </summary>
        float MarginRatio { get; }
        /// <summary>
        /// X,Y軸拡大制御端子の中央位置制御端子に対する大きさの比率
        /// </summary>
        float AxisRatio { get; }

        Color4 CenterColor { get; }
        Color4 AxisXColor { get; }
        Color4 AxisYColor { get; }
        Color4 SelectionColor { get; }

        public SquareCoord CenterSquareCoord => CreateSquare(Size / (MarginRatio + 2), 0, 0);
        public SquareCoord XSquareCoord => CreateSquare(Size * AxisRatio / (MarginRatio + 2), (MarginRatio + 1) * 2, 0);
        public SquareCoord YSquareCoord => CreateSquare(Size * AxisRatio / (MarginRatio + 2), 0, (MarginRatio + 1) * 2);

        public ScalingController(Device device, Effect effect, RasterizerState drawMode, float size, Vector2 screenSize) :
            base(device, effect.GetTechniqueByName("VectorOffsetTechnique").GetPassByName("DrawScalingControllerPass"), drawMode)
        {
            Size = size;
            this.screenSize = screenSize;

            MarginRatio = 1;
            AxisRatio = 0.75f;

            CenterColor = new Color4(0, 0, 0);
            AxisXColor = new Color4(1, 0, 0);
            AxisYColor = new Color4(0, 1, 0);
            SelectionColor = new Color4(1, 0, 1);

            Initialize();
        }

        protected override void DrawToDevice()
        {
            Device.ImmediateContext.DrawIndexed(4, 0, 0);
            Device.ImmediateContext.DrawIndexed(4, 0, 4);
            Device.ImmediateContext.DrawIndexed(4, 0, 8);
        }

        protected override VectorOffset[] CreateVertices()
        {
            var cPos = CenterSquareCoord;
            var xPos = XSquareCoord;
            var yPos = YSquareCoord;

            Color4 centerColor = SelectedElement == Elements.Center ? SelectionColor : CenterColor;
            Color4 axisXColor = SelectedElement == Elements.X ? SelectionColor : AxisXColor;
            Color4 axisYColor = SelectedElement == Elements.Y ? SelectionColor : AxisYColor;

            VectorOffset[] centerVertices = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = cPos.NN,
                    Color = centerColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = cPos.PN,
                    Color = centerColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = cPos.NP,
                    Color = centerColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = cPos.PP,
                    Color = centerColor,
                },
            };

            VectorOffset[] xAxisVertices = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = xPos.NN,
                    Color = axisXColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = xPos.PN,
                    Color = axisXColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = xPos.NP,
                    Color = axisXColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = xPos.PP,
                    Color = axisXColor,
                },
            };

            VectorOffset[] yAxisVertices = new[]
            {
                new VectorOffset
                {
                    Position = Center,
                    Offset = yPos.NN,
                    Color = axisYColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = yPos.PN,
                    Color = axisYColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = yPos.NP,
                    Color = axisYColor,
                },
                new VectorOffset
                {
                    Position = Center,
                    Offset = yPos.PP,
                    Color = axisYColor,
                },
            };

            return centerVertices.Concat(xAxisVertices).Concat(yAxisVertices).ToArray();
        }

        protected override uint[] CreateIndices() => new uint[]
        {
            0, 1, 2, 3
        };

        SquareCoord CreateSquare(float ratio, float xMargin, float yMargin)
        {
            Vector2 aspectCorrection = new Vector2(
                ScreenSize.X > ScreenSize.Y ? ScreenSize.Y / ScreenSize.X : 1,
                ScreenSize.Y > ScreenSize.X ? ScreenSize.X / ScreenSize.Y : 1
            );

            return (
                ratio * new Vector3(xMargin - aspectCorrection.X, yMargin - aspectCorrection.Y, 0),
                ratio * new Vector3(xMargin + aspectCorrection.X, yMargin - aspectCorrection.Y, 0),
                ratio * new Vector3(xMargin - aspectCorrection.X, yMargin + aspectCorrection.Y, 0),
                ratio * new Vector3(xMargin + aspectCorrection.X, yMargin + aspectCorrection.Y, 0)
            );
        }

        internal enum Elements
        {
            None,
            Center,
            X,
            Y,
        }
    }

    internal struct SquareCoord
    {
        public Vector3 NN;
        public Vector3 PN;
        public Vector3 NP;
        public Vector3 PP;

        public SquareCoord(Vector3 nN, Vector3 pN, Vector3 nP, Vector3 pP)
        {
            NN = nN;
            PN = pN;
            NP = nP;
            PP = pP;
        }

        internal SquareCoord ReverseY()
        {
            var ry = new Vector3(1, -1, 1);
            return (NP.ElementProduct(ry), PP.ElementProduct(ry), NN.ElementProduct(ry), PN.ElementProduct(ry));
        }

        internal System.Drawing.RectangleF ToRectangleF(Vector3 centerOffset, Func<Vector2, Vector2> mapper)
        {
            var min = mapper(NN.ToVector2());
            var max = mapper(PP.ToVector2());
            return new System.Drawing.RectangleF(min.X + centerOffset.X, min.Y + centerOffset.Y, max.X - min.X, max.Y - min.Y);
        }

        public override bool Equals(object obj)
        {
            return obj is SquareCoord other &&
                   NN.Equals(other.NN) &&
                   PN.Equals(other.PN) &&
                   NP.Equals(other.NP) &&
                   PP.Equals(other.PP);
        }

        public override string ToString()
        {
            return $"X : {NN.X} - {PP.X}{Environment.NewLine}" +
                   $"Y : {NN.Y} - {PP.Y}";
        }

        public override int GetHashCode()
        {
            int hashCode = -1930981952;
            hashCode = hashCode * -1521134295 + NN.GetHashCode();
            hashCode = hashCode * -1521134295 + PN.GetHashCode();
            hashCode = hashCode * -1521134295 + NP.GetHashCode();
            hashCode = hashCode * -1521134295 + PP.GetHashCode();
            return hashCode;
        }

        public void Deconstruct(out Vector3 nN, out Vector3 pN, out Vector3 nP, out Vector3 pP)
        {
            nN = NN;
            pN = PN;
            nP = NP;
            pP = PP;
        }

        public static implicit operator (Vector3 NN, Vector3 PN, Vector3 NP, Vector3 PP)(SquareCoord value)
        {
            return (value.NN, value.PN, value.NP, value.PP);
        }

        public static implicit operator SquareCoord((Vector3 NN, Vector3 PN, Vector3 NP, Vector3 PP) value)
        {
            return new SquareCoord(value.NN, value.PN, value.NP, value.PP);
        }
    }
}
