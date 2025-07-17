using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Builds", menuName = "Buildings/Builds", order = 51)]
public class BaseBuild_SO : ScriptableObject
{
    [SerializeField] public string buildingName;
    [SerializeField] public float price;
    [SerializeField] public GameObject prefab;
}
