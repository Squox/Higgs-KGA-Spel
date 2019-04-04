using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int MaxHealth = 3;

    public int Health;

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        Health -= damage;
    }

    public bool IsPlayerRight()
    {
        if (PlayerPhysics.PlayerTransform.position.x > transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPlayerInRange(float rangeX, float rangeY = 0)
    {
        return Utility.IsInRange(PlayerPhysics.PlayerTransform, transform, rangeX, rangeY);
    }
}
