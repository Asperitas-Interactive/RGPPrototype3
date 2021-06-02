using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   public TMP_Text m_msgHolder;

   public string m_message;
   private void OnTriggerEnter(Collider _collider)
   {
      if (_collider.CompareTag("Player"))
      {
         m_msgHolder.SetText(m_message);
      }
   }
}


