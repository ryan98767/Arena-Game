using UnityEngine;

public class MeleeProjectile : Projectile
{
    protected bool canDamage = true;
    protected void OnTriggerEnter2D(Collider2D collision) => HitPlayer(collision);

    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
