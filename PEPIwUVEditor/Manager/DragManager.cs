using SlimDX;

namespace IwUVEditor.Manager
{
    class DragManager
    {
        bool state;

        public bool IsStartingJust { get; private set; }
        /// <summary>
        /// ドラッグ中か
        /// </summary>
        public bool IsDragging { get; private set; }
        /// <summary>
        /// ドラッグが終了したか
        /// </summary>
        public bool IsEndingJust { get; private set; }

        public Vector2 Start { get; private set; }
        public Vector2 Current { get; private set; }
        public Vector2 End { get; private set; }

        /// <summary>
        /// <para>ドラッグ情報の監視メソッド</para>
        /// <para>マウスイベントの発生時に毎回このメソッドを呼び出す</para>
        /// </summary>
        /// <param name="e"></param>
        /// <param name="clickState"></param>
        public void ReadState(Vector2 currentMousePosition, bool clickState)
        {
            // false => true : ドラッグ開始
            if (!this.state && clickState)
            {
                Start = currentMousePosition;
                IsStartingJust = true;
                IsDragging = true;
                IsEndingJust = false;
            }
            else
                IsStartingJust = false;

            // true => false : ドラッグ終了
            if (this.state && !clickState)
            {
                End = currentMousePosition;
                IsDragging = false;
                IsEndingJust = true;
            }
            else
            {
                IsEndingJust = false;
            }

            Current = currentMousePosition;
            state = clickState;
        }

        /// <summary>
        /// 未ドラッグ状態に変更
        /// </summary>
        public void Reset()
        {
            IsEndingJust = false;
        }
    }
}
