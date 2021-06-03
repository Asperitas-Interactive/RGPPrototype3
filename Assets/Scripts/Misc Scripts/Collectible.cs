using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
   public TMP_Text m_msgHolder;

   public string m_message;
   public UnityEvent m_OnTrigger;
   
   private void Start()
   {
      m_msgHolder = GameObject.FindGameObjectWithTag("PickUpMessage").GetComponent<TMP_Text>();
   }

   private void OnTriggerEnter(Collider _collider)
   {
      if (_collider.CompareTag("Player"))
      {
         m_OnTrigger?.Invoke();
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


