using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FantomObjController : MonoBehaviour
{
    [SerializeField]
    public GameObject fantomObject;
    public string fantomName;
    public Material fantomMaterial;
    [FormerlySerializedAs("IsActiveSystem")] public bool IsActiveSystem = true;

    private GameObject fantomInstance;

    private void Start()
    {
        fantomInstance = Instantiate(fantomObject);
        fantomInstance.transform.SetParent(transform, false);
        fantomInstance.SetActive(false);
    }

    public void SetFantomStatusActive(bool statusNotStoped)
    {
        IsActiveSystem = statusNotStoped;
    }
    
    public void SetFantomStatusActive(bool statusNotStoped, bool statusActiveFantom)
    {
        IsActiveSystem = statusNotStoped;
        fantomInstance.SetActive(statusActiveFantom);
    }
    
    public void SwitchFantomStatusActive()
    {
        IsActiveSystem = !IsActiveSystem;
    }

    public void SetFantom(GameObject _fantomObject)
    {
        bool isFantomeActive = fantomInstance.activeInHierarchy;
        Destroy(fantomInstance);
        fantomObject = _fantomObject;
        
        fantomInstance = Instantiate(fantomObject);
        fantomInstance.transform.SetParent(transform, false);
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
        if (IsActiveSystem)
            fantomInstance.SetActive(true);
    }
    public virtual void FantomOff()
    {
        if (IsActiveSystem)
            fantomInstance.SetActive(false);
    }
}
