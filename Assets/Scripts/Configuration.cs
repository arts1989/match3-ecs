using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
   
    public class Configuration : ScriptableObject
    {
        public int minChainLenght = 3;
        
        public List<Level> levels = new List<Level>();
        public List<Block> blocks = new List<Block>();

        public Vector2 offset;
    }
}