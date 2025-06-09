using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mounth
{
    January, February, March, April, May, June, July, August, September, October, November
}
public static class GlobalInteractEvent
{
    public static Action OnEmptyClicked;
    public static Action OnNextMounth;
    public static Mounth mounth = Mounth.January;
    public static bool IsLockOnUI = false;
    
    public static float MounthSecond = 2.0f;
    private static string methodName = "TimeUpdate";
}
