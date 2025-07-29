using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : ObjectInteractionController
{
    [SerializeField] private Building_SO buildingSO;
    [SerializeField] private BuildUIController buildUIController;
    private InteractableObject TargetBuild;

    private void Start()
    {
        mainCamera = Camera.main;
        buildUIController.Refrash += Refrash;
        buildUIController.gameObject.SetActive(false);
    }

    private void Refrash(int arg0)
    {
        if( TargetBuild.gameObject.TryGetComponent<BuildZone>(out var target))
            target.SetBuild(buildingSO.Buildings[arg0].prefab);
    }

    protected override void HandleRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, raycastDistance, interactionLayer) && !buildUIController.IsHovered)
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
                print($"{buildUIController.gameObject.GetComponent<RectTransform>().anchoredPosition}\t{Input.mousePosition}");
                currentHovered.OnMouseDownCustom();
                buildUIController.gameObject.SetActive(true);
                buildUIController.SetUIPosition(Input.mousePosition);
                
                if (TargetBuild != null && currentHovered != TargetBuild)
                    TargetBuild = currentHovered;
            }
            else
            {
                if (TargetBuild == null && currentHovered != TargetBuild)
                    TargetBuild = currentHovered;
            }
        }
        else {
            if (currentHovered != null && !buildUIController.IsHovered)
            {
                currentHovered.OnMouseExitCustom();
                currentHovered = null;
                buildUIController.gameObject.SetActive(false);
            }
        }
    }
}
