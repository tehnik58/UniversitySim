using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class G
{
    public static Timer _timer;
    public static float _secondsPerYear = 24.0f;

    public static Action<string> TimeIterationMounth;
    public static Action<int> TimeIterationYear;
}
