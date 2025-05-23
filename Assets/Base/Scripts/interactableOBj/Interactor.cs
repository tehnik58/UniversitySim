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
    private bool StabilizeInputMouseBtn0 = false;
    private bool StabilizeInputMouseBtn1 = false;

    void Start()
    {
        //CustomPoolStatic.CustomUpdateList += CustomUpdate;
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
                    print("OK");
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
        if (interactableObj != null && StabilizeInputMouseBtn0)
        {
            print($"Click: {interactableObj} from {this}");
            interactableObj?.OnClick();
        }
        else if (interactableObj == null && StabilizeInputMouseBtn0)
        {
            print("Emty Click");
            GlobalInteractEvent.OnEmptyClicked.Invoke();
        }
        if (interactableObj != null && StabilizeInputMouseBtn1)
        {
            print($"ClickRight: {interactableObj}");
            interactableObj?.OnRightClick();
        }
    }

    private void StabilizeMouseInput()
    {
        bool inputBtn = Input.GetMouseButtonDown(0);
        if ( inputBtn != StabilizeInputMouseBtn0 && !StabilizeInputMouseBtn0)
            StabilizeInputMouseBtn0 = true;
        else
            StabilizeInputMouseBtn0 = false;
        inputBtn = Input.GetMouseButtonDown(1);
        if ( inputBtn != StabilizeInputMouseBtn1 && !StabilizeInputMouseBtn1)
            StabilizeInputMouseBtn1 = true;
        else
            StabilizeInputMouseBtn1 = false;
    }
    private void Update()
    {
        StabilizeMouseInput();
        CheckCLickOnObject();
    }

    void OnDestroy()
    {
        CustomPoolStatic.CustomFixedUpdateList -= CustomFixedUpdate; 
        //CustomPoolStatic.CustomUpdateList -= CustomUpdate;
    }
}
