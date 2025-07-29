using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIBuildingButtonAction : MonoBehaviour
{
    [SerializeField]
    private BuildingInfo buildingInfo;
    public void SetBuildingInfo(BuildingInfo _buildingInfo)
    {
        buildingInfo = _buildingInfo;
    }
    public void Build()
    {
        //print(buildingInfo);
        BuildStaticInfo.ClickedBuild?.SetBuildObj(buildingInfo.Building, buildingInfo.PreBuilding);
    }

    public void OnHover(bool _isHovered)
    {
        BuildStaticInfo.IsHoveredOnUI = _isHovered;
        GlobalInteractEvent.IsLockOnUI = _isHovered;
    }
}
