using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBuildObj : InteractableObj
{
    [SerializeField] GameObject _BuildObj;
    [SerializeField] GameObject _PreBuildObj;
    
    [SerializeField] GameObject _InteractObj;

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
        _InteractObj.SetActive(false);
    }

    public override void Click()
    {
        Instantiate(_BuildObj, this.transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 0.5f);
    }
}
