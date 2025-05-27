using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAdapter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyField;
    [SerializeField] private TextMeshProUGUI SatisfactionField;

    void Update()
    {
        moneyField.text = $"Бюджет: {StaticEconomicInfo.Money}";
        if(GamplayStaticController.satisfaction > 0.5f)
            SatisfactionField.text = $"Удовлетворенность: норм";
        else
            SatisfactionField.text = $"Удовлетворенность: боль";
    }
}
