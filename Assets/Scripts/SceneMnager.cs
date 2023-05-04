using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMnager : MonoBehaviour
{
    public void LoadAfterAnimation()
    {
        SceneManager.LoadScene(1);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
