using System.Drawing;

namespace IwUVEditor.ExportUV
{
    interface IUVDrawer
    {
        Point CalcTextureRepeatCount(UVMesh mesh);
        Point CalcUnitSize(int imageSize, int textureWidth, int textureHeight);
        void Draw(UVMesh mesh, int imageSize, string texturePath, string exportPath, bool enableDrawBackground);
    }
}
