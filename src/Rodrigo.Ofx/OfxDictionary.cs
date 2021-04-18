using System.Collections;
using System.Collections.Generic;

namespace Rodrigo.Ofx
{
    public class OfxDictionary<TKey, TValue> : IEnumerable
    {
        private Dictionary<TKey, TValue> Values { get; set; }

        public OfxDictionary()
        {
            Values = new Dictionary<TKey, TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            Values.Add(key, value);
        }

        public IEnumerator GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public dynamic this[TKey key]
        {
            get
            {
                return Values[key];
            }
        }
    }
}
