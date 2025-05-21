using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public interface IInteractableObj
{
    public virtual void Interact()
    {
        
    }
    public virtual void DeInteract()
    {
        
    }
    public virtual void Click()
    {
        
    }
    public virtual void OnInteract()
    {
        
    }
    public virtual void OnDeInteract()
    {
        
    }
}
public class InteractableObj : MonoBehaviour, IInteractableObj
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
    
    public virtual void Click()
    {
        print("Click");
    }
}
