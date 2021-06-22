using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    static class Extensions
    {
        /// <summary>
        /// Vector3の(X, Y)をVector2に変換する
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vec) => new Vector2(vec.X, vec.Y);

        /// <summary>
        /// Vector2をVector3に変換する
        /// Z座標は0
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vec) => new Vector3(vec, 0);

        /// <summary>
        /// 要素ごとの掛け算
        /// </summary>
        public static Vector2 ElementProduct(this Vector2 multiplicand, Vector2 multiplier) =>
            new Vector2(multiplicand.X * multiplier.X, multiplicand.Y * multiplier.Y);

        /// <summary>
        /// 要素ごとの掛け算
        /// </summary>
        public static Vector3 ElementProduct(this Vector3 multiplicand, Vector3 multiplier) =>
            new Vector3(multiplicand.X * multiplier.X, multiplicand.Y * multiplier.Y, multiplicand.Z * multiplier.Z);

        /// <summary>
        /// 要素ごとの割り算
        /// </summary>
        public static Vector2 ElementDivision(this Vector2 dividend, Vector2 divisor) =>
            new Vector2(dividend.X / divisor.X, dividend.Y / divisor.Y);

        /// <summary>
        /// 要素ごとの割り算
        /// </summary>
        public static Vector3 ElementDivision(this Vector3 dividend, Vector3 divisor) =>
            new Vector3(dividend.X / divisor.X, dividend.Y / divisor.Y, dividend.Z / divisor.Z);
    }
}
