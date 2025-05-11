using System;
using System.Collections.Generic;
using System.Linq;

namespace Misc
{
    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        public List<SerializableKeyValuePair<TKey, TValue>> pairs = new List<SerializableKeyValuePair<TKey, TValue>>();

        public void FromDictionary(Dictionary<TKey, TValue> dictionary)
        {
            pairs.Clear();
            foreach (var kvp in dictionary)
            {
                pairs.Add(new SerializableKeyValuePair<TKey, TValue> { Key = kvp.Key, Value = kvp.Value });
            }
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            var dictionary = new Dictionary<TKey, TValue>();
            foreach (var pair in pairs)
            {
                dictionary[pair.Key] = pair.Value;
            }

            return dictionary;
        }

        public void Add(TKey key, TValue value)
        {
            pairs.Add(new SerializableKeyValuePair<TKey, TValue> { Key = key, Value = value });
        }

        public bool ContainsKey(TKey key)
        {
            return pairs.Any(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key));
        }

        public TValue this[TKey key]
        {
            get
            {
                var pair = pairs.FirstOrDefault(p => EqualityComparer<TKey>.Default.Equals(p.Key, key));
                if (pair == null) throw new KeyNotFoundException("The given key was not present in the dictionary.");
                return pair.Value;
            }
            set
            {
                var pair = pairs.FirstOrDefault(p => EqualityComparer<TKey>.Default.Equals(p.Key, key));
                if (pair == null)
                {
                    pairs.Add(new SerializableKeyValuePair<TKey, TValue> { Key = key, Value = value });
                }
                else
                {
                    pair.Value = value;
                }
            }
        }
        
        public void Clear()
        {
            pairs.Clear();
        }
    }
}