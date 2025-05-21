using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIBuildActivator : MonoBehaviour
{
    void Start()
    {
        BuildStaticInfo.OnSelectedBuild += SetPosition;
    }

    void SetPosition(IInteractableObj buildObj)
    {
        this.GetComponent<RectTransform>().position = new Vector3((int)Input.mousePosition.x, (int)Input.mousePosition.y, 0);
        print($"{new Vector3((int)Input.mousePosition.x, (int)Input.mousePosition.y, 0)} - {this.GetComponent<RectTransform>().position}");
    }
}
