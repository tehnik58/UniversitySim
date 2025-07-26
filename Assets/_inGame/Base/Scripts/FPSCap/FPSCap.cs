using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCap : MonoBehaviour
{
    [Tooltip("���������� -1 ��� ������������� V-Sync")]
    [Range(-1, 170)] public int targetFPS = 60;

    void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }

   
}
