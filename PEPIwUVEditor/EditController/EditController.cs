using IwUVEditor.StateContainer;
using System;

namespace IwUVEditor.EditController
{
    abstract class EditController
    {
        public SelectionMode CurrentMode { get; protected set; }
        protected DirectX.UVViewDrawProcess Process { get; }

        protected EditController(DirectX.UVViewDrawProcess process)
        {
            Process = process;
        }

        /// <summary>
        /// 描画準備
        /// </summary>
        public abstract void PrepareDrawing();
        /// <summary>
        /// マウス入力読み込み
        /// </summary>
        /// <param name="input">マウス情報</param>
        /// <param name="editFunction">編集方法</param>
        public virtual void ReadInput(InputStates input, Action<InputStates> editFunction)
        {
            var modeTmp = CurrentMode;

            if (!(input.MouseLeft.IsDragging || input.MouseLeft.IsEndingJust))
                CurrentMode = CalcMode(input);

            Execute(CurrentMode, editFunction);

            // ドラッグ終了時点の場合はモードの再読込を行わないと
            // 次のマウス入力までモードが持続してしまう
            if (input.MouseLeft.IsEndingJust)
                CurrentMode = CalcMode(input);

            if (modeTmp != CurrentMode)
                ApplyModeChange(CurrentMode);
        }

        protected abstract SelectionMode CalcMode(InputStates input);
        protected abstract void Execute(SelectionMode mode, Action<InputStates> editFunction);
        protected abstract void ApplyModeChange(SelectionMode mode);

        public enum SelectionMode
        {
            None,
            Center,
            X,
            Y,
        }
    }
}
