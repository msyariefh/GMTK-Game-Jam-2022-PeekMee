using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private UnitStats stats;

    private void Start()
    {
        stats = GetComponent<UnitStats>();
    }

    public void Attack(UnitStats enemyStats, Roller.Items item, int multiplier)
    {
        if (item == Roller.Items.SEVEN && multiplier == 3)
        {
            // Auto Win!
            return;
        }
        switch (item)
        {
            case Roller.Items.SWORD:
                if (multiplier == 1)
                {
                    // Do normal basic attack
                }
                else
                {
                    // Do double attack!
                }
                break;
            case Roller.Items.SHIELD:
                if ( multiplier == 1)
                {
                    // Do half absorbtion damage
                }
                else
                {
                    // Do full absorbtion damage
                }
                break;
            case Roller.Items.POTION:
                if (multiplier == 1)
                {
                    // Heal half of the effect
                }
                else
                {
                    // Heal full effect
                }
                break;
        }
    }
}
