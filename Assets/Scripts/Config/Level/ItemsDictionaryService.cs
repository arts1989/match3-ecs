using System;
using UnityEngine;

namespace Config.Level
{
    [Serializable]
    public class ItemsDictionaryService<TKey, TValue>
    {
        [SerializeField] private TKey _key;
        [SerializeField] private TValue _value;
        
        public TKey Key => _key;
        public TValue Value => _value;
    }
}