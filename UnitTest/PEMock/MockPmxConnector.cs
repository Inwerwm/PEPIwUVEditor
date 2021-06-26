using PEPlugin.Pmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PEMock
{
    class MockPmxConnector : IPXPmxConnector
    {
        public string CurrentPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private IPXPmx Model { get; }

        public MockPmxConnector(IPXPmx model)
        {
            Model = model;
        }

        public IPXPmx GetCurrentState() => Model;

        public void LockUndo()
        {
            throw new NotImplementedException();
        }

        public void UnlockUndo()
        {
            throw new NotImplementedException();
        }

        public void Update(IPXPmx px)
        {
            throw new NotImplementedException();
        }

        public void Update(IPXPmx px, PmxUpdateObject obj, int index)
        {
            throw new NotImplementedException();
        }

        public void Update(IPXPmx px, PmxUpdateObject obj, int[] indices)
        {
            throw new NotImplementedException();
        }
    }
}
