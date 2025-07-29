using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class GamplayStaticController
{
    private static int expectedNumberOfBuildings;
    private static float satisfaction = 1.0f;

    private static bool isManualPause = false;

    public static Action<bool> PauseEvent;

    public static void ToggleManualPause()
    {
        isManualPause = !isManualPause;
        UpdatePauseState(); 
    }

    private static void UpdatePauseState()
    {
        bool shouldPause = isManualPause || (expectedNumberOfBuildings > BuildStaticInfo.GetCountOfBuilding());
        PauseEvent?.Invoke(shouldPause);
    }
    public static void AddRoom(int exp)
    {
        expectedNumberOfBuildings+=exp;
    }
    public static void CheckConditionsForUnPause()
    {
        Debug.Log($"{expectedNumberOfBuildings} > {BuildStaticInfo.GetCountOfBuilding()}");
        UpdatePauseState();
    }
    public static bool CheckOnLose()
    {
        return StaticEconomicInfo.Money < 0.0f || satisfaction < 0.0;
    }
    public static void ScoreSatisfaction()
    {
        if (StaticEconomicInfo.GetTeacherCount() != 0 && StaticEconomicInfo.GetStudentCount() != 0)
            satisfaction += (StaticEconomicInfo.GetTeacherCount()/ StaticEconomicInfo.GetStudentCount())/(1.0f/8.0f) - (2 - StaticEconomicInfo.GetRoomCount()/3);
    }
    public static float GetScoreSatisfaction()
    {
        if (StaticEconomicInfo.GetTeacherCount() != 0 && StaticEconomicInfo.GetStudentCount() != 0)
            return (StaticEconomicInfo.GetTeacherCount()/ StaticEconomicInfo.GetStudentCount())/(1.0f/8.0f) - (2 - StaticEconomicInfo.GetRoomCount()/3);
        return 0.0f;
    }
    public static float GetSatisfaction()
    {
        return satisfaction;
    }
}
