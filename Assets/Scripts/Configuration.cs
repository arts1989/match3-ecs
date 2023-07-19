using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
   
    public class Configuration : ScriptableObject
    {
        public int GemInlineToDestroy = 3;
        
        public List<Level> levels = new List<Level>();
        public List<Block> gems = new List<Block>();

        public Vector2 offset;
    }
}