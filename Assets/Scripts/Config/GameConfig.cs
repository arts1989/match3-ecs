using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
   
    public class GameConfig : ScriptableObject
    {
        public int GemInlineToDestroy = 3;
        
        public List<LevelConfig> levels = new List<LevelConfig>();
        public List<Gem> gems = new List<Gem>();

        public Vector2 offset;
    }
}