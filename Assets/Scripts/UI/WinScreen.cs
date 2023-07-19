using Leopotam.Ecs;
using System;
using TMPro;
using UnityEngine.UI;

namespace Match3
{
    public class WinScreen : Screen
    {
        public TextMeshProUGUI TotalMoneyLabel;
        public TextMeshProUGUI LevelNumberLabel;
        public Button NextButton;
        public EcsWorld World;

        private void Awake()
        {
            NextButton.onClick.AddListener(OnNextButtonClick);
        }

        private void OnNextButtonClick()
        {
            //World.NewEntity(out LoadNextLevelEvent )
        }

        public void Init()
        {

        }
    }
}
