using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buildings", menuName = "Buildings/Building")]
public class Building_SO : ScriptableObject
{
    [SerializeField] public List<BaseBuild_SO> Buildings;
}
