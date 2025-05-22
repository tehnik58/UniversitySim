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
        Vector3 mousePosOnScreen = new Vector3((int)Input.mousePosition.x, (int)Input.mousePosition.y, 0);
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        float x, y;

        if (mousePosOnScreen.x < Screen.width / 2)
            x = (mousePosOnScreen - (rectTransform.sizeDelta.x / 2 * Vector3.left)).x;
        else
            x = (mousePosOnScreen + (rectTransform.sizeDelta.x / 2 * Vector3.left)).x;

        if (mousePosOnScreen.y < Screen.height / 2)
            y = (mousePosOnScreen + (rectTransform.sizeDelta.y / 2 * Vector3.up)).y;
        else
            y = (mousePosOnScreen - (rectTransform.sizeDelta.y / 2 * Vector3.up)).y;

        rectTransform.anchoredPosition = Vector3.up * y + Vector3.right * x;
    }
}
