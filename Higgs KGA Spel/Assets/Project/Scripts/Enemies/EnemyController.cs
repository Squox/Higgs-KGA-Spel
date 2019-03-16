using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public int MaxHealth = 3;

    public int Health;

    private GameObject player;

    private void Awake()
    {
        Health = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int damage = 1)
    {
        Health -= damage;
    }

    public bool IsPlayerRight()
    {
        if (player.transform.position.x > transform.position.x)
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
        if (rangeY == 0)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < rangeX)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < rangeX && Mathf.Abs(player.transform.position.y - transform.position.y) < rangeY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }       
    }
}
