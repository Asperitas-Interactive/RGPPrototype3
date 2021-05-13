using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 100f;
    [SerializeField]
    private Transform player;

    private CharacterController m_playerCont;

    float xRot = 0f;
    float yRot = 90f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.parent.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        m_playerCont = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot += mouseY;

        xRot = Mathf.Clamp(xRot, 0.0f, 80.0f);

        yRot += mouseX;
        //transform.parent.transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);

        //transform.RotateAround(transform.parent.transform.position, Vector3.right, -mouseY);
        //player.Rotate(Vector3.up, mouseX);
        //transform.parent.Rotate(Vector3.up, mouseX);
        transform.parent.localRotation = Quaternion.Euler(xRot, yRot, 0.0f);
        player.Rotate(Vector3.up, mouseX);
    }

    private void LateUpdate()
    {
        transform.parent.position = player.transform.position;
    }
}
