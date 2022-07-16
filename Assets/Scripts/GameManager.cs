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


    private void Awake()
    {
        stateInGame = InGameState.START;
        playerStats = playerPrefab.GetComponent<UnitStats>();
        enemyStats = EnemyPrefab.GetComponent<UnitStats>();
        SetUpBattle();
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

    public void Battle()
    {
        isAttacking = true;
        Roller roll = FindObjectOfType<Roller>();
        roll.Rolls();

        Roller.Items[] gacha = roll.jackpot;
        var duplicates = gacha.GroupBy(x => x).Where(y => y.Count() > 1).Select(y => y.Key);
        
        if (duplicates.Count() < 1) 
        {
            enemyStats.Attack(playerStats, 1);
            print("Player HP: " + playerStats.GetCurrentHP());
            print("Enemy HP: " + enemyStats.GetCurrentHP());
            return; 
        }
        int multiplier = 0;
        foreach (Roller.Items i in gacha)
        {
            if (i == duplicates.ToList()[0]) { multiplier++; }
        }

        if (multiplier > 0)
        {
            // Results from gacha
            switch (duplicates.ToList()[0])
            {
                case Roller.Items.MAGIC:
                    playerStats.SpecialAttack(enemyStats);
                    break;
                case Roller.Items.POISON:
                    if (multiplier == 2) enemyStats.Attack(playerStats, 2);
                    else if (multiplier == 3) enemyStats.SpecialAttack(playerStats);
                    break;
                case Roller.Items.POTION:
                    playerStats.Heal(potionPercentage, multiplier);
                    break;
                case Roller.Items.SEVEN:
                    if (multiplier == 2) playerStats.Attack(enemyStats, 2);
                    else if (multiplier == 3) playerStats.SpecialAttack(enemyStats);
                    break;
                case Roller.Items.SHIELD:
                    playerStats.isShielded = true;
                    break;
                case Roller.Items.SWORD:
                    if (multiplier == 2) playerStats.Attack(enemyStats, 1);
                    else if (multiplier == 3) playerStats.Attack(enemyStats, 3);
                    break;
            }
        }
        enemyStats.Attack(playerStats, 1);
        print("Player HP: " + playerStats.GetCurrentHP());
        print("Enemy HP: " + enemyStats.GetCurrentHP());
        isAttacking = false;
    }

}
