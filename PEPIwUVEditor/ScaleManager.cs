using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class ScaleManager
    {
        /// <summary>
        /// 計算された拡大率
        /// </summary>
        public float Scale => Sigmoid((DeltaOffset + WheelDelta) * (Step / 120)) * Amplitude + Offset;
        /// <summary>
        /// 現在のスクロール量
        /// </summary>
        public float WheelDelta { get; set; }
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

        private float Sigmoid(float x) => 1 / (1 + (float)Math.Pow(Math.E, -1 * Gain * x));
    }
}
