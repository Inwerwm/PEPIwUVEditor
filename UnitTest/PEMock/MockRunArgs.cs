using PEPlugin;

namespace UnitTest.PEMock
{
    class MockRunArgs : IPERunArgs
    {
        public bool IsBootup { get; }

        public string ModulePath { get; }

        public IPEPluginHost Host { get; }

        public MockRunArgs(IPEPluginHost host)
        {
            Host = host;
        }
    }
}
