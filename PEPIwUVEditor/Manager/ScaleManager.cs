using System;

namespace IwUVEditor.Manager
{
    class ScaleManager
    {
        private float wheelDelta;

        /// <summary>
        /// 計算された拡大率
        /// </summary>
        public float Scale => Sigmoid((DeltaOffset + WheelDelta) * (Step / 120)) * Amplitude + Offset;
        /// <summary>
        /// 現在のスクロール量
        /// </summary>
        public float WheelDelta
        {
            get => wheelDelta;
            set => wheelDelta = value > UpperLimit ? UpperLimit : value < LowerLimit ? LowerLimit : value;
        }
        /// <summary>
        /// スクロール量加算値
        /// </summary>
        public int DeltaOffset { get; set; }
        /// <summary>
        /// シグモイド関数の結果に掛ける数
        /// </summary>
        public float Amplitude { get; set; }
        /// <summary>
        /// シグモイド関数の結果に加える数
        /// </summary>
        public float Offset { get; set; }
        /// <summary>
        /// 一回のスクロールで移動する量
        /// </summary>
        public float Step { get; set; }
        /// <summary>
        /// シグモイド関数のゲイン
        /// </summary>
        public float Gain { get; set; }
        /// <summary>
        /// スクロール量の下限
        /// </summary>
        public int LowerLimit { get; set; } = int.MinValue;
        /// <summary>
        /// スクロール量の上限
        /// </summary>
        public int UpperLimit { get; set; } = int.MaxValue;

        private float Sigmoid(float x) => 1 / (1 + (float)Math.Pow(Math.E, -1 * Gain * x));
    }
}
