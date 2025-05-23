using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class BuildStaticInfo
{
    private static InteractiveBuildObj SelectedBuild;

    public static InteractiveBuildObj ClickedBuild;
    public static Action<InteractiveBuildObj> OnSelectedBuild;
    public static Action<InteractiveBuildObj> OnDeSelectedBuild;
    public static Action<InteractiveBuildObj> OnCloseFreeUIBuild;
    public static Action OnCloseBuildUI;
    public static bool IsHoveredOnUI = false;

    public static void SetHoveredOnUI(bool _isHoveredOnUI)
    {
        IsHoveredOnUI = _isHoveredOnUI;
    }
    public static void SetSelectedBuildInstance(InteractiveBuildObj buildObj)
    {
        SelectedBuild = buildObj;
        OnSelectedBuild?.Invoke(buildObj);
    }
    public static void SetDeSelectedBuildInstance(InteractiveBuildObj buildObj)
    {
        SelectedBuild = null;
        OnDeSelectedBuild?.Invoke(buildObj);
    }
    public static bool IsSelectedBuild(InteractiveBuildObj buildObj)
    {
        return SelectedBuild == buildObj;
    }
    public static void SetClickeBuildInstance(InteractiveBuildObj buildObj)
    {
        Debug.Log($"ClickedBuild: {ClickedBuild}, buildObj: {buildObj}");
        if (buildObj == ClickedBuild)
        {
            Debug.Log($"{buildObj} == {ClickedBuild}");
            
            ClickedBuild = null;
            return;
        }
        if (ClickedBuild)
        {
            Debug.Log($"DeSelect {ClickedBuild}");
            ClickedBuild?.SetDeSelect();
        }
        //Debug.Log($"new Clicked: {ClickedBuild}");
        ClickedBuild = buildObj;
    }
    public static void OnBuild()
    {
        IsHoveredOnUI = false;
        OnCloseBuildUI.Invoke();
        //Debug.Log("ClickedBuild = null");
        ClickedBuild = null;
        SelectedBuild = null;
    }
}
