using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace IwUVEditor.Drawer
{
    class UVExporter
    {
        public int ImageSize { get; }
        public IList<IPXVertex> Vertices { get; }
        public IList<IPXFace> Faces { get; }

        public UVExporter(int imageSize, IList<IPXVertex> vertices, IList<IPXFace> faces)
        {
            ImageSize = imageSize;
            Vertices = vertices;
            Faces = faces;
        }

        public void Export(string texturePath, string exportPath, bool drawTexture)
        {
            using (var texture = !File.Exists(texturePath) ? null : IsTGA(texturePath) ? new TGASharpLib.TGA(texturePath).ToBitmap() : new Bitmap(texturePath))
            {
                decimal textureWidth = texture?.Width ?? ImageSize;
                decimal textureHeight = texture?.Height ?? ImageSize;

                var hRatio = (int)Math.Round(textureHeight / textureWidth, MidpointRounding.AwayFromZero);
                int width = ImageSize;
                int height = ImageSize * hRatio;
                var mesh = Faces.AsParallel().SelectMany(UVEdge.FromFace).Select(e => e.Mul(width, height));

                using (var bmp = new Bitmap(width, height))
                using (var graph = Graphics.FromImage(bmp))
                using (var pen = new Pen(Color.Black) { Width = 1 })
                {
                    graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    if (drawTexture)
                        graph.DrawImage(texture, 0, 0, width, height);

                    foreach (var edge in mesh)
                    {
                        graph.DrawLine(pen, edge.UV[0], edge.UV[1]);
                    }

                    bmp.Save(exportPath, ImageFormat.Png);
                }
            }
        }

        private static bool IsTGA(string texturePath)
        {
            return Path.GetExtension(texturePath).ToLower() == ".tga";
        }
    }
}
