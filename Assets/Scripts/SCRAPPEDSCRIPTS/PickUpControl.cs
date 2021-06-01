using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUpControl : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("pickUps")] [SerializeField]
    private PickUps[] m_pickUps;

    private PickUps m_setPickup;

    [FormerlySerializedAs("source")] public AudioSource m_source;

    void Start()
    {
        m_setPickup = m_pickUps[Random.Range(0, m_pickUps.Length)];
        gameObject.GetComponent<MeshFilter>().mesh = m_setPickup.m_mMesh;
        gameObject.GetComponent<MeshRenderer>().material = m_setPickup.m_mMaterial;
        m_source = GameObject.FindGameObjectWithTag(m_setPickup.m_soundTag).GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.tag == "Player")
        {
            m_setPickup.PowerUp(_other.gameObject);
            m_source.Play();
            Destroy(this.gameObject);
        }
    }
}
