using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript: MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
