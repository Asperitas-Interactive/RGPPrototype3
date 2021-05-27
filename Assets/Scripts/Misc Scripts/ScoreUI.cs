using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Image[] images;
    [SerializeField]
    Sprite GreyedStar;
    [SerializeField]
    Sprite ColoredStar;

    public int rank = 1;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Image img in images)
        {
            img.sprite = GreyedStar;
        }

        foreach (Image img in images)
        {
            img.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(i < rank)
            {
                images[i].sprite = ColoredStar;
            } else
            {
                images[i].sprite = GreyedStar;
            }
        }
    }

    public void ShowImages()
    {
        foreach (Image img in images)
        {
            img.enabled = true;
        }
    }

    public void HideImages()
    {
        foreach (Image img in images)
        {
            img.enabled = false;
        }
    }
}
