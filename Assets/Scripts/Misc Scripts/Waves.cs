using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Wave", menuName = "Configure Wave Set-up")]
public class Waves : ScriptableObject
{
    [FormerlySerializedAs("enemies")] public GameObject[] m_enemies;
}
