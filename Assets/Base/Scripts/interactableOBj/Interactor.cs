using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPlayer;
    [SerializeField] private Camera _camera;
    [SerializeField] private float distance;
    
    private RaycastHit hit;
    private IInteractableObj interactableObj;

    public IInteractableObj selectedObj;

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
                if (interactableObj != _interactableObj)
                {
                    interactableObj = _interactableObj;
                    interactableObj.Interact();
                }
            }
            else if (interactableObj != null)
            {
                interactableObj.DeInteract();
                interactableObj = null;
            }
        }
    }

    private void CheckCLickOnObject()
    {
        if (interactableObj != null && Input.GetMouseButtonDown(0))
        {
            interactableObj.OnClick();
        }
        else if (interactableObj != null && Input.GetMouseButtonDown(1))
        {
            interactableObj.OnRightClick();
        }
        else if (interactableObj == null && Input.GetMouseButtonDown(0))
        {
            print("Emty Click");
            GlobalInteractEvent.OnEmptyClicked.Invoke();
        }
                
    }

    private void CustomUpdate()
    {
        CheckCLickOnObject();
    }

    void OnDestroy()
    {
        CustomPoolStatic.CustomFixedUpdateList -= CustomFixedUpdate;
        CustomPoolStatic.CustomUpdateList -= CustomUpdate;
    }
}
