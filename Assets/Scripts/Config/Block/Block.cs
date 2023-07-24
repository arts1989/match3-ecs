using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
    public class Block : ScriptableObject
    {
        public int points = 5;
        public GameObject sprite;

        [SerializeField] public BlockTypes type;
    }
}