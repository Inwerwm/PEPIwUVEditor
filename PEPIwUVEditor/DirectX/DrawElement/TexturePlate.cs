using IwUVEditor.DirectX.Vertex;
using SlimDX;
using SlimDX.Direct3D11;
using System.Collections.Generic;
using System.Linq;
using Device = SlimDX.Direct3D11.Device;

namespace IwUVEditor.DirectX.DrawElement
{
    class TexturePlate : DrawElementInstanced<VertexStruct, InstanceOffset>, IDrawElement
    {
        private float peripheryPlateAlpha;
        private int radius;

        int InstanceCount { get; set; }

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
                UpdateVertices();
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
                UpdateVertices();
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

                UpdateVertices();
            }
        }

        public TexturePlate(Device device, Effect effect, RasterizerState drawMode) :
            base(device, effect.GetTechniqueByName("TexturePlatesTechnique").GetPassByName("DrawTexturePlatesPass"), drawMode)
        {
            Initialize();
        }

        protected override void DrawToDevice()
        {
            Device.ImmediateContext.DrawIndexedInstanced(3, InstanceCount, 0, 0, 0);
            Device.ImmediateContext.DrawIndexedInstanced(3, InstanceCount, 3, 0, 0);
        }

        protected override VertexStruct[] CreateVertices() => new[]
        {
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

        protected override uint[] CreateIndices() => new uint[]
        {
            2, 1, 0,
            1, 2, 3
        };

        protected override InstanceOffset[] CreateInstances()
        {
            var instances = new List<InstanceOffset>();

            // 中心
            instances.Add
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

                instances.Add
                (
                    new InstanceOffset()
                    {
                        Offset = Matrix.Translation(i, j, 0),
                        AlphaRatio = PeripheryPlateAlpha
                    }
                );

                if (j != 0)
                    instances.Add
                    (
                        new InstanceOffset
                        {
                            Offset = Matrix.Translation(i, -j, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                    );

                if (i != 0)
                    instances.Add
                    (
                        new InstanceOffset
                        {
                            Offset = Matrix.Translation(-i, j, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                    );

                if (i != 0 && j != 0)
                    instances.Add
                    (
                        new InstanceOffset
                        {
                            Offset = Matrix.Translation(-i, -j, 0),
                            AlphaRatio = PeripheryPlateAlpha
                        }
                    );
            }

            InstanceCount = instances.Count;

            return instances.ToArray();
        }
    }
}
