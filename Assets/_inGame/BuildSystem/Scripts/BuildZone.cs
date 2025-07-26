using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildZone : InteractableObject
{
    [SerializeField] public int TargetIndex = 0;
    [SerializeField] private GameObject build;
    private GameObject buildInstance;

    public void SetBuild(GameObject _build)
    {
        build = _build;
    }
    
    public override void OnMouseDownCustom()
    {
        print("OnMouseDown");
    }

    public override void OnMouseEnterCustom()
    {
        base.OnMouseEnterCustom();
        if(build != null)
            buildInstance = Instantiate(build, transform.position, Quaternion.identity);
    }
    
    public override void OnMouseEnterCustom(GameObject _build)
    {
        base.OnMouseEnterCustom();
        build = _build;
        if(build != null)
            buildInstance = Instantiate(build, transform.position, Quaternion.identity);
    }

    public override void OnMouseExitCustom()
    {
        base.OnMouseExitCustom();
        Destroy(buildInstance);
    }
}
