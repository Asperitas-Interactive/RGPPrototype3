using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Image[] images = new Image[5];
    int rank = 1;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Image img in images)
        {
            img.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < rank; i++)
        {
            images[i].enabled = true;
        }
    }
}
