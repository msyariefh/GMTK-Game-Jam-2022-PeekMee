using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Item Stats
    public int potionPercentage; // Increase Player HP by percentage
    public int poisonPercentage; // Decrease Player HP by percentage continous up to 3 turns

    public GameObject playerPrefab;
    public GameObject EnemyPrefab;

    private UnitStats playerStats;
    private UnitStats enemyStats;

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
        //playerStats = playerPrefab.GetComponent<UnitStats>();
        //enemyStats = EnemyPrefab.GetComponent<UnitStats>();
        SetUpBattle();
    }

    private void Update()
    {
        if (isAttacking == true) return;
        Battle();
    }

    private void SetUpBattle()
    {
        // Doing something when starting the game battle


        // Move to Battle Phase
        stateInGame = InGameState.PLAYERTURN;
    }

    public void Battle()
    {
        Roller roll = FindObjectOfType<Roller>();
        roll.Rolls();

        Roller.Items[] gacha = roll.jackpot;
        var duplicates = gacha.GroupBy(x => x).Where(y => y.Count() > 1).Select(y => y.Key);
        

        int multiplier = 0;
        if (duplicates.Count() > 0)
        {
            print(duplicates.ToList()[0]);
            foreach (Roller.Items i in gacha)
            {
                if(i == duplicates.ToList()[0]) { multiplier++; }
            }
        }
        print(multiplier);

        switch (stateInGame)
        {
            case InGameState.PLAYERTURN:
                break;
            case InGameState.ENEMYTURN:
                break;
            case InGameState.START:
                break;
            case InGameState.LOST:
                break;
            case InGameState.WON:
                break;
        }
        isAttacking = true;
    }

    public void TestRoll()
    {
        isAttacking = false;
    }
}
