using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
    public class Block : ScriptableObject
    {
        public int points = 5;
        public GameObject prefab;
        public Sprite[] sprites;

        [SerializeField] public BlockTypes type;
    }
}