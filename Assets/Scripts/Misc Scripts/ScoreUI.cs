using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [FormerlySerializedAs("images")] public Image[] m_images;
    [FormerlySerializedAs("greyedStar")] [FormerlySerializedAs("GreyedStar")] [SerializeField]
    Sprite m_greyedStar;
    [FormerlySerializedAs("coloredStar")] [FormerlySerializedAs("ColoredStar")] [SerializeField]
    Sprite m_coloredStar;

    [FormerlySerializedAs("rank")] public int m_rank = 1;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var img in m_images)
        {
            img.sprite = m_greyedStar;
        }

        foreach (var img in m_images)
        {
            img.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(var i = 0; i < m_images.Length; i++)
        {
            m_images[i].sprite = i < m_rank ? m_coloredStar : m_greyedStar;
        }
    }

    public void ShowImages()
    {
        foreach (var img in m_images)
        {
            img.enabled = true;
        }
    }

    public void HideImages()
    {
        foreach (var img in m_images)
        {
            img.enabled = false;
        }
    }
}
