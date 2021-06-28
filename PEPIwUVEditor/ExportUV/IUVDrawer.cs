namespace IwUVEditor.ExportUV
{
    interface IUVDrawer
    {
        void Draw(UVMesh mesh, int imageSize, string texturePath, string exportPath, bool enableDrawBackground);
    }
}
