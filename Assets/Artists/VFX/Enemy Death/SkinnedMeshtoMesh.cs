using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class NewBehaviourScript : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMesh;
    public VisualEffect
        public float refreshRate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVFXGraph());
    }
    IEnumerator UpdateVFXGraph()
    {

    }
}

