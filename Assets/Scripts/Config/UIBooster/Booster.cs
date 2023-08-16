using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
    public class Booster : ScriptableObject
    {
        public GameObject prefab;

        [SerializeField] public BoosterTypes boosterType;
     }
}