using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private RaycastHit _hit;
    private bool IsClick = false;
    
    [SerializeField] private bool IsDegugMode;
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private GameObject _debugCentrObj;
    [SerializeField] private float      RotationSpeed;

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, _playerCamera.transform.forward, out _hit, 1000, LayerMask.GetMask("Ground")))
        {
        }
    }
    void Update()
    {
        if (IsDegugMode)
            _debugCentrObj.transform.position = _hit.point;
        Vector3 preVector = transform.position - _hit.point;
        
        if (Input.GetMouseButtonDown(2))
        {
            IsClick = true;
        }else if (Input.GetMouseButtonUp(2))
            IsClick = false;
        
        this.transform.RotateAround(_hit.point, Vector3.up, Input.GetKey(KeyCode.Q)? RotationSpeed * Time.deltaTime: Input.GetKey(KeyCode.E)? - RotationSpeed * Time.deltaTime: 0);
    }
}
