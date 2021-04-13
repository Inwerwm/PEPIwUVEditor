using IwUVEditor.Subform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Log
{
    static class DebugLog
    {
        private static FormDebugLog Form { get; } = new FormDebugLog();

        public static int Count { get; set; }

        public static void Show() => Form.Show();
        public static void Hide() => Form.Hide();

        public static void Write(string log) => Form.Write(log);
        public static void Append(string log) => Form.Append(log);
        public static void Clear() => Form.Clear();
    }
}
