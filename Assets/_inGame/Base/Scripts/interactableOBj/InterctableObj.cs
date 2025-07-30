using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterctableObj : MonoBehaviour
{
    [Header("Interact Events")]
    public SerializebleAction OnHoverAction;
    public SerializebleAction OnUnHoverAction;
    public SerializebleAction OnClickAction;

    void OnMouseEnter()
    {
        OnHoverAction?.Invoke();
    }

    void OnMouseExit()
    {
        OnUnHoverAction?.Invoke();
    }
    void OnMouseDown()
    {
        OnClickAction?.Invoke();
    }
}
