using UnityEngine;
using UnityEngine.Tilemaps;

namespace Match3
{
    [CreateAssetMenu]
    public class Underlay : ScriptableObject
    {
        public int points = 5;
        public Tile[] tiles;
    }
}