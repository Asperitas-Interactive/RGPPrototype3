using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.CompareTag("Player"))
        {
            _collider.gameObject.GetComponent<PlayerMovement>().Heal(-10);
            
            // m_rankingSys.DropCombo();
            // m_damagesound.Play();
        }
    }
}
