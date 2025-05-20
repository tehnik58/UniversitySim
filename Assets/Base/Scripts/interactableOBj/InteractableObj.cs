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
}
public class InteractableObj : MonoBehaviour, IInteractableObj
{
    public virtual void Interact()
    {
        print("Interact");
    }
    public virtual void DeInteract()
    {
     print("DeInteract");   
    }
    
    public virtual void Click()
    {
        print("Click");
    }
}
