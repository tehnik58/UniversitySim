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
    private IInteractableObj interactableObj;

    void Start()
    {
        CustomPoolStatic.CustomUpdateList += CustomUpdate;
        CustomPoolStatic.CustomFixedUpdateList += CustomFixedUpdate;
    }
    void CustomFixedUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            IInteractableObj _interactableObj;
            if (hit.transform.gameObject.TryGetComponent<IInteractableObj>(out _interactableObj))
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
            interactableObj.OnClick();
        if (interactableObj != null && Input.GetMouseButtonDown(1))
            interactableObj.OnRightClick();
    }
    
    void CustomUpdate()
    {
        checkCLickOnObject();
    }

    void OnDestroy()
    {
        CustomPoolStatic.CustomFixedUpdateList -= CustomFixedUpdate;
        CustomPoolStatic.CustomUpdateList -= CustomUpdate;
    }
}
