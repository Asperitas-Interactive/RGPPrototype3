using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    [FormerlySerializedAs("detector")] public ShopDetection m_detector;

    public void CloseMenu()
    {
        m_detector.CloseMenus();
    }

    public void NextLevel()
    {
       gameManager.Instance.GameOver();
    }
}
