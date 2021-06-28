using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace IwUVEditor.ExportUV
{
    class UVMesh
    {
        IEnumerable<UVEdge> Mesh { get; }
        Point MinBound { get; }
        Point MaxBound { get; }

        public UVMesh(IList<IPXVertex> vertices, IList<IPXFace> faces)
        {
            Mesh = faces.AsParallel().SelectMany(UVEdge.FromFace);
            (MinBound, MaxBound) = CalcUVRange(Mesh);
        }

        private void DrawUV(int imageSize, string texturePath, string exportPath, bool enableDrawBackground, ParallelQuery<UVEdge> mesh, (Point Min, Point Max) uvRange)
        {
            using (var texture = !File.Exists(texturePath) ? null : IsTGA(texturePath) ? new TGASharpLib.TGA(texturePath).ToBitmap() : new Bitmap(texturePath))
            {
                decimal textureWidth = texture?.Width ?? imageSize;
                decimal textureHeight = texture?.Height ?? imageSize;
                var hRatio = textureHeight / textureWidth;
                var outputWidth = imageSize;
                var outputHeight = (int)Math.Round(imageSize * hRatio, MidpointRounding.AwayFromZero);

                var uvDrawOffset = (X: -uvRange.Min.X, Y: -uvRange.Min.Y);
                var imagePosMesh = mesh.Select(e => e.Add(uvDrawOffset.X, uvDrawOffset.Y).Mul(outputWidth - 1, outputHeight - 1));

                var textureRepeatCount = (X: uvRange.Max.X - uvRange.Min.X, Y: uvRange.Max.Y - uvRange.Min.Y);
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

        private (Point Min, Point Max) CalcUVRange(IEnumerable<UVEdge> mesh)
        {
            var uvBound = mesh.Aggregate((Min: new PointF(float.MaxValue, float.MaxValue), Max: new PointF(float.MinValue, float.MinValue)), (acm, elm) => (
                    new PointF(Math.Min(elm.UV.Min(p => p.X), acm.Min.X), Math.Min(elm.UV.Min(p => p.Y), acm.Min.Y)),
                    new PointF(Math.Max(elm.UV.Max(p => p.X), acm.Max.X), Math.Max(elm.UV.Max(p => p.Y), acm.Max.Y))
                ));

            int Round(float value) => (int)(value < 0 ? Math.Floor(value) : Math.Ceiling(value));
            var uvRange = (new Point(Round(uvBound.Min.X), Round(uvBound.Min.Y)), new Point(Round(uvBound.Max.X), Round(uvBound.Max.Y)));
            return uvRange;
        }
    }
}
