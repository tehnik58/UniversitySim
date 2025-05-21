using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBuildObj : InteractableObj
{
    [SerializeField] GameObject _BuildObj;
    [SerializeField] GameObject _PreBuildObj;
    private GameObject _InteractObj;
    private bool _IsSelected = false;
    
    private void Start()
    {
        _InteractObj = Instantiate(_PreBuildObj, this.transform, false);
        _InteractObj.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _InteractObj.SetActive(false);
    }

    public override void OnInteract()
    {
        _InteractObj.SetActive(true);
    }
    public override void OnDeInteract()
    {
        if (_IsSelected)
            return;
        if (!BuildStaticInfo.IsSelectedBuild(this))
            BuildStaticInfo.DeSelectedBuildInstance(this);
        _InteractObj.SetActive(false);
    }
    
    public override void OnRightClick()
    {
        BuildStaticInfo.SelectedBuildInstance(this);
        _IsSelected = !_IsSelected;
    }
    
    public override void OnClick()
    {
        Instantiate(_BuildObj, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }

    public void SetBuildObj(GameObject buildObj, GameObject preBuildObj)
    {
        _BuildObj = buildObj;
        _PreBuildObj = preBuildObj;
    }
}
