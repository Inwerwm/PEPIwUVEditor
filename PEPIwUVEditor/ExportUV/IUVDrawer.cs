using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.ExportUV
{
    interface IUVDrawer
    {
        void Draw(UVMesh mesh, int imageSize, string texturePath, string exportPath, bool enableDrawBackground);
    }
}
