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
        moneyField.text = $"������: {StaticEconomicInfo.Money}";
        if(GamplayStaticController.satisfaction > 0.5f)
            SatisfactionField.text = $"�����������������: ����";
        else
            SatisfactionField.text = $"�����������������: ����";
    }
}
