using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
    public class Block : ScriptableObject
    {
        public int points = 5;
        public Sprite[] sprites;

        [SerializeField] public BlockTypes type;

        public int health = 0;
    }
}