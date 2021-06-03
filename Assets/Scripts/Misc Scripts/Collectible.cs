using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   public TMP_Text m_msgHolder;

   public string m_message;

   private void Start()
   {
      m_msgHolder = GameObject.FindGameObjectWithTag("PickUpMessage").GetComponent<TMP_Text>();
   }

   private void OnTriggerEnter(Collider _collider)
   {
      if (_collider.CompareTag("Player"))
      {
         m_msgHolder.SetText(m_message);
         Invoke("Text", 4f);
      }
   }

   void Text()
   {
      m_msgHolder.SetText("");
      Destroy(this.gameObject);

   }
}


