using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static readonly string[] mounthName = new string[]
    {
        "январь", "февраль", "март", "апрель", "май", 
        "июнь", "июль", "август", "сентябрь", "октябрь", 
        "ноябрь", "декабрь"
    };
    public int _year = 2015;
    public bool IsActiveTimer = true;

    private void Start()
    {
        G._timer = this;
        StartCoroutine(TimeIteration(0));
    }

    private IEnumerator TimeIteration(int i)
    {
        while(IsActiveTimer)
        {
            G.TimeIterationMounth?.Invoke(mounthName[i % 12]);
            i = (i + 1) % 12;
            if (i == 0)
            {
                _year++;
                G.TimeIterationYear?.Invoke(_year);
            }
            yield return new WaitForSeconds(G._secondsPerYear / 12.0f);
        }
    }
}
