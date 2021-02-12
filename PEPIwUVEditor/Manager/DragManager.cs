using SlimDX.RawInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Manager
{
    class DragManager
    {
        bool state;

        /// <summary>
        /// ドラッグ中か
        /// </summary>
        public bool IsDragging { get; private set; }
        /// <summary>
        /// ドラッグの始端-終端情報を持っているか
        /// </summary>
        public bool HasValue { get; private set; }

        public Point Start { get; private set; }
        public Point End { get; private set; }

        /// <summary>
        /// <para>ドラッグ情報の監視メソッド</para>
        /// <para>マウスイベントの発生時に毎回このメソッドを呼び出す</para>
        /// </summary>
        /// <param name="e"></param>
        /// <param name="clickState"></param>
        public void ReadState(MouseInputEventArgs e, bool clickState)
        {
            // false => true : ドラッグ開始
            if(!this.state && clickState)
            {
                Start = new Point(e.X, e.Y);
                IsDragging = true;
                HasValue = false;
            }

            // true => false : ドラッグ終了
            if(this.state && !clickState)
            {
                End = new Point(e.X, e.Y);
                IsDragging = false;
                HasValue = true;
            }

            state = clickState;
        }

        /// <summary>
        /// 値未所持状態に変更
        /// </summary>
        public void Reset()
        {
            HasValue = false;
        }
    }
}
