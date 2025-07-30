using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float edgeScrollSpeed = 5f;
    public float rotationSpeed = 50f;
    public float zoomSpeed = 20f;

    [Header("Limits")]
    public float minZoom = 5f;
    public float maxZoom = 50f;
    public float paddingMoveUI = 5f;

    private Vector3 dragOrigin;
    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    void HandleMovement()
    {
        // WASD
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(x, 0, z), Space.Self);

        // Edge Scrolling (опционально)
        Vector3 move = Vector3.zero;
        if (Input.mousePosition.x <= paddingMoveUI) move += Vector3.left;
        if (Input.mousePosition.x >= Screen.width - paddingMoveUI) move += Vector3.right;
        if (Input.mousePosition.y <= paddingMoveUI) move += Vector3.forward * -1;
        if (Input.mousePosition.y >= Screen.height - paddingMoveUI) move += Vector3.forward;
        transform.Translate(move.normalized * edgeScrollSpeed * Time.deltaTime);

            // Drag Pan (средн€€ кнопка мыши)
        if (Input.GetMouseButtonDown(2)) dragOrigin = Input.mousePosition;
        if (Input.GetMouseButton(2))
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            transform.Translate(new Vector3(difference.x, 0, difference.y), Space.World);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 tr = transform.position + Camera.main.transform.forward * scroll * zoomSpeed * 100 * Time.deltaTime;
        if (tr.magnitude > minZoom && tr.magnitude < maxZoom)
            transform.position = tr;
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E)) transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }
}
