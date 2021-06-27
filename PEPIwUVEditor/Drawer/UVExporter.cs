using PEPlugin.Pmx;
using SlimDX.Direct2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Export(string path)
        {

        }
    }
}
