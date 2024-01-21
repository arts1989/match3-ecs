using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBack : MonoBehaviour
{
    public int SceneNumber;

    public void Buttonback()
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
