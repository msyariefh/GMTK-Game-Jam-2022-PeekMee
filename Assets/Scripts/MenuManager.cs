using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject pauseCanvas;
    public GameObject GameOverCanvas;
    public GameObject VictoryCanvas;

    public enum GameState
    {
        GAMEPLAY,
        GAMEOVER,
        MENU
    }
    public GameState state;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && state == GameState.MENU)
        {
            // Quit the application
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && state == GameState.GAMEPLAY)
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        //Start GAME
        state = GameState.GAMEPLAY;
        FindObjectOfType<AudioManager>().PlaySound("Menu", "Arena");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GameOver()
    {
        FindObjectOfType<AudioManager>().PlaySound("Arena", "Lose");
        GameOverCanvas.SetActive(true);

        //Game OVER
        state = GameState.GAMEOVER;
    }
    public void GameWin()
    {
        FindObjectOfType<AudioManager>().PlaySound("Arena", "Victory");
        VictoryCanvas.SetActive(true);
    }

    public void RetryGame()
    {
        //Retry
        FindObjectOfType<AudioManager>().PlaySound("Arena", "Arena");
        SceneManager.LoadScene(1);
        state = GameState.GAMEPLAY;
    }
    public void MenuGame()
    {
        state = GameState.MENU;
        FindObjectOfType<AudioManager>().PlaySound("Arena", "Menu");
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
