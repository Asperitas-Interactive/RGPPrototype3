using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image viginette;
    bool fade = false;
    // Start is called before the first frame update
    void Start()
    {
        viginette = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fade == true)
        {
            viginette.color = new Color(viginette.color.r, viginette.color.g, viginette.color.b, viginette.color.a + (0.5f * Time.deltaTime));
        }

        Debug.Log(viginette.color.a);

        if(viginette.color.a > 1.0f)
        {
            GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>().gameLost();
        }
    }

    public void beginFade()
    {
        fade = true;
    }
}
