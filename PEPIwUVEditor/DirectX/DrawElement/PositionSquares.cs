using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class PositionSquares : IDrawElement
    {
        private bool disposedValue;

        Device Device { get; }
        EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        InputLayout VertexLayout { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }
        Buffer InstanceBuffer { get; set; }

        Material SourceMaterial { get; }

        public PositionSquares(Device device, Effect effect, RasterizerState drawMode, Material material)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("MainTechnique").GetPassByName("DrawVertexColorPass");
            DrawMode = drawMode;
            SourceMaterial = material;

            if (SourceMaterial is null)
                return;

            CreateVertexLayout();

            CreateVertexBuffer();
            CreateIndexBuffer();
            CreateInstanceBuffer();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        private void CreateVertexLayout()
        {
            throw new NotImplementedException();
        }

        private void CreateVertexBuffer()
        {
            throw new NotImplementedException();
        }

        private void CreateIndexBuffer()
        {
            throw new NotImplementedException();
        }

        private void CreateInstanceBuffer()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~PositionSquare()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
