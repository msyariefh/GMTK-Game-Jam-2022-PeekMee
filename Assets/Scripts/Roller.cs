using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    public GameObject roll1;
    public GameObject roll2;
    public GameObject roll3;

    private SpriteRenderer roll1Sprite;
    private SpriteRenderer roll2Sprite;
    private SpriteRenderer roll3Sprite;

    public enum Items
    {
        SWORD,
        POTION,
        SHIELD,
        MAGIC,
        SEVEN,
        POISON
    }

    private Items[] jackpot;
    private void Start()
    {
        roll1Sprite = roll1.GetComponent<SpriteRenderer>();
        roll2Sprite = roll2.GetComponent<SpriteRenderer>();
        roll3Sprite = roll3.GetComponent<SpriteRenderer>();
        jackpot = new Items[3];
    }

    public void Rolls()
    {
        // Do rolling
        int slotA = Random.Range(1, 141);
        int slotB = Random.Range(1, 141);
        int slotC = Random.Range(1, 141);


        // Result
        jackpot[0] = CheckPrize(slotA);
        jackpot[1] = CheckPrize(slotB);
        jackpot[2] = CheckPrize(slotC);

        PrintPrize();
    }

    public Items CheckPrize(int random)
    {
        if (random >= 1 && random <= 60) return Items.SWORD;
        if (random >= 61 && random <= 70) return Items.SHIELD;
        if (random >= 71 && random <= 100) return Items.POTION;
        if (random == 101) return Items.SEVEN;
        if (random >= 102 && random <= 126) return Items.POISON;
        if (random >= 127 && random <= 141) return Items.MAGIC;

        return Items.SWORD;
    }

    public void PrintPrize()
    {
        Debug.Log(jackpot[0] + " | "+ jackpot[1] + " | " + jackpot[2] );
    }
}
