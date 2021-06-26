using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    delegate void Vector3EventHandler(Vector3 value);

    interface IEditParameter
    {
        event Vector3EventHandler RotationCenterChanged;
        event Vector3EventHandler ScaleCenterChanged;

        Vector3 MoveOffset { get; set; }

        Vector3 RotationCenter { get; set; }
        float RotationAngle { get; set; }

        Vector3 ScaleCenter { get; set; }
        Vector3 ScaleRatio { get; set; }
    }
}
