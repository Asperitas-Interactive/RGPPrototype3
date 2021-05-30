using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TImerUpdate : MonoBehaviour
{
    [FormerlySerializedAs("manager")] public waveManager m_manager;

    // Update is called once per frame
    void Update()
    {
        //if (manager.restWave)
        //    this.GetComponent<Text>().text = ((int)manager.waveTimer).ToString();
        //else
        //    this.GetComponent<Text>().text = (manager.boidCount).ToString();
    }
}
