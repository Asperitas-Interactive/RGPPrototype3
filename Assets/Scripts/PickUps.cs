using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickup", menuName = "Pickups")]
public class PickUps : ScriptableObject
{
    public enum Type
    {
        Health,
        HealthUp,
        AttackUp
    }

    public Type pickUpType;

    public Material material;

    public int increase;

    public void powerUp(GameObject gameObject)
    {
        switch (pickUpType)
        {
            case Type.Health:
                if(gameObject.GetComponent<PlayerMovement>() != null)
                {
                    gameObject.GetComponent<PlayerMovement>().Heal(increase);
                }
                break;
            case Type.HealthUp:
                if (gameObject.GetComponent<PlayerMovement>() != null)
                {
                    gameObject.GetComponent<PlayerMovement>().MaxHealthUp(increase);
                }
                break;
            case Type.AttackUp:
                if (gameObject.GetComponent<CombatControl>() != null)
                {
                    gameObject.GetComponent<CombatControl>().DamageBoost(increase);
                }
                break;
            default:
                break;
        }
    }
}
