using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionController : MonoBehaviour
{
    protected Camera mainCamera;
    protected InteractableObject currentHovered;
    
    [SerializeField] protected LayerMask interactionLayer;
    [SerializeField] protected float raycastDistance;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }
    
    protected virtual void HandleRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, raycastDistance, interactionLayer))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            
            if (interactable != currentHovered)
            {
                if (currentHovered != null) currentHovered.OnMouseExitCustom();
                currentHovered = interactable;
                BuildZone target;
                if (currentHovered != null) 
                    currentHovered.OnMouseEnterCustom();
            }
            
            if (Input.GetMouseButtonDown(0) && currentHovered != null)
            {
                currentHovered.OnMouseDownCustom();
            }
        }
        else if (currentHovered != null)
        {
            currentHovered.OnMouseExitCustom();
            currentHovered = null;
        }
    }
    
    private void Update()
    {
        HandleRaycast();
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay( mainCamera.transform.position,ray.direction, Color.red);
    }
}