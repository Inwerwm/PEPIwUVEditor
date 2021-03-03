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

        public Vector2 Center
        {
            get;
            set;
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
        float MarginRatio { get; set; }
        /// <summary>
        /// X,Y軸拡大制御端子の中央位置制御端子に対する大きさの比率
        /// </summary>
        float AxisRatio { get; set; }

        Color4 CenterColor { get; set; }
        Color4 AxisXColor { get; set; }
        Color4 AxisYColor { get; set; }

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
            var unitRatio = 1 / (MarginRatio + 2);

            var margin = (MarginRatio + 1) * 2;

            VectorOffset[] centerVertices = new[]
            {
                new VectorOffset
                {
                    Position = Size * unitRatio * new Vector3(-1, -1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = CenterColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * new Vector3(1, -1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = CenterColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * new Vector3(-1, 1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = CenterColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * new Vector3(1, 1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = CenterColor,
                },
            };

            VectorOffset[] xAxisVertices = new[]
            {
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(-1 + margin, -1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisXColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(1 + margin, -1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisXColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(-1 + margin, 1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisXColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(1 + margin, 1, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisXColor,
                },
            };

            VectorOffset[] yAxisVertices = new[]
            {
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(-1, -1 + margin, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisYColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(1, -1 + margin, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisYColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(-1, 1 + margin, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisYColor,
                },
                new VectorOffset
                {
                    Position = Size * unitRatio * AxisRatio * new Vector3(1, 1 + margin, 0),
                    Offset = new Vector3(Center, 0),
                    Color = AxisYColor,
                },
            };

            return centerVertices.Concat(xAxisVertices).Concat(yAxisVertices).ToArray();
        }

        protected override uint[] CreateIndices() => new uint[]
        {
            0, 1, 2, 3
        };
    }
}
