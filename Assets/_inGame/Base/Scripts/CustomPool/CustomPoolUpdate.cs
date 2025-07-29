using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomPoolUpdate : MonoBehaviour
{
    void FixedUpdate()
    {
        CustomPoolStatic.CustomFixedUpdateList?.Invoke();
    }
    void Update()
    {
        CustomPoolStatic.CustomUpdateList?.Invoke();
    }
}
