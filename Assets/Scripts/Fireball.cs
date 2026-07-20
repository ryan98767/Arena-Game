using UnityEngine;

public class Fireball : Projectile
{
    [SerializeField] private float speed = 8f;
    private Vector2 direction;
    private float lifetime = 5f;

    public override void Init(int dmg, Transform target, GameObject caster)
    {
        base.Init(dmg, target, caster);
        direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle/180);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) => HitPlayer(collision);

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    
}
