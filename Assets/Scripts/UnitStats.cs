using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public string nameI;
    public int maxHP;
    private int currentHP;
    private int _currentHP;
    [HideInInspector] public bool isShielded = false;
    [SerializeField] private int basicAttack;
    [SerializeField] private int specialAttackPercentage;
    [SerializeField] private int regenHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        if (currentHP != _currentHP && OnHPChange != null)
        {
            _currentHP = currentHP;
            OnHPChange(_currentHP);
        }
    }

    public void Attack (UnitStats other, int multiplication)
    {
        other.Attacked(basicAttack * multiplication);
        print(nameI + " Attack " + other.nameI + " by " + (basicAttack * multiplication));
    }

    public void Attacked(int attackPoint)
    {
        int afterShielded = attackPoint;
        if (isShielded == true)
        {
            afterShielded -= Mathf.FloorToInt(30 / 100f * attackPoint);
            print(nameI + " Have a shield! ");
            isShielded = false;
        }
        if (currentHP - afterShielded <= 0)
        {
            // DIE!
            print(nameI + " DIED!");
            currentHP = 0;
            return ;
        }
        currentHP -= afterShielded;

    }

    public void Heal(int percentage, int multiplication)
    {
        float a = percentage / 100f * maxHP * (multiplication / 2f);
        int addHealth = Mathf.FloorToInt(a);
        if (currentHP + addHealth >= maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += addHealth;
        }
        print(nameI + "Being Healed by " + addHealth + " HP");
    }

    public void SpecialAttack(UnitStats other)
    {
        other.Attacked(Mathf.FloorToInt(other.maxHP * specialAttackPercentage / 100f));
        print(nameI + "  launch special attack to" + other.nameI + " by " + (other.maxHP * specialAttackPercentage / 100f));
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public delegate void OnHPChangeDelegate(int newHPVal);
    public event OnHPChangeDelegate OnHPChange;
}
