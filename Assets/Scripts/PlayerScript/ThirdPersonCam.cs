using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ThirdPersonCam : MonoBehaviour
{
    [FormerlySerializedAs("sensitivity")] [SerializeField]
    private float m_sensitivity = 100f;
    [FormerlySerializedAs("player")] [SerializeField]
    private Transform m_player;
    GameObject m_temp;

    private CharacterController m_playerCont;

    bool m_turning;

    float m_xRot = 0f;
    float m_yRot = 90f;

    ShopDetection m_shopCheck;

    // Start is called before the first frame update
    void Start()
    {
        m_temp = new GameObject();

        m_temp.transform.position = m_player.transform.position;
        m_temp.transform.rotation = m_player.transform.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        transform.parent.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        m_playerCont = m_player.GetComponent<CharacterController>();
        m_shopCheck = GameObject.FindGameObjectWithTag("ShopEvent").GetComponent<ShopDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_shopCheck.m_inMenu) {
            float mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;

            m_xRot += mouseY;

            m_xRot = Mathf.Clamp(m_xRot, 0.0f, 80.0f);

            m_yRot += mouseX;
            //transform.parent.transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);

        //transform.RotateAround(transform.parent.transform.position, Vector3.right, -mouseY);
        //player.Rotate(Vector3.up, mouseX);
        //transform.parent.Rotate(Vector3.up, mouseX);
        
        // transform.parent.localRotation = Quaternion.Euler(xRot, yRot, 0.0f);
        //         m_temp.transform.Rotate(Vector3.up, mouseX);

        // if(player.gameObject.GetComponent<PlayerMovement>().velZ > 0.01f || m_turning)
        // {
        //      player.rotation = m_temp.transform.rotation;
        // }

           
            
            //transform.RotateAround(transform.parent.transform.position, Vector3.right, -mouseY);
            //player.Rotate(Vector3.up, mouseX);
            //transform.parent.Rotate(Vector3.up, mouseX);
            transform.parent.localRotation = Quaternion.Euler(m_xRot, m_yRot, 0.0f);
            //player.Rotate(Vector3.up, mouseX);
        
        }
    }


    void LateUpdate()
    {
        transform.parent.position = m_player.transform.position;
    }
}
