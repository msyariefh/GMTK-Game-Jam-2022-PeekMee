using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text comment;
    // Item Stats
    public int potionPercentage; // Increase Player HP by percentage
    public int poisonPercentage; // Decrease Player HP by percentage continous up to 3 turns

    public GameObject playerPrefab;
    public GameObject EnemyPrefab;

    [HideInInspector] public UnitStats playerStats;
    [HideInInspector] public UnitStats enemyStats;

    

    private bool isAttacking = true;


    [HideInInspector] public InGameState stateInGame;
    public enum InGameState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        LOST,
        WON
    }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        stateInGame = InGameState.START;
        playerStats = playerPrefab.GetComponent<UnitStats>();
        enemyStats = EnemyPrefab.GetComponent<UnitStats>();
        SetUpBattle();
    }
    private void Start()
    {
        comment.text = "BATTLE START!";
    }

    private void Update()
    {
        //if (isAttacking == true) return;
        //Battle();
    }

    private void SetUpBattle()
    {
        // Doing something when starting the game battle


        // Move to Battle Phase
        stateInGame = InGameState.PLAYERTURN;
    }

    

}
