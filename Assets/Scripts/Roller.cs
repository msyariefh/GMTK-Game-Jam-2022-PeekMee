using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Security.Cryptography;
using TMPro;

public class Roller : MonoBehaviour
{

    private SlotSprite[] slots;

    public GameObject roll1;
    public GameObject roll2;
    public GameObject roll3;

    public GameObject lever;

    private Image roll1Image;
    private Image roll2Image;
    private Image roll3Image;

    public RuntimeAnimatorController RollAnm;
    public GameObject EnemyAnm;

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
    private void Awake()
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


        roll1Image = roll1.GetComponent<Image>();
        roll2Image = roll2.GetComponent<Image>();
        roll3Image = roll3.GetComponent<Image>();

        slots = FindObjectOfType<SlotController>().slotSprites;
        jackpot = new Items[3];


    }

    public void Rolls()
    {
        // Animate lever
        FindObjectOfType<AudioManager>().PlaySound("LeverSlot");
        GetComponentInChildren<Button>().interactable = false;
        LeanTween.rotateZ(lever, -25f, .75f).setEase(LeanTweenType.easeInCubic)
            .setOnComplete(() => {
                ChangeSlots();
                LeanTween.rotateZ(lever, 25f, .75f).setEase(LeanTweenType.easeOutCubic);
            });

        // Do rolling
        int slotA = UnityEngine.Random.Range(1, 100);
        int slotB = UnityEngine.Random.Range(1, 100);
        int slotC = UnityEngine.Random.Range(1, 100);

        

        // Result
        jackpot[0] = itemListRandomizer[slotA - 1];
        jackpot[1] = itemListRandomizer[slotB - 1];
        jackpot[2] = itemListRandomizer[slotC - 1];



    }

    private int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[4];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }

    private void ChangeSlots()
    {
        var sprite1 = Array.Find(slots, x => x.type == jackpot[0]);
        var sprite2 = Array.Find(slots, x => x.type == jackpot[1]);
        var sprite3 = Array.Find(slots, x => x.type == jackpot[2]);

        FindObjectOfType<AudioManager>().PlaySound("RollSlot");
        LeanTween.value(0f, 1f, 1f).setOnUpdate((float val) =>
        {
            roll1.GetComponent<Animator>().runtimeAnimatorController = RollAnm;
        }).setOnComplete(() => {
            roll1.GetComponent<Animator>().runtimeAnimatorController = null;
            roll1Image.sprite = sprite1.sprite; });
        
        LeanTween.value(0f, 1f, 2f).setOnUpdate((float val) =>
        {
            roll2.GetComponent<Animator>().runtimeAnimatorController = RollAnm;
        }).setOnComplete(() => {
            roll2.GetComponent<Animator>().runtimeAnimatorController = null;
            roll2Image.sprite = sprite2.sprite; });
        
        LeanTween.value(0f, 1f, 3f).setOnUpdate((float val) =>
        {
            roll3.GetComponent<Animator>().runtimeAnimatorController = RollAnm;
        }).setOnComplete(() => {
            roll3.GetComponent<Animator>().runtimeAnimatorController = null;
            roll3Image.sprite = sprite3.sprite;
            Battle();
            GetComponentInChildren< Button>().interactable = true;
        });


        
        
        
    }
    private void Battle()
    {
        UnitStats enemyStats = GameManager.Instance.enemyStats;
        UnitStats playerStats = GameManager.Instance.playerStats;
        TMP_Text comment = GameManager.Instance.comment;

        var duplicates = jackpot.GroupBy(x => x).Where(y => y.Count() > 1).Select(y => y.Key);

        if ( duplicates.Count() < 1)
        {
            enemyStats.Attack(playerStats, 1);
            EnemyAnm.GetComponent<Animator>().SetTrigger("Attack");
            LeanTween.value(0f, 1f, .5f).setOnComplete(() => {
                comment.text += "\n" + playerStats.comment;
            });
            return;
        }

        int multiplier = 0;
        foreach (Roller.Items i in jackpot)
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
                    EnemyAnm.GetComponent<Animator>().SetTrigger("Hit");
                    LeanTween.value(0f, 1f, .5f).setOnComplete(()=> {
                        comment.text += "\n" + enemyStats.comment;
                    });
                    
                    break;
                case Roller.Items.POISON:
                    if (multiplier == 2) enemyStats.Attack(playerStats, 3);
                    else if (multiplier == 3) enemyStats.SpecialAttack(playerStats);
                    LeanTween.value(0f, 1f, .5f).setOnComplete(() => {
                        comment.text += "\n" + playerStats.comment;
                    });
                    break;
                case Roller.Items.POTION:

                    playerStats.Heal(GameManager.Instance.potionPercentage, multiplier-1);
                    LeanTween.value(0f, 1f, .5f).setOnComplete(() => {
                        comment.text += "\n" + playerStats.comment;
                    });
                    break;
                case Roller.Items.SEVEN:
                    if (multiplier == 2) playerStats.Attack(enemyStats, 2);
                    else if (multiplier == 3) {
                        FindObjectOfType<AudioManager>().PlaySound("Jackpot");
                        playerStats.SpecialAttack(enemyStats); }
                    LeanTween.value(0f, 1f, .5f).setOnComplete(() => {
                        comment.text += "\n" + playerStats.comment;
                        comment.text += "\n" + enemyStats.comment;
                    });
                    break;
                case Roller.Items.SHIELD:
                    playerStats.isShielded = true;
                    if (multiplier == 3) playerStats.isFullShield = true;
                    LeanTween.value(0f, 1f, .5f).setOnComplete(() => {
                        comment.text += "\n" + playerStats.name + " Got a shield!";
                    });
                    
                    break;
                case Roller.Items.SWORD:
                    if (multiplier == 2) playerStats.Attack(enemyStats, 1);
                    else if (multiplier == 3) playerStats.Attack(enemyStats, 3);
                    EnemyAnm.GetComponent<Animator>().SetTrigger("Hit");
                    LeanTween.value(0f, 1f, .5f).setOnComplete(() => {
                        comment.text += "\n" + enemyStats.comment;
                    });
                    break;
            }
            //comment.text = playerStats.comment;
        }
    }
    
}
