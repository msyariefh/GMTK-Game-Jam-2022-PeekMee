using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public string nameI;
    public int maxHP;
    private int currentHP;
    private int _currentHP;
    [HideInInspector] public string comment;
    [HideInInspector] public bool isShielded = false;
    [HideInInspector] public bool isFullShield = false;
    [SerializeField] private int basicAttack;
    [SerializeField] private int specialAttackPercentage;
    [SerializeField] private int regenHP;
    public GameObject redscreen;
    private Camera cam;
    private void Awake()
    {
        currentHP = maxHP;
        _currentHP = currentHP;
        cam = Camera.main;
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
        //comment = nameI + " Attack " + other.nameI + " by " + (basicAttack * multiplication);
        switch (nameI)
        {
            case "Enemy":
                FindObjectOfType<AudioManager>().PlaySound("EnemyAttack");
                break;
            case "Player":
                FindObjectOfType<AudioManager>().PlaySound("AttackPlayer");
                break;
        }
        
    }

    public void Attacked(int attackPoint)
    {
        int percentage;
        if (isFullShield == true) percentage = 50;
        else percentage = 30;
        int afterShielded = attackPoint;
        if (nameI == "Player")
        {
            redscreen.SetActive(true);
            cam.GetComponent<Shake>().start = true;
            LeanTween.value(0f, 1f, .5f).setOnComplete(()=>{ redscreen.SetActive(false); });
        }
        if (isShielded == true)
        {
            FindObjectOfType<AudioManager>().PlaySound("Absorb");
            afterShielded -= Mathf.FloorToInt(percentage / 100f * attackPoint);
            GameManager.Instance.comment.text += "\n" + nameI + " use shield! " + " (- "+ percentage+ "%)";
            isShielded = false;
        }
        if (currentHP - afterShielded <= 0)
        {
            // DIE!
            print(nameI + " DIED!");
            if (nameI == "Player") MenuManager.Instance.GameOver();
            else
            {
                LeanTween.value(0f, 1f, .4f).setOnComplete(() =>
                {
                    LeanTween.moveY(gameObject, -7.5f, 1f).setEase(LeanTweenType.easeOutCubic)
                    .setOnComplete(() => { MenuManager.Instance.GameWin(); });
                });
                
                
            }
            currentHP = 0;
            return ;
        }
        currentHP -= afterShielded;
        comment = nameI + " -" +afterShielded + "HP" ;

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
        FindObjectOfType<AudioManager>().PlaySound("Heal");
        comment = nameI + " +" + addHealth + "HP";
    }

    public void SpecialAttack(UnitStats other)
    {
        FindObjectOfType<AudioManager>().PlaySound("Buff");
        FindObjectOfType<AudioManager>().PlaySound("Attack");
        other.Attacked(Mathf.FloorToInt(other.maxHP * specialAttackPercentage / 100f));
        comment = nameI + "  launch special attack!";
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public delegate void OnHPChangeDelegate(int newHPVal);
    public event OnHPChangeDelegate OnHPChange;
}
