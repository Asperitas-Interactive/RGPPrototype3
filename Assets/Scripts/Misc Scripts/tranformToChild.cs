using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tranformToChild : MonoBehaviour
{

    Vector3 m_movePos;
    // Start is called before the first frame update
    void Start()
    {
        m_movePos = new Vector3();
    }
    private void OnEnable()
    {
        m_movePos = new Vector3(transform.GetChild(5).localPosition.y, 0f, 0f);
    }

    private void OnDisable()
    {
        //transform.position += movePos;
        //transform.GetChild(5).position = new Vector3(0f, 1.4f, -0.11f);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
