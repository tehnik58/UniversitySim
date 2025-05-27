using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInputData : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomCount;
    [SerializeField] private TMP_InputField teachersCount;
    [SerializeField] private TMP_InputField studentsCount;
    
    public void SetValuesOnEducationProgramm()
    {
        StaticEconomicInfo.EduPrograms.Add(
            new EducationalProgram(float.Parse(teachersCount.text), 
            float.Parse(studentsCount.text), 
            float.Parse(roomCount.text))
            );
        Debug.Log($"Economic Score: {StaticEconomicInfo.ScorePerYers(0)}");
    }
}
