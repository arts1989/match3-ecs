using UnityEngine;

namespace Match3
{
    [CreateAssetMenu]
    public class UIBooster : ScriptableObject
    {
        public GameObject prefab;

        [SerializeField] public UIBoosterTypes boosterType;
     }
}