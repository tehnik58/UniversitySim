using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _inputVector;
    private Vector3 _moveVector = Vector3.zero;
    private bool IsHovered = false;
    
    [FormerlySerializedAs("MaxSpeed")] [SerializeField] private float _maxSpeed;
    [FormerlySerializedAs("MinSpeed")] [SerializeField] private float _minSpeed;
    [FormerlySerializedAs("SpeedUp")] [SerializeField] private float _speedUp;
    [FormerlySerializedAs("CameraPlayer")] [SerializeField] private GameObject _cameraPlayer;
    void Start()
    {
        _player = this.gameObject;
        _inputVector = Vector3.zero;
    }

    void CheckInput()
    {
        _inputVector.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ?  1 : 0;
        _inputVector.z = Input.GetKey(KeyCode.W) ?  1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        _inputVector.y = Input.GetKey(KeyCode.LeftControl) ?  1 : Input.GetKey(KeyCode.LeftShift) ? -1 : 0;
    }

    public void VerticalScreenTouch(float vertical)
    {
        _inputVector.z = vertical;
        IsHovered = vertical != 0;
    }
    
    public void HorizontalScreenTouch(float horizontal)
    {
        _inputVector.x = horizontal;
        IsHovered = horizontal != 0;
    }

    void MoveScore()
    {
        Vector3 moveVector = (
            _player.transform.right * _inputVector.x + 
                             _player.transform.forward * _inputVector.z + 
                             _cameraPlayer.transform.forward * _inputVector.y
            ).normalized * _speedUp;
        
            if (moveVector != Vector3.zero)
            {
                _moveVector = Vector3.Lerp(_moveVector, moveVector, _speedUp * Time.deltaTime);
            }
            else
            {
                _moveVector = Vector3.Lerp(_moveVector, Vector3.zero, _speedUp*10 * Time.deltaTime);
            }
            
            if (_moveVector.magnitude > _maxSpeed)
                _moveVector = _moveVector.normalized * _maxSpeed;
    }
    
    void Update()
    {
        if(!IsHovered)
            CheckInput();
        MoveScore();
        this.transform.position += _moveVector;
    }
}
