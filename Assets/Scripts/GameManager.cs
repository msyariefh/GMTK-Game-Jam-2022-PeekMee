using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Item Stats
    public int potionPercentage; // Increase Player HP by percentage
    public int poisonPercentage; // Decrease Player HP by percentage continous up to 3 turns

    public GameObject playerPrefab;
    public GameObject EnemyPrefab;

    private UnitStats playerStats;
    private UnitStats enemyStats;


    [HideInInspector] public InGameState stateInGame;
    public enum InGameState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        LOST,
        WON
    }

    //public static GameManager Instance;

    //private void Awake()
    //{
    //    if (Instance == null) Instance = this;
    //    else
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    DontDestroyOnLoad(gameObject);
    //}

    private void Awake()
    {
        stateInGame = InGameState.START;
        playerStats = playerPrefab.GetComponent<UnitStats>();
        enemyStats = EnemyPrefab.GetComponent<UnitStats>();
        SetUpBattle();
    }

    private void SetUpBattle()
    {
        // Doing something when starting the game battle


        // Move to Battle Phase
        stateInGame = InGameState.PLAYERTURN;
    }

    public void Battle()
    {
        switch (stateInGame)
        {
            case InGameState.PLAYERTURN:
                break;
            case InGameState.ENEMYTURN:
                break;
        }
    }

    public void Attack()
    {

    }
}
