using PEPlugin;

namespace UnitTest.PEMock
{
    class MockPluginHost : IPEPluginHost
    {
        public string Name { get; }

        public string Version { get; }

        public IPEBuilder Builder { get; }

        public IPEConnector Connector { get; }

        public MockPluginHost(IPEConnector connector)
        {
            Connector = connector;
        }
    }
}
