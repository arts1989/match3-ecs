using UnityEngine;
using UnityEngine.UI;

public class UIStars : MonoBehaviour
{
    [SerializeField] private Image[] _emptyIconStar;
    [SerializeField] private Sprite _fillIconStar;

    public void Init(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _emptyIconStar[i].overrideSprite = _fillIconStar;
        }
    }
}
