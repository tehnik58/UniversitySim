using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InteractableObj : CustomPoolUpdate, IInteractableObj
{
    private bool _isHovered = false;

    public virtual void OnInteract()
    {
        print("OnInteract");
    }
    public virtual void OnDeInteract()
    {
        print("OnDeInteract");
    }
    public void Interact()
    {
        if (!_isHovered)
        {
            _isHovered = true;
            OnInteract();
        }
    }
    public void DeInteract()
    {
        if (_isHovered)
        {
            _isHovered = false;
            OnDeInteract();
        }
    }

    public virtual void SetDeSelect()
    {
        _isHovered = true;
        DeInteract();
    }

    public virtual void OnClick()
    {
        print("Click");
    }
    public virtual void OnRightClick()
    {
        print("RightClick");
    }
}
