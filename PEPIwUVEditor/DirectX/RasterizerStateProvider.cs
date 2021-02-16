using SlimDX.Direct3D11;

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
