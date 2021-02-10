using DxManager;
using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.DirectX
{
    class RasterizerStateProvider
    {
        Device Device { get; }
        public CullMode CullMode { get; set; } = CullMode.Back;
        public bool IsMultisampleEnabled { get; set; } = true;

        public RasterizerStateProvider(Device device)
        {
            Device = device;
        }

        public RasterizerState Solid => RasterizerState.FromDescription(
            Device,
            new RasterizerStateDescription
            {
                CullMode = CullMode,
                FillMode = FillMode.Solid,
                IsMultisampleEnabled = true,
            }
            );

        public RasterizerState Wireframe => RasterizerState.FromDescription(
            Device,
            new RasterizerStateDescription
            {
                CullMode = CullMode,
                FillMode = FillMode.Wireframe,
            }
            );
    }
}
