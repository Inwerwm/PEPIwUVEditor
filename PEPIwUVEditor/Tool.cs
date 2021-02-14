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
        Dictionary<Material, CommandManager> Commanders { get; set; }

        public Tools(IEnumerable<Material> materials)
        {
            Commanders = materials.ToDictionary(m => m, _ => new CommandManager());
        }

        public void RectangleSelect(Material target, Vector2 start, Vector2 end, SelectionMode mode, Action updateAction)
        {
            var cmd = new CommandRectangleSelection(target, start, end, mode, updateAction);

            Commanders[target].Do(cmd);
        }

        public void Undo(Material target)
        {
            Commanders[target].Undo();
        }
        public void Redo(Material target)
        {
            Commanders[target].Redo();
        }
    }

    enum Tool
    {
        RectangleSelection,

    }
}
