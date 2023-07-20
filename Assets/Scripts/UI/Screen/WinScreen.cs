using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Match3
{
    public class WinScreen : Screen
    {
        public Text text;

        public void SetCurrentLevelText(int value)
        {
            text.text = value.ToString();
        }

        public void onButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}