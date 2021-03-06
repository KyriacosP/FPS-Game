using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
		{
            if(gameIsPaused)
			{
                Resume();
			}
            else
			{
                Pause();
			}
		}
    }

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
		gameIsPaused = false;
	}

	private void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.None;
		gameIsPaused = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
