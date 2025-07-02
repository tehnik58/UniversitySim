using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAdapter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyField;
    [SerializeField] private TextMeshProUGUI SatisfactionField;
    [SerializeField] private TextMeshProUGUI SatisfactionField1;
    [SerializeField] private TextMeshProUGUI StudentField;
    [SerializeField] private TextMeshProUGUI TeacherField;
    [SerializeField] private TextMeshProUGUI pribInfo;

    void Update()
    {
        moneyField.text = $"Бюджет: {StaticEconomicInfo.Money}";
        SatisfactionField.text = $"Отношение: {GamplayStaticController.GetSatisfaction()}";
        SatisfactionField1.text = $"Отношение: {GamplayStaticController.GetScoreSatisfaction()}";
        StudentField.text = $"Учащиеся: {StaticEconomicInfo.GetStudentCount()}";
        TeacherField.text = $"Преподаватели: {StaticEconomicInfo.GetTeacherCount()}";
    }
}
