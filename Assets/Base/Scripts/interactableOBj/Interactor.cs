using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPlayer;
    [SerializeField] private Camera _camera;
    [SerializeField] private float distance;
    
    private Vector2 ScreenSize;
    private RaycastHit hit;
    private Outline outline;
    private InteractableObj interactableObj;
    
    void FixedUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            Outline _outline;
            if (hit.transform.gameObject.TryGetComponent<Outline>(out _outline))
            {
                outline = _outline;
                outline.OutlineWidth = 5.0f;
            }
            else if (outline)
            {
                outline.OutlineWidth = .0f;
                outline = null;
            }

            InteractableObj _interactableObj;
            if (hit.transform.gameObject.TryGetComponent<InteractableObj>(out _interactableObj))
            {
                interactableObj = _interactableObj;
                interactableObj.Interact();
            }
            else if (interactableObj != null)
            {
                interactableObj.DeInteract();
                interactableObj = null;
            }
        }
    }

    void checkCLickOnObject()
    {
        if (interactableObj != null && Input.GetMouseButtonDown(0))
            interactableObj.Click();
    }
    
    void Update()
    {
        checkCLickOnObject();
    }
}
