using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform m_player;
    void Update()
    {
        transform.LookAt(new Vector3(m_player.position.x,transform.position.y, m_player.position.z ));
    }
}
