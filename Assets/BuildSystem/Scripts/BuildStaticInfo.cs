using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildStaticInfo
{
    private static IInteractableObj SelectedBuild;
    public static Action<IInteractableObj> OnSelectedBuild;
    public static Action<IInteractableObj> OnDeSelectedBuild;

    public static void SelectedBuildInstance(IInteractableObj buildObj)
    {
        SelectedBuild = null;
        OnSelectedBuild?.Invoke(buildObj);
    }
    public static void DeSelectedBuildInstance(IInteractableObj buildObj)
    {
        SelectedBuild = buildObj;
        OnDeSelectedBuild?.Invoke(buildObj);
    }
    public static bool IsSelectedBuild(IInteractableObj buildObj)
    {
        return SelectedBuild == buildObj;
    }
}
