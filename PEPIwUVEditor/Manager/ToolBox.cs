﻿using IwUVEditor.DirectX;
using SlimDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Tool
{
    class ToolBox
    {
        internal SlimDX.Direct3D11.Device Device { get; set; }
        UVViewDrawProcess Process { get; set; }

        Dictionary<Type, IEditTool> ToolOf { get; }

        public ToolBox()
        {
            ToolOf = new Dictionary<Type, IEditTool>
            {
                { typeof(RectangleSelection), null}
            };
        }

        T CallTool<T>(Func<T> constructor, UVViewDrawProcess process) where T : IEditTool
        {
            if (ToolOf[typeof(T)] == null || Process != process)
            {
                ToolOf[typeof(T)] = constructor();
                Process = process;
            }

            return (T)ToolOf[typeof(T)];
        }

        public RectangleSelection RectangleSelection(UVViewDrawProcess process)
        {
            return CallTool(() => new RectangleSelection(Device, process.Effect, process.Rasterize.Solid, process.PositionSquares), process);
        }
    }
}
