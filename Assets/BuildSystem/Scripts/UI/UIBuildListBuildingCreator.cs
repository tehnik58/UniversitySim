using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct BuildingInfo
{
    public string name;
    public GameObject Building;
    public GameObject PreBuilding;

    public override string ToString()
    {
        return $"{name}: {Building}, {PreBuilding}";
    }
}
public class UIBuildListBuildingCreator : MonoBehaviour
{
    [SerializeField] List<BuildingInfo> buildingList = new List<BuildingInfo>();
    [SerializeField] GameObject buildingBtnPref;
    private List<GameObject> Buttons = new List<GameObject>();
    void Start()
    {
        foreach (var buildingInfo in buildingList) 
        {
            Buttons.Add(Instantiate(buildingBtnPref, this.transform));
            Buttons[Buttons.Count - 1].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText(buildingInfo.name);
            Buttons[Buttons.Count - 1].GetComponent<UIBuildingButtonAction>().SetBuildingInfo(buildingInfo);
        }
    }
}
