using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovment : MonoBehaviour
{
    [FormerlySerializedAs("sensitivity")] [SerializeField]
    private float m_sensitivity = 100f;
    [FormerlySerializedAs("player")] [SerializeField]
    private Transform m_player;

    float m_xRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;

        m_xRot -= mouseY;

        m_xRot = Mathf.Clamp(m_xRot, -45.0f, 45.0f);

        transform.localRotation = Quaternion.Euler(m_xRot, 0.0f, 0.0f);

        m_player.Rotate(Vector3.up * mouseX);
    }
}
