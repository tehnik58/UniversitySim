using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public struct EducationalProgram
{
    public float TeacherCount;
    public float StudentCount;
    public float RoomCount;

    private float PricePerYersRoom;
    private float PriceForStudentPerYers;
    private float PriceTeachersPerYers;
    public EducationalProgram(float _teacherCount, float _studentCount, float _roomCount,
        float _pricePerYersRoom = 20.0f,
        float _priceForStudentPerYers = 3.0f,
        float _priceTeachersPerYers = 4.0f)
    {
        TeacherCount = _teacherCount;
        StudentCount = _studentCount;
        RoomCount = _roomCount;
        PricePerYersRoom = _pricePerYersRoom;
        PriceForStudentPerYers = _priceForStudentPerYers;
        PriceTeachersPerYers = _priceTeachersPerYers;
        GamplayStaticController.AddRoom((int)_roomCount);
    }

    public float ScoreEconomic()
    {
        return StudentCount*PriceForStudentPerYers - (TeacherCount*PriceTeachersPerYers + RoomCount*PricePerYersRoom);
    }
}
public static class StaticEconomicInfo
{
    public static Action OnStartYers;
    public static List<EducationalProgram> EduPrograms = new List<EducationalProgram>();
    public static float Money = 200.0f;

    public static bool TryBuy(float price)
    {
        if (Money > price) 
        { 
            Money -= price;
            return true;
        }
        return false;
    }
    public static void AcceptEconomicIteration()
    {
        foreach (var eduProgram in EduPrograms) 
        {
            Money += eduProgram.ScoreEconomic();
        }
        Debug.Log($"Money: {Money}");
    }
    public static float ScorePerYers(int index)
    {
        return EduPrograms[index].ScoreEconomic();
    }
    public static float GetTeacherCount()
    {
        float summ = 0.0f;
        foreach (var eduProgram in EduPrograms)
        {
            summ += eduProgram.TeacherCount;
        }
        return summ;
    }
    public static float GetStudentCount()
    {
        float summ = 0.0f;
        foreach (var eduProgram in EduPrograms)
        {
            summ += eduProgram.StudentCount;
        }
        return summ;
    }
    public static float GetRoomCount()
    {
        float summ = 0.0f;
        foreach (var eduProgram in EduPrograms)
        {
            summ += eduProgram.RoomCount;
        }
        return summ;
    }
}
