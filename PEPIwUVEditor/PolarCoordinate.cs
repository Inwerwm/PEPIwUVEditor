using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    internal class PolarCoordinate
    {
        public float Angle { get; private set; }
        public float Radius { get; private set; }

        private PolarCoordinate(){ }

        private static float CalcRadius(float x, float y) => (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        public static PolarCoordinate FromPolar(float angle, float radius) => new PolarCoordinate()
        {
            Angle = angle,
            Radius = radius
        };

        public static PolarCoordinate FromOrthogonal(float x, float y) => new PolarCoordinate()
        {
            Angle = x == 0 && y == 0 ? 0 : (float)(Math.Sign(y) * Math.Acos(x / CalcRadius(x, y))),
            Radius = CalcRadius(x,y)
        };

        public static PolarCoordinate FromOrthogonal(Vector2 vector) => FromOrthogonal(vector.X, vector.Y);

        public float DifferenceOfAngle(PolarCoordinate other)
        {
            float dif = Angle - other.Angle;

            // 普通に第2象限と第3象限の間で差をとると、長いほうの弧で値が出てしまうので分岐させて短いほうを計算して出す
            return IsTransLeftOrthant() ? (float)(-Math.Sign(dif) * (2 * Math.PI - Math.Abs(dif))) : dif;

            bool IsTransLeftOrthant() => (Math.Abs(Angle) > Math.PI / 2 && Math.Abs(other.Angle) > Math.PI / 2) && (Math.Sign(Angle) != Math.Sign(other.Angle));
        }


        public float DifferenceOfRadius(PolarCoordinate other) => Radius - other.Radius;
    }
}
