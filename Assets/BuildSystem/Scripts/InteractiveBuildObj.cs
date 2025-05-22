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
        GlobalInteractEvent.OnEmptyClicked += SetDeSelect;
    }
    private void OnDestroy()
    {
        GlobalInteractEvent.OnEmptyClicked -= SetDeSelect;
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
            BuildStaticInfo.SetDeSelectedBuildInstance(this);
        _InteractObj.SetActive(false);
    }
    public override void OnRightClick()
    {
        Instantiate(_BuildObj, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }
    
    public override void OnClick()
    {
        BuildStaticInfo.SetSelectedBuildInstance(this);
        BuildStaticInfo.SetClickeBuildInstance(this);
        _IsSelected = !_IsSelected;
    }

    public override void SetDeSelect()
    {
        _IsSelected = false;
        print("De Select Click");
        OnDeInteract();
    }

    public void SetBuildObj(GameObject buildObj, GameObject preBuildObj)
    {
        _BuildObj = buildObj;
        _PreBuildObj = preBuildObj;
    }

    
}
