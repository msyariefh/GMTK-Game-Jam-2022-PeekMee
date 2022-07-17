using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public GameObject unit;
    public TMP_Text text;
    private UnitStats objectStats;
    private bool isAnimating = false;
    private Slider slider;

    private void Start()
    {
        objectStats = unit.GetComponent<UnitStats>();
        slider = GetComponent<Slider>();
        slider.maxValue = objectStats.maxHP;
        //slider.value = objectStats.maxHP;
        StartAnimation();
        text.text = "HP: " + Mathf.FloorToInt(slider.maxValue) + "/" + slider.maxValue;
        objectStats.OnHPChange += HPChangeHandler;
    }

    private void Update()
    {

    }
    private void StartAnimation()
    {
        LeanTween.value(slider.value, objectStats.maxHP, 1.5f)
            .setOnUpdate((float val) => slider.value = val);
    }

    private void ChangeHPBar(float to)
    {
        LeanTween.value(slider.value, to, .5f)
            .setOnUpdate((float val) => slider.value = val)
            .setOnComplete(() => isAnimating = false);
        LeanTween.value(slider.value, to, .5f)
            .setOnUpdate((float val) => { text.text = "HP: " + Mathf.FloorToInt(val) + "/" + slider.maxValue; });
    }

    private void HPChangeHandler(int newHPValue)
    {
        ChangeHPBar(newHPValue);
    }

}
