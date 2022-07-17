using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

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

        DontDestroyOnLoad(gameObject);
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
            // Pause
        }
    }

    public void StartGame()
    {
        //Start GAME
        state = GameState.GAMEPLAY;
    }
    public void GameOver()
    {
        //Game OVER
        state = GameState.GAMEOVER;
    }
    public void RetryGame()
    {
        //Retry
        state = GameState.GAMEPLAY;
    }
    public void MenuGame()
    {
        state = GameState.MENU;
    }
}
