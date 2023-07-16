using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
   
    public class GameConfig : ScriptableObject
    {
        public int GemInlineToDestroy = 3;
        
        public List<Level> levels = new List<Level>();
        public List<Gem> gems = new List<Gem>();

        public Vector2 offset;
    }
}