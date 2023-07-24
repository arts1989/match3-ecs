using UnityEngine.UI;
using UnityEngine;

namespace Match3
{
    public class ScoreWidget : Screen
    {
        public Text textMovesLeft;
        public Text textPointsScored;

        public void SetMovesLeftText(int value)
        {
            Debug.Log("--------------------- " + value.ToString());
            textMovesLeft.text = "Ходов осталось: " +  value.ToString(); 
        }

        public void SetPointsScoredText(int value)
        {
            Debug.Log("--------------------- " + value.ToString());
            textPointsScored.text = "Очков набрано: " + value.ToString();
        }
    }
}