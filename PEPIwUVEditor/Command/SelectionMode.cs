using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor.Command
{
    /// <summary>
    /// 選択範囲設定モード
    /// </summary>
    enum SelectionMode
    {
        /// <summary>
        /// 新たに設定する
        /// </summary>
        Create,
        /// <summary>
        /// 加える
        /// </summary>
        Union,
        /// <summary>
        /// 除去する
        /// </summary>
        Difference
    }
}
