using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImerUpdate : MonoBehaviour
{
    public waveManager manager;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = ((int)manager.waveTimer).ToString();
    }
}
