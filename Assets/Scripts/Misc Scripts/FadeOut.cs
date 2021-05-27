using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [FormerlySerializedAs("viginette")] public Image m_viginette;
    bool m_fade = false;
    // Start is called before the first frame update
    void Start()
    {
        m_viginette = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fade == true)
        {
            m_viginette.color = new Color(m_viginette.color.r, m_viginette.color.g, m_viginette.color.b, m_viginette.color.a + (0.5f * Time.deltaTime));
        }

        if(m_viginette.color.a > 1.0f)
        {
            gameManager.Instance.GameLost();
        }
    }

    public void BeginFade()
    {
        m_fade = true;
    }
}
