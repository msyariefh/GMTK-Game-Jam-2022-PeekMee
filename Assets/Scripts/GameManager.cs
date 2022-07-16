using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Item Stats
    public int potionPercentage; // Increase Player HP by percentage
    public int poisonPercentage; // Decrease Player HP by percentage continous up to 3 turns

    [HideInInspector] public InGameState stateInGame;
    public enum InGameState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        LOST,
        WON
    }

    public static GameManager Instance;

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

    private void Start()
    {
        stateInGame = InGameState.START;
    }
}
