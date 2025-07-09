using UnityEngine;

public class ObjectInteractionController : MonoBehaviour
{
    private Camera mainCamera;
    private InteractableObject currentHovered;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float raycastDistance;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }
    
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
                if (currentHovered != null) currentHovered.
                    OnMouseEnterCustom();
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