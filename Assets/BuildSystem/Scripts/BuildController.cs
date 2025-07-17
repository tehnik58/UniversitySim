using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : ObjectInteractionController
{
    [SerializeField] private Building_SO buildingSO;
    [SerializeField] private BuildUIController buildUIController;
    private InteractableObject TargetBuild;
    
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
                if (currentHovered != null && currentHovered.TryGetComponent<BuildZone>(out var target))
                    target.OnMouseEnterCustom(buildingSO.Buildings[buildUIController.targetIndex].prefab);
            }
            
            if (Input.GetMouseButtonDown(0) && currentHovered != null)
            {
                currentHovered.OnMouseDownCustom();
                if (TargetBuild != null)
                    if (currentHovered != TargetBuild)
                        TargetBuild = currentHovered;
                    else
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
