﻿using System;
using UnityEngine;

namespace Match3
{
    [Serializable]
    public class SerializeItem<TKey, TValue>
    {
        [SerializeField] private TKey _key;
        [SerializeField] private TValue _value;
        
        public TKey Key => _key;
        public TValue Value => _value;
    }
}