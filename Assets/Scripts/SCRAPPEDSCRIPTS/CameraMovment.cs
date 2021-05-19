using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;
    [SerializeField]
    private Transform player;

    float xRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -45.0f, 45.0f);

        transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);

        player.Rotate(Vector3.up * mouseX);
    }
}
