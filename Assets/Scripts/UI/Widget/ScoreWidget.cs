using UnityEngine.UI;

namespace Match3
{
    public class ScoreWidget : Screen
    {
        public Text text;

        public void SetMovesLeftText(int value)
        {
           text.text = value.ToString(); //"Ходов осталось: " + 
        }
    }
}