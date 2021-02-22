using IwUVEditor.Manager;
using SlimDX;
using SlimDX.RawInput;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IwUVEditor.StateContainer
{
    class InputStates
    {
        /// <summary>
        /// <para>入力を受け付けているか</para>
        /// <para>マウスカーソルが描画画面内に存在する場合受け付ける</para>
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// スクリーンの大きさ
        /// </summary>
        public (int X, int Y) ScreenSize { get; set; }
        /// <summary>
        /// マウスのスクリーン座標
        /// </summary>
        public Vector2 MousePos { get; set; }
        /// <summary>
        /// マウスの移動量
        /// </summary>
        public (int X, int Y) MouseOffset { get; private set; } = (0, 0);
        /// <summary>
        /// <para>マウス左ボタンのドラッグ状態</para>
        /// <para>座標情報はワールド座標</para>
        /// </summary>
        public DragManager MouseLeft { get; } = new DragManager();
        public Dictionary<MouseButtons, bool> IsClicking { get; } = new Dictionary<MouseButtons, bool>
        {
            { MouseButtons.Left, false },
            { MouseButtons.Middle, false },
            { MouseButtons.Right, false },
        };
        public (bool IsScrolling, int Delta) Wheel { get; private set; }

        public Dictionary<Keys, bool> IsPress { get; } = new Dictionary<Keys, bool>
        {
            { Keys.ShiftKey, false },
            { Keys.ControlKey, false }
        };

        public void ReadMouseInput(MouseInputEventArgs e, Func<Vector2, Vector2> ScreenPosToWorldPos)
        {
            // 移動量の読取り
            MouseOffset = (e.X, e.Y);
            
            // 左中右クリックの上下の読取り
            switch (e.ButtonFlags)
            {
                case MouseButtonFlags.MiddleUp:
                    IsClicking[MouseButtons.Middle] = false;
                    break;
                case MouseButtonFlags.MiddleDown:
                    IsClicking[MouseButtons.Middle] = true;
                    break;
                case MouseButtonFlags.RightUp:
                    IsClicking[MouseButtons.Right] = false;
                    break;
                case MouseButtonFlags.RightDown:
                    IsClicking[MouseButtons.Right] = true;
                    break;
                case MouseButtonFlags.LeftUp:
                    IsClicking[MouseButtons.Left] = false;
                    break;
                case MouseButtonFlags.LeftDown:
                    IsClicking[MouseButtons.Left] = true;
                    break;
                default:
                    break;
            }

            // ホイールの読取り
            Wheel = e.ButtonFlags == MouseButtonFlags.MouseWheel ? (true, e.WheelDelta) : (false, 0);

            // ドラッグ状態の読取り
            MouseLeft.ReadState(ScreenPosToWorldPos(MousePos), IsClicking[MouseButtons.Left]);
        }
    }
}
