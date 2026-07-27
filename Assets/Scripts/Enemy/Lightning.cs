using UnityEngine;

public class Lightning : Projectile
{
    [SerializeField] private BoxCollider2D hitboxTransform;
    [SerializeField] private float growDuration = 2f;
    [SerializeField] private Vector3 maxScale;

    private float timer = 0f;

    void Start()
    {
        maxScale = hitboxTransform.size * 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < growDuration)
        {
            timer += Time.deltaTime;
            float t = timer / growDuration;
            hitboxTransform.size = Vector3.Lerp(Vector3.zero, maxScale, t);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) => HitPlayer(collision);

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (hitboxTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, hitboxTransform.size);
        }
    }
}
