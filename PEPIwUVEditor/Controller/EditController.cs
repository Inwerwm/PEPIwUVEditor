using IwUVEditor.StateContainer;
using IwUVEditor.Tool;
using SlimDX;
using System;

namespace IwUVEditor.Controller
{
    abstract class EditController : IDisposable
    {
        public SelectionMode CurrentMode { get; protected set; }
        protected DirectX.UVViewDrawProcess Process { get; }
        protected IEditParameter Parameters { get; }
        public abstract Vector3 Center { get; set; }

        protected EditController(DirectX.UVViewDrawProcess process, IEditParameter parameters)
        {
            Process = process;
            Parameters = parameters;
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

            Execute(input, CurrentMode, editFunction);

            // ドラッグ終了時にモード変更が必要な位置にカーソルがあっても
            // 次のマウス入力までモードが持続してしまうので
            // モードを再計算する
            if (input.MouseLeft.IsEndingJust)
                CurrentMode = CalcMode(input);

            if (modeTmp != CurrentMode)
                ApplyModeChange(CurrentMode);
        }

        protected abstract SelectionMode CalcMode(InputStates input);
        protected virtual void Execute(InputStates input, SelectionMode mode, Action<InputStates> editFunction)
        {
            switch (mode)
            {
                case SelectionMode.Center:
                    MoveCenter(input);
                    break;
                default:
                    editFunction(input);
                    break;
            }
        }
        protected abstract void ApplyModeChange(SelectionMode mode);
        protected virtual void MoveCenter(InputStates input)
        {
            if (input.MouseLeft.IsDragging)
                Center += new Vector3(input.MouseLeft.Offset, 0);
        }

        public abstract void Dispose();

        public enum SelectionMode
        {
            None,
            Center,
            X,
            Y,
        }
    }
}
