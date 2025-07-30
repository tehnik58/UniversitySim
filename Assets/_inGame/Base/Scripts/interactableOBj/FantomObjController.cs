using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomObjController : MonoBehaviour
{
    public GameObject fantomObject;
    public string fantomName;
    public Material fantomMaterial;
    public bool IsNotStoped = true;

    private GameObject fantomInstance;

    private void Start()
    {
        fantomInstance = Instantiate(fantomObject);
        fantomInstance.transform.position = transform.position;
        fantomInstance.SetActive(false);
    }

    public void SetFantomStatusActive(bool statusNotStoped)
    {
        IsNotStoped = statusNotStoped;
    }
    
    public void SetFantomStatusActive(bool statusNotStoped, bool statusActiveFantom)
    {
        IsNotStoped = statusNotStoped;
        fantomInstance.SetActive(statusActiveFantom);
    }
    
    public void SwitchFantomStatusActive()
    {
        IsNotStoped = !IsNotStoped;
    }

    public void SetFantom(GameObject _fantomObject)
    {
        bool isFantomeActive = fantomInstance.activeInHierarchy;
        Destroy(fantomInstance);
        fantomObject = _fantomObject;
        
        fantomInstance = Instantiate(fantomObject);
        fantomInstance.transform.position = transform.position;
        fantomInstance.SetActive(isFantomeActive);
    }
    public void SetFantom(GameObject _fantomObject, string _fantomName)
    {
        fantomName = _fantomName;
        
        bool isFantomeActive = fantomInstance.activeInHierarchy;
        Destroy(fantomInstance);
        fantomObject = _fantomObject;
        
        fantomInstance = Instantiate(fantomObject);
        fantomInstance.transform.position = transform.position;
        fantomInstance.SetActive(isFantomeActive);
    }
    
    public virtual void FantomOn()
    {
        if (IsNotStoped)
            fantomInstance.SetActive(true);
    }
    public virtual void FantomOff()
    {
        if (IsNotStoped)
            fantomInstance.SetActive(false);
    }
}
