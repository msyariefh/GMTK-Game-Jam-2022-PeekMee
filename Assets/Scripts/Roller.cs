using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Security.Cryptography;

public class Roller : MonoBehaviour
{
    public GameObject roll1;
    public GameObject roll2;
    public GameObject roll3;

    private SpriteRenderer roll1Sprite;
    private SpriteRenderer roll2Sprite;
    private SpriteRenderer roll3Sprite;

    private Items[] itemListRandomizer;

    public enum Items
    {
        SWORD,
        POTION,
        SHIELD,
        MAGIC,
        SEVEN,
        POISON
    }

    [HideInInspector] public Items[] jackpot;
    private void Start()
    {
        itemListRandomizer = new Items[100];
        for (int k = 0; k < itemListRandomizer.Length; k++)
        {
            if (k >= 0 && k < 35) { itemListRandomizer[k] = Items.SWORD; continue; }
            if (k >= 35 && k < 55) { itemListRandomizer[k] = Items.SHIELD; continue; }
            if (k >= 55 && k < 72) { itemListRandomizer[k] = Items.POISON; continue; }
            if (k >= 72 && k < 87) { itemListRandomizer[k] = Items.POTION; continue; }
            if (k >= 87 && k < 96) { itemListRandomizer[k] = Items.MAGIC; continue; }
            if (k >= 96 && k < 100) { itemListRandomizer[k] = Items.SEVEN; continue; }

        }
        RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        itemListRandomizer = itemListRandomizer.OrderBy(x => Next(random)).ToArray();

        //print(String.Join(",", itemListRandomizer));

        roll1Sprite = roll1.GetComponent<SpriteRenderer>();
        roll2Sprite = roll2.GetComponent<SpriteRenderer>();
        roll3Sprite = roll3.GetComponent<SpriteRenderer>();
        jackpot = new Items[3];


    }

    public void Rolls()
    {
        // Do rolling
        int slotA = UnityEngine.Random.Range(1, 100);
        int slotB = UnityEngine.Random.Range(1, 100);
        int slotC = UnityEngine.Random.Range(1, 100);


        // Result
        jackpot[0] = itemListRandomizer[slotA - 1];
        jackpot[1] = itemListRandomizer[slotB - 1];
        jackpot[2] = itemListRandomizer[slotC - 1];

        print(jackpot[0] + " | " + jackpot[1] + " | " + jackpot[2]);

    }

    private int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[4];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }
    
}
