using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomObjController : MonoBehaviour
{
    public GameObject fantomObject;
    public Material fantomMaterial;
    public bool IsNotStoped = true;

    private GameObject fantomInstance;

    private void Start()
    {
        fantomInstance = Instantiate(fantomObject);
        fantomInstance.transform.position = transform.position;
        fantomInstance.SetActive(false);
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
