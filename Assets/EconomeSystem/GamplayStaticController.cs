using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class GamplayStaticController
{
    private static int expectedNumberOfBuildings;
    public static float satisfaction = 1.0f;

    public static Action<bool> PauseEvent;

    public static void AddRoom(int exp)
    {
        expectedNumberOfBuildings+=exp;
    }
    public static void CheckConditionsForUnPause()
    {
        Debug.Log($"{expectedNumberOfBuildings} > {BuildStaticInfo.GetCountOfBuilding()}");
        PauseEvent.Invoke(expectedNumberOfBuildings > BuildStaticInfo.GetCountOfBuilding());
    }
    public static bool CheckOnLose()
    {
        ScoreSatisfaction();
        return StaticEconomicInfo.Money < 0.0f || satisfaction < 0.0;
    }
    public static void ScoreSatisfaction()
    {
        satisfaction += (StaticEconomicInfo.GetTeacherCount()/ StaticEconomicInfo.GetStudentCount())/(1/8) - (2 - StaticEconomicInfo.GetRoomCount()/2);
    }
}
