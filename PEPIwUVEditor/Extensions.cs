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
    }
}
