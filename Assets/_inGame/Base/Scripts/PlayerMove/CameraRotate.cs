using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private RaycastHit _hit;
    
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private float      RotationSpeed;
    private float      _rotation;
/*
    void Start()
    {
        CustomPoolStatic.CustomFixedUpdateList += CustomFixedUpdate;
    }
    
    void CustomFixedUpdate()
    {
        if (Physics.Raycast(transform.position, _playerCamera.transform.forward, out _hit, 1000, LayerMask.GetMask("Ground")))
        {
        }
    }*/

    public void RotateFromUI(float horizontal)
    {
        GlobalInteractEvent.IsLockOnUI = horizontal != 0;
        print(horizontal);
        _rotation = RotationSpeed * horizontal;
    }

    public Vector3 GetPivotPoint()
    {
        return _hit.point;
    }
    void Update()
    {
        Vector3 preVector = transform.position - _hit.point;

        if (!GlobalInteractEvent.IsLockOnUI)
            _rotation = Input.GetKey(KeyCode.Q) ? RotationSpeed:
                Input.GetKey(KeyCode.E) ? -RotationSpeed  : 0;
        this.transform.RotateAround(_hit.point, Vector3.up,
            (GlobalInteractEvent.IsLockOnUI? (Input.GetMouseButton(0) ? _rotation: 0.0f): _rotation) * Time.deltaTime);

    
    }
}
