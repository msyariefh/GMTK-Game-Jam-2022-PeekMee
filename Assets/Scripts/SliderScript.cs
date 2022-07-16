using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public GameObject unit;
    private UnitStats objectStats;

    private Slider slider;

    private void Start()
    {
        objectStats = unit.GetComponent<UnitStats>();
        slider = GetComponent<Slider>();
        slider.maxValue = objectStats.maxHP;
        slider.value = objectStats.maxHP;
    }

    private void Update()
    {
        if ( objectStats.GetCurrentHP() == slider.value) { return; }
        slider.value = objectStats.GetCurrentHP();
    }

}
