using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBuildObj : InteractableObj
{
    [SerializeField] GameObject _BuildObj;
    [SerializeField] GameObject _PreBuildObj;
    private GameObject _InteractObj;
    [SerializeField] private bool _IsSelected = false;
    
    private void Start()
    {
        _InteractObj = Instantiate(_PreBuildObj, this.transform, false);
        _InteractObj.transform.position = transform.position;
        _InteractObj.SetActive(false);
        GlobalInteractEvent.OnEmptyClicked += SetDeSelect;
    }
    private void OnDestroy()
    {
        //if(BuildStaticInfo.ClickedBuild == this)

        GlobalInteractEvent.OnEmptyClicked -= SetDeSelect;
    }
    public override void OnInteract()
    {
        print($"OnInteract: {_InteractObj}");
        _InteractObj?.SetActive(true);
    }
    public override void OnDeInteract()
    {
        //print("DeSelectBuild");
        if (_IsSelected)
            return;
        if (!BuildStaticInfo.IsSelectedBuild(this))
            BuildStaticInfo.SetDeSelectedBuildInstance(this);
        if(_InteractObj)
            _InteractObj?.SetActive(false);
    }
    public override void OnRightClick()
    {
        Instantiate(_BuildObj, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }
    
    public override void OnClick()
    {
        if (BuildStaticInfo.IsHoveredOnUI)
            return;
        BuildStaticInfo.SetSelectedBuildInstance(this);
        BuildStaticInfo.SetClickeBuildInstance(this);
        _IsSelected = !_IsSelected;
        if (!_IsSelected)
        {
            BuildStaticInfo.OnCloseBuildUI?.Invoke();
        } 
    }

    public override void SetDeSelect()
    {
        if (BuildStaticInfo.IsHoveredOnUI)
            return;
        _IsSelected = false;
    }

    public void SetBuildObj(GameObject buildObj, GameObject preBuildObj)
    {
        //print("switch Build");
        BuildStaticInfo.OnCloseBuildUI.Invoke();
        Destroy(_InteractObj);
        _BuildObj = buildObj;
        _PreBuildObj = preBuildObj;
        _InteractObj = Instantiate(_PreBuildObj, this.transform, false);
        _InteractObj.transform.position = transform.position;
    }
}
