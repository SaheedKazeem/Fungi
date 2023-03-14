using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject pauseUI;

    public enum GameState { menu, play }

    public GameState gameState;
    public bool pause;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            if (pause == true)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
        pause = true;
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pause = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void startButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void replayButton()
    {
        SceneManager.LoadScene(1);
    }
}
