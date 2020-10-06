using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

   public void Replay()
    {
        // (SceneManager.GetActiveScene().name  取得目前啟動場景的名稱
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void Quit()
    {
        Application.Quit();
    }

}
