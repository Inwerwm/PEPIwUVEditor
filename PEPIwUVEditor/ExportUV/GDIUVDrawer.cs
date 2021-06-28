using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace IwUVEditor.ExportUV
{
    class GDIUVDrawer : IUVDrawer
    {
        public void Draw(UVMesh mesh, int imageSize, string texturePath, string exportPath, bool enableDrawBackground)
        {
            using (var texture = !File.Exists(texturePath) ? null : IsTGA(texturePath) ? new TGASharpLib.TGA(texturePath).ToBitmap() : new Bitmap(texturePath))
            {
                var textureRepeatCount = (X: mesh.MaxBound.X - mesh.MinBound.X, Y: mesh.MaxBound.Y - mesh.MinBound.Y);
                var background = texture != null && enableDrawBackground ? CreateBackgroundImage(texture, textureRepeatCount) : null;

                var unitSize = CalcUnitSize(imageSize, texture);

                using (var bmp = new Bitmap(unitSize.X * textureRepeatCount.X, unitSize.Y * textureRepeatCount.Y))
                {
                    DrawMesh(bmp, background, mesh, unitSize);
                    bmp.Save(exportPath, ImageFormat.Png);
                }
            }
        }

        private static bool IsTGA(string texturePath) =>
            Path.GetExtension(texturePath).ToLower() == ".tga";

        private static Point CalcUnitSize(int imageSize, Bitmap texture)
        {
            decimal textureWidth = texture?.Width ?? imageSize;
            decimal textureHeight = texture?.Height ?? imageSize;

            var unitWidth = imageSize;

            var hRatio = textureHeight / textureWidth;
            var unitHeight = (int)Math.Round(imageSize * hRatio, MidpointRounding.AwayFromZero);

            return new Point(unitWidth, unitHeight);
        }

        private static Bitmap CreateBackgroundImage(Bitmap texture, (int X, int Y) repeatCount)
        {
            if (repeatCount.X > 1 || repeatCount.Y > 1)
                using (var repeatedTexture = CreateRepeatBitMap(texture, repeatCount.X, repeatCount.Y))
                    return repeatedTexture;
            else
                return texture;
        }

        private static Bitmap CreateRepeatBitMap(Bitmap texture, int xCount, int yCount)
        {
            var result = new Bitmap(texture.Width * xCount, texture.Height * yCount);
            using (var g = Graphics.FromImage(result))
            {
                for (int x = 0; x < xCount; x++)
                {
                    for (int y = 0; y < yCount; y++)
                    {
                        g.DrawImageUnscaled(texture, texture.Width * x, texture.Height * y);
                    }
                }
            }
            return result;
        }

        private static void DrawMesh(Bitmap drawTartget, Bitmap background, UVMesh mesh, Point unitSize)
        {
            var uvDrawOffset = (X: -mesh.MinBound.X, Y: -mesh.MinBound.Y);
            var imagePosMesh = mesh.Mesh.Select(e => e.Add(uvDrawOffset.X, uvDrawOffset.Y).Mul(unitSize.X - 1, unitSize.Y - 1));

            using (var graph = Graphics.FromImage(drawTartget))
            using (var pen = new Pen(Color.Black) { Width = 1 })
            {
                graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                if (background != null)
                    graph.DrawImage(background, 0, 0, drawTartget.Width, drawTartget.Height);

                foreach (var edge in imagePosMesh)
                {
                    graph.DrawLine(pen, edge.UV[0], edge.UV[1]);
                }
            }
        }
    }
}
