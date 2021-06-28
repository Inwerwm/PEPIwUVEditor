using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.ExportUV
{
    class GDIUVDrawer : IUVDrawer
    {
        public void Draw(UVMesh mesh, int imageSize, string texturePath, string exportPath, bool enableDrawBackground)
        {
            using (var texture = !File.Exists(texturePath) ? null : IsTGA(texturePath) ? new TGASharpLib.TGA(texturePath).ToBitmap() : new Bitmap(texturePath))
            {
                decimal textureWidth = texture?.Width ?? imageSize;
                decimal textureHeight = texture?.Height ?? imageSize;
                var hRatio = textureHeight / textureWidth;
                var outputWidth = imageSize;
                var outputHeight = (int)Math.Round(imageSize * hRatio, MidpointRounding.AwayFromZero);

                var uvDrawOffset = (X: -mesh.MinBound.X, Y: -mesh.MinBound.Y);
                var imagePosMesh = mesh.Mesh.Select(e => e.Add(uvDrawOffset.X, uvDrawOffset.Y).Mul(outputWidth - 1, outputHeight - 1));

                var textureRepeatCount = (X: mesh.MaxBound.X - mesh.MinBound.X, Y: mesh.MaxBound.Y - mesh.MinBound.Y);
                var background = texture != null && enableDrawBackground ? CreateBackgroundImage(texture, textureRepeatCount) : null;
                using (var bmp = new Bitmap(outputWidth * textureRepeatCount.X, outputHeight * textureRepeatCount.Y))
                {
                    using (var graph = Graphics.FromImage(bmp))
                    using (var pen = new Pen(Color.Black) { Width = 1 })
                    {
                        graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                        if (background != null)
                        {
                            graph.DrawImage(background, 0, 0, bmp.Width, bmp.Height);
                        }

                        foreach (var edge in imagePosMesh)
                        {
                            graph.DrawLine(pen, edge.UV[0], edge.UV[1]);
                        }
                    }
                    bmp.Save(exportPath, ImageFormat.Png);
                }
            }
        }

        private Bitmap CreateBackgroundImage(Bitmap texture, (int X, int Y) uvLengthRatio)
        {
            if (uvLengthRatio.X > 1 || uvLengthRatio.Y > 1)
                using (var repeatedTexture = CreateRepeatBitMap(texture, uvLengthRatio.X, uvLengthRatio.Y))
                    return repeatedTexture;
            else
                return texture;
        }

        private Bitmap CreateRepeatBitMap(Bitmap texture, int xCount, int yCount)
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

        private static bool IsTGA(string texturePath)
        {
            return Path.GetExtension(texturePath).ToLower() == ".tga";
        }
    }
}
