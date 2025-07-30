using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuildingInfo
{
    public GameObject buildingPref;
    public string     buildingName;
    public int        price;
}

[CreateAssetMenu(fileName = "Buildings", menuName = "BuildingSys/Building")]
public class Buildings_SO : ScriptableObject
{
    public BuildingInfo[] buildings;
}
