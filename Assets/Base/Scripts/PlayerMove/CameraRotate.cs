using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private RaycastHit _hit;
    
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private float      RotationSpeed;
    private float      _rotation;

    public void RotateFromUI(float horizontal)
    {
        print(horizontal);
        _rotation = RotationSpeed * horizontal;
    }
    void Update()
    {
        Vector3 preVector = transform.position - _hit.point;
        
        _rotation = Input.GetKey(KeyCode.Q) ? RotationSpeed:
            Input.GetKey(KeyCode.E) ? -RotationSpeed  : 0;
        this.transform.RotateAround(_hit.point, Vector3.up,
            _rotation * Time.deltaTime);
    }
}
