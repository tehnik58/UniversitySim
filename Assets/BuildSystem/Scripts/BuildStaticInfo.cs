using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildStaticInfo
{
    private static IInteractableObj SelectedBuild;
    private static IInteractableObj ClickedBuild;
    public static Action<IInteractableObj> OnSelectedBuild;
    public static Action<IInteractableObj> OnDeSelectedBuild;

    public static void SetSelectedBuildInstance(IInteractableObj buildObj)
    {
        SelectedBuild = null;
        OnSelectedBuild?.Invoke(buildObj);
    }
    public static void SetDeSelectedBuildInstance(IInteractableObj buildObj)
    {
        SelectedBuild = buildObj;
        OnDeSelectedBuild?.Invoke(buildObj);
    }
    public static bool IsSelectedBuild(IInteractableObj buildObj)
    {
        return SelectedBuild == buildObj;
    }
    public static void SetClickeBuildInstance(IInteractableObj buildObj)
    {
        if (buildObj == ClickedBuild)
            return;
        ClickedBuild?.SetDeSelect();
        ClickedBuild = buildObj;
    }
}
