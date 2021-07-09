using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    class CommandTransaction : IEditorCommand
    {
        public bool IsDestructive => Commands.Any(cmd => cmd.IsDestructive);

        private IEnumerable<IEditorCommand> Commands { get; }

        public CommandTransaction(IEnumerable<IEditorCommand> commands)
        {
            Commands = commands;
        }

        public void Do()
        {
            foreach (var cmd in Commands)
            {
                cmd.Do();
            }
        }

        public void Undo()
        {
            foreach (var cmd in Commands.Reverse())
            {
                cmd.Undo();
            }
        }
    }
}
