using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int maxHP;
    private int currentHP;
    [SerializeField] private int basicAttack;
    [SerializeField] private int specialAttackPercentage;
    [SerializeField] private int regenHP;


    public void Attack (UnitStats other, int multiplication)
    {
        if (other.currentHP - basicAttack*multiplication <= 0)
        {
            // WON!
            return;
        }
        other.Attacked(basicAttack * multiplication);
    }

    public void Attacked(int attackPoint)
    {
        currentHP -= attackPoint;
    }

    public void Heal(int percentage)
    {
        int addHealth = Mathf.FloorToInt(percentage / 100 * currentHP);
        if (currentHP + addHealth >= maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += addHealth;
        }
    }

    public void SpecialAttack(UnitStats other)
    {
        other.Attacked(Mathf.FloorToInt(other.maxHP * specialAttackPercentage / 100));
    }
}
