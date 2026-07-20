using UnityEngine;

public class MeleeProjectile : Projectile
{
    private bool canDamage = true;
    private void OnTriggerEnter2D(Collider2D collision) => HitPlayer(collision);

    public void EnableDamage() => canDamage = true;
    public void DisableDamage() => canDamage = false;

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
