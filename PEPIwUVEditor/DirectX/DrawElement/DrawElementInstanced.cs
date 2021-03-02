using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System.Linq;
using Buffer = SlimDX.Direct3D11.Buffer;

namespace IwUVEditor.DirectX.DrawElement
{
    abstract class DrawElementInstanced<TVertex, TInstanceData> : DrawElement<TVertex>, IDrawElement
        where TVertex : IDxVertex, new()
        where TInstanceData : IDxVertex, new()
    {
        protected Buffer InstanceBuffer { get; set; }

        protected DrawElementInstanced(Device device, EffectPass usingEffectPass, RasterizerState drawMode) : base(device, usingEffectPass, drawMode) { }

        protected override void Initialize()
        {
            base.Initialize();
            CreateInstanceBuffer();
        }

        public override void UpdateVertices()
        {
            CreateInstanceBuffer();
        }

        protected override void SetVertexBuffer()
        {
            Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, new TVertex().SizeInBytes, 0),
                new VertexBufferBinding(InstanceBuffer, new TInstanceData().SizeInBytes, 0)
            );
        }

        protected override void CreateVertexLayout()
        {
            VertexLayout?.Dispose();
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                new TVertex().VertexElements.Concat(new TInstanceData().VertexElements).ToArray()
            );
        }

        protected abstract TInstanceData[] CreateInstances();

        protected virtual void CreateInstanceBuffer()
        {
            InstanceBuffer?.Dispose();
            using (DataStream instanceStream = new DataStream(CreateInstances(), false, true))
                InstanceBuffer = new Buffer(
                    Device,
                    instanceStream,
                    new BufferDescription
                    (
                        (int)instanceStream.Length,
                        ResourceUsage.Dynamic,
                        BindFlags.VertexBuffer,
                        CpuAccessFlags.Write,
                        ResourceOptionFlags.None,
                        0
                    )
                );
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    InstanceBuffer?.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}
