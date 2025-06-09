using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeEventController : MonoBehaviour
{
    [SerializeField] public float MounthSecond = 2.0f;
    private string methodName = "TimeUpdate";
    
    public void StartTimer()
    {
        InvokeRepeating(methodName, MounthSecond, MounthSecond);
    }
    
    public void StopTimer()
    {
        CancelInvoke(methodName);
    }
    
    private void TimeUpdate()
    {
        GlobalInteractEvent.OnNextMounth.Invoke();
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }
}
