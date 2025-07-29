using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class InteractiveBuildObj : InteractableObj
{
    [SerializeField] GameObject _BuildObj;
    [SerializeField] GameObject _PreBuildObj;
    [SerializeField] float Price = 33.0f;
    [SerializeField] float PricePerYear = 20.0f;
    [SerializeField] private bool _IsSelected = false;
    private GameObject _InteractObj;

    private void Start()
    {
        _InteractObj = Instantiate(_PreBuildObj, this.transform, false);
        _InteractObj.transform.position = transform.position;
        _InteractObj.SetActive(false);
        GlobalInteractEvent.OnEmptyClicked += SetDeSelect;
    }
    private void OnDestroy()
    {
        GlobalInteractEvent.OnEmptyClicked -= SetDeSelect;
    }
    public override void OnInteract()
    {
        //print($"OnInteract: {_InteractObj}");
        _InteractObj.SetActive(true);
    }
    public override void OnDeInteract()
    {
        if (_IsSelected)
            return;
        //print($"OnDeInteract: {_InteractObj}");
        if (!BuildStaticInfo.IsSelectedBuild(this))
            BuildStaticInfo.SetDeSelectedBuildInstance(this);
        if(_InteractObj)
            _InteractObj?.SetActive(false);
    }
    public override void OnRightClick()
    {
        if (!StaticEconomicInfo.TryBuy(33.3f))
            return;
        GameObject building = Instantiate(_BuildObj, this.transform.position, Quaternion.identity);
        BuildStaticInfo.OnBuildEvent?.Invoke(building);
        BuildStaticInfo.OnCloseBuildUI.Invoke();
        BuildStaticInfo.AddBuilding();
        GamplayStaticController.CheckConditionsForUnPause();
        building.SetActive(true);
        building.transform.rotation = transform.rotation;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 0.5f);
        
    }
    
    public override void OnClick()
    {
        if (BuildStaticInfo.IsHoveredOnUI)
            return;
        BuildStaticInfo.SetSelectedBuildInstance(this);
        BuildStaticInfo.SetClickedBuildInstance(this);
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
        _InteractObj?.SetActive(false);
        _IsSelected = false;
    }

    public void SetBuildObj(GameObject buildObj, GameObject preBuildObj)
    {
        Destroy(_InteractObj);
        _BuildObj = buildObj;
        _PreBuildObj = preBuildObj;
        _InteractObj = Instantiate(_PreBuildObj, this.transform, false);
        _InteractObj.transform.position = transform.position;
        _InteractObj.SetActive(true);
    }
}
