using PEPlugin;
using PEPlugin.Form;
using PEPlugin.Pmd;
using PEPlugin.Pmx;
using PEPlugin.View;

namespace UnitTest.PEMock
{
    class MockConnector : IPEConnector
    {
        public IPXPmxConnector Pmx { get; }

        public IPEPmdConnector Pmd { get; }

        public IPEFormConnector Form { get; }

        public IPEViewConnector View { get; }

        public IPESystemConnector System { get; }

        public MockConnector(IPXPmxConnector pmx)
        {
            Pmx = pmx;
        }
    }
}
