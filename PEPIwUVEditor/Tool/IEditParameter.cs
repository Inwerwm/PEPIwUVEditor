using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    interface IEditParameter
    {
        Vector3 MoveCenter { get; set; }
        Vector3 MoveOffset { get; set; }

        Vector3 RotationCenter { get; set; }
        float RotationAngle { get; set; }

        Vector3 ScaleCenter { get; set; }
        Vector3 ScaleRatio { get; set; }
    }
}
