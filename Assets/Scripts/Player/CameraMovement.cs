using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;

    private float cameraCap = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraCap -= mouseY;
        cameraCap = Mathf.Clamp(cameraCap, -90f, 90f);

        transform.localRotation = Quaternion.Euler(cameraCap, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
