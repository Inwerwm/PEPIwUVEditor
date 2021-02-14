using IwUVEditor.Command;
using SlimDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    class Tools
    {
        CommandManager Commander { get;}

        public Tools(CommandManager commander)
        {
            Commander = commander;
        }

        public void RectangleSelect(Material target, Vector2 start, Vector2 end, SelectionMode mode)
        {
            var cmd = new CommandRectangleSelection(target)
            {
                StartPosition = start,
                EndPosition = end,
                Mode = mode,
            };

            Commander.Do(cmd);
        }
    }

    enum Tool
    {
        RectangleSelection,

    }
}
