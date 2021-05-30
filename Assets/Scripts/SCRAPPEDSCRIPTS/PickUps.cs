using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Pickup", menuName = "Pickups")]
public class PickUps : ScriptableObject
{
    public enum Type
    {
        Health,
        HealthUp,
        AttackUp
    }

    [FormerlySerializedAs("pickUpType")] public Type m_pickUpType;

    //public Material material;
    [FormerlySerializedAs("mMesh")] public Mesh m_mMesh;
    [FormerlySerializedAs("mMaterial")] public Material m_mMaterial;
    [FormerlySerializedAs("sound")] public AudioClip m_sound;

    [FormerlySerializedAs("soundTag")] public string m_soundTag;

    [FormerlySerializedAs("increase")] public int m_increase;

    public void PowerUp(GameObject _gameObject)
    {
        switch (m_pickUpType)
        {
            case Type.Health:
                if(_gameObject.GetComponent<PlayerMovement>() != null)
                {
                    _gameObject.GetComponent<PlayerMovement>().Heal(m_increase);
                }
                break;
            case Type.HealthUp:
                if (_gameObject.GetComponent<PlayerMovement>() != null)
                {
                    _gameObject.GetComponent<PlayerMovement>().MAXHealthUp(m_increase);
                }
                break;
            case Type.AttackUp:
                if (_gameObject.GetComponent<CombatControl>() != null)
                {
                    _gameObject.GetComponent<CombatControl>().DamageBoost(m_increase);
                }
                break;
            default:
                break;
        }
    }
}
