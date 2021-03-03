using SlimDX;

namespace IwUVEditor.Manager
{
    class DragManager
    {
        bool state;

        /// <summary>
        /// ちょうどドラッグが開始した時点であるか
        /// </summary>
        public bool IsStartingJust { get; private set; }
        /// <summary>
        /// ドラッグ中か
        /// </summary>
        public bool IsDragging { get; private set; }
        /// <summary>
        /// ちょうどドラッグが終了した時点であるか
        /// </summary>
        public bool IsEndingJust { get; private set; }

        /// <summary>
        /// ドラッグ開始座標
        /// </summary>
        public Vector2 Start { get; private set; }
        /// <summary>
        /// 現在のマウス座標
        /// </summary>
        public Vector2 Current { get; private set; }
        /// <summary>
        /// ドラッグ終了座標
        /// </summary>
        public Vector2 End { get; private set; }

        private Vector2 Previous { get; set; }
        /// <summary>
        /// 直前時点からの移動量
        /// </summary>
        public Vector2 Offset => Current - Previous;
        /// <summary>
        /// 開始地点からの移動量
        /// </summary>
        public Vector2 Translation => Current - Start;

        /// <summary>
        /// <para>ドラッグ情報の監視メソッド</para>
        /// <para>マウスイベントの発生時に毎回このメソッドを呼び出す</para>
        /// </summary>
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

            Previous = Current;
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
