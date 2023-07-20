using UnityEngine;

namespace Match3
{
    public class Screen : MonoBehaviour
    {
        public virtual void Show(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
