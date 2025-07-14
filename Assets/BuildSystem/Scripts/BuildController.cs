using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : ObjectInteractionController
{
    [SerializeField] private Building_SO buildingSO;
    private int targetIndex = 1;
    
    private void HandleRaycast()
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
                if (currentHovered != null && currentHovered.TryGetComponent<BuildZone>(out target))
                    target.OnMouseEnterCustom(buildingSO.Buildings[targetIndex].prefab);
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
}
