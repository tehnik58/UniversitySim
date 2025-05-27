using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInputData : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomCount;
    [SerializeField] private TMP_InputField teachersCount;
    [SerializeField] private TMP_InputField studentsCount;
    [SerializeField] private TextMeshProUGUI pribInfo;
    [SerializeField] private TextMeshProUGUI rashInfo;
    public void SetValuesOnEducationProgramm()
    {
        StaticEconomicInfo.EduPrograms.Add(
            new EducationalProgram(float.Parse(teachersCount.text), 
            float.Parse(studentsCount.text), 
            float.Parse(roomCount.text))
            );
        GamplayStaticController.CheckConditionsForUnPause();
    }

    void Update()
    {
        pribInfo.text = $"Доход в год: {float.Parse(studentsCount.text) * 3.0f}";
        rashInfo.text = $"Расход в год: {float.Parse(roomCount.text) * 20.0f + float.Parse(teachersCount.text) * 4.0f}";
        
    }
}
