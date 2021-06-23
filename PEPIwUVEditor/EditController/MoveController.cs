using IwUVEditor.DirectX;
using IwUVEditor.StateContainer;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.EditController
{
    class MoveController : EditController
    {
        public override Vector3 Center { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MoveController(UVViewDrawProcess process) : base(process)
        {
        }

        public override void PrepareDrawing()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyModeChange(SelectionMode mode)
        {
            throw new NotImplementedException();
        }

        protected override SelectionMode CalcMode(InputStates input)
        {
            throw new NotImplementedException();
        }
    }
}
