using IwUVEditor.DirectX.Vertex;
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
    class TexturePlate : IDrawElement
    {
        private bool disposedValue;
        private float peripheryPlateAlpha;
        private int radius;

        Device Device { get; }
        EffectPass UsingEffectPass { get; }
        public RasterizerState DrawMode { get; set; }

        InputLayout VertexLayout { get; set; }

        Buffer VertexBuffer { get; set; }
        Buffer IndexBuffer { get; set; }
        Buffer InstanceBuffer { get; set; }

        VertexStruct[] PlateVertices { get; }
        uint[] PlateIndices { get; }
        List<InstanceOffset> Instances { get; set; }

        /// <summary>
        /// <para>テクスチャ板の敷き詰め半径</para>
        /// <para>無駄な処理を省けるので透過率も変えるときは <c>InstanceParams</c> を使う</para>
        /// </summary>
        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                CreateInstanceBuffer();
            }
        }
        /// <summary>
        /// <para>中心以外の板の透過率</para>
        /// <para>無駄な処理を省けるので半径も変えるときは <c>InstanceParams</c> を使う</para>
        /// </summary>
        public float PeripheryPlateAlpha
        {
            get => peripheryPlateAlpha;

            set
            {
                peripheryPlateAlpha = value;
                CreateInstanceBuffer();
            }
        }
        /// <summary>
        /// <para>半径と透過率の一括指定</para>
        /// <para>無駄な処理を省けるので両方変えるときはこっちを使う</para>
        /// </summary>
        public (int Radius, float PeripheryPlateAlpha) InstanceParams
        {
            set
            {
                radius = value.Radius;
                peripheryPlateAlpha = value.PeripheryPlateAlpha;

                CreateInstanceBuffer();
            }
        }

        public TexturePlate(Device device, Effect effect, RasterizerState drawMode)
        {
            Device = device;
            UsingEffectPass = effect.GetTechniqueByName("TexturePlatesTechnique").GetPassByName("DrawTexturePlatesPass");
            DrawMode = drawMode;

            (PlateVertices, PlateIndices) = CreateConstantData();

            CreateVertexLayout();

            CreateVertexBuffer();
            CreateIndexBuffer();
            CreateInstanceBuffer();
        }

        public void Prepare()
        {
            // 描画方式を設定
            UsingEffectPass.Apply(Device.ImmediateContext);
            Device.ImmediateContext.Rasterizer.State = DrawMode;
            Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleStrip;

            // レイアウトを設定
            Device.ImmediateContext.InputAssembler.InputLayout = VertexLayout;

            // バッファを設定
            Device.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(VertexBuffer, VertexStruct.SizeInBytes, 0),
                new VertexBufferBinding(InstanceBuffer, InstanceOffset.SizeInBytes, 0)
            );
            Device.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);

            // 描画を設定
            Device.ImmediateContext.DrawIndexedInstanced(3, Instances.Count, 0, 0, 0);
            Device.ImmediateContext.DrawIndexedInstanced(3, Instances.Count, 3, 0, 0);
        }

        void CreateVertexLayout()
        {
            VertexLayout?.Dispose();
            VertexLayout = new InputLayout(
                Device,
                UsingEffectPass.Description.Signature,
                VertexStruct.VertexElements.Concat(InstanceOffset.VertexElements).ToArray()
            );
        }

        void CreateVertexBuffer()
        {
            VertexBuffer?.Dispose();
            using (var vertexStream = new DataStream(PlateVertices, true, true))
            {
                VertexBuffer = new Buffer(
                    Device,
                    vertexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)vertexStream.Length,
                        BindFlags = BindFlags.VertexBuffer,
                    }
                );
            }
        }

        void CreateIndexBuffer()
        {
            IndexBuffer?.Dispose();
            using (DataStream indexStream = new DataStream(PlateIndices, true, true))
            {
                IndexBuffer = new Buffer(
                    Device,
                    indexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)indexStream.Length,
                        BindFlags = BindFlags.IndexBuffer,
                    }
                );
            }
        }

        void CreateInstanceBuffer()
        {
            CreateInstances();

            InstanceBuffer?.Dispose();
            using (DataStream instanceStream = new DataStream(Instances.ToArray(), false, true))
                InstanceBuffer = new Buffer(
                    Device,
                    instanceStream,
                    new BufferDescription
                    (
                        InstanceOffset.SizeInBytes * Instances.Count,
                        ResourceUsage.Dynamic,
                        BindFlags.VertexBuffer,
                        CpuAccessFlags.Write,
                        ResourceOptionFlags.None,
                        0
                    )
                );
        }

        void CreateInstances()
        {
            Instances = new List<InstanceOffset>();

            // 中心
            Instances.Add
            (
                new InstanceOffset()
                {
                    Offset = Matrix.Translation(0, 0, 0),
                    AlphaRatio = 1
                }
            );

            // 周辺に四角形を放射配置するため、{0..n}と{0..n}の直積集合でループする
            foreach ((int i, int j) in Enumerable.Range(0, Radius + 1).SelectMany(i => Enumerable.Range(0, Radius + 1).Select(j => (i, j))).Skip(1))
            {

                Instances.Add
                (
                    new InstanceOffset()
                    {
                        Offset = Matrix.Translation(i, j, 0),
                        AlphaRatio = PeripheryPlateAlpha
                    }
                );

                if (j != 0)
                    Instances.Add
                    (
                        new InstanceOffset
                        {
                            Offset = Matrix.Translation(i, -j, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                    );

                if (i != 0)
                    Instances.Add
                    (
                        new InstanceOffset
                        {
                            Offset = Matrix.Translation(-i, j, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                    );

                if (i != 0 && j != 0)
                    Instances.Add
                    (
                        new InstanceOffset
                        {
                            Offset = Matrix.Translation(-i, -j, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                    );
            }
        }

        (VertexStruct[] vertices, uint[] indices) CreateConstantData()
        {
            var plateVertices = new[] {
                new VertexStruct
                {
                    Position = new Vector3(0, 1, 0),
                    Color = new Color4(1, 1, 1, 1),
                    TEXCOORD = new Vector2(0, 1)
                },
                new VertexStruct
                {
                    Position = new Vector3(1, 1, 0),
                    Color = new Color4(1, 1, 1, 1),
                    TEXCOORD = new Vector2(1, 1)
                },
                new VertexStruct
                {
                    Position = new Vector3(0, 0, 0),
                    Color = new Color4(1, 1, 1, 1),
                    TEXCOORD = new Vector2(0 ,0)
                },
                new VertexStruct
                {
                    Position = new Vector3(1, 0, 0),
                    Color = new Color4(1, 1, 1, 1),
                    TEXCOORD = new Vector2(1, 0)
                }
            };

            var plateIndices = new uint[] {
                2, 1, 0,
                1, 2, 3
            };

            return (plateVertices, plateIndices);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    VertexLayout?.Dispose();
                    VertexBuffer?.Dispose();
                    IndexBuffer?.Dispose();
                    InstanceBuffer?.Dispose();
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~TexturePlate()
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
