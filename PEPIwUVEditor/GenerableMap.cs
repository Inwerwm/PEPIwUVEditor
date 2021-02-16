using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwUVEditor
{
    /// <summary>
    /// 指定キーに対応する値が存在しない場合は生成する連想配列
    /// </summary>
    class GenerableMap<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private Func<TKey, TValue> DefaultGenerator { get; }

        public GenerableMap(Func<TKey, TValue> defaultGenerator)
        {
            DefaultGenerator = defaultGenerator;
        }

        public new TValue this[TKey key]
        {
            get
            {
                TValue value;
                return TryGetValue(key, out value) ? value : AddAndReturn(key, DefaultGenerator(key));
            }
        }

        private TValue AddAndReturn(TKey key, TValue value)
        {
            Add(key, value);
            return base[key];
        }
    }
}
