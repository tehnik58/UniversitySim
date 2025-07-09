using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildZone : InteractableObject
{
    [SerializeField]
    private GameObject build;
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
        buildInstance = Instantiate(build, transform.position, Quaternion.identity);
    }

    public override void OnMouseExitCustom()
    {
        base.OnMouseExitCustom();
        Destroy(buildInstance);
    }
}
