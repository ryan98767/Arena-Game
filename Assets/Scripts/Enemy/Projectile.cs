using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int damage;
    protected Transform target;
    public virtual void Init(int dmg, Transform target, GameObject caster) 
    { 
        damage = dmg; 
        this.target = target;

    }

    protected void HitPlayer(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
