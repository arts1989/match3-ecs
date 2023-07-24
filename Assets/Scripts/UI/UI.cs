using UnityEngine;

namespace Match3
{
    public class UI : MonoBehaviour
    {
        // modal window popups
        public WinScreen WinScreen;
        public LoseScreen LoseScreen;

        // game widgets on scene
        public ScoreWidget scoreWidget;
        public BoosterWidget boosterWidget; 
    }
}