using UnityEngine;
using System.Collections;

public class BossEnemy : Enemy
{

    [SerializeField] private GameObject[] bossAttacks;
    [SerializeField] private float castTime = 0.75f;
    [SerializeField] private float castSpd = 5f;

    private float timeBtwCast;
    private bool spacing = false;

    // Update is called once per frame
    void Update()
    {
        if (chasingPlayer)
        {
            rb.linearVelocity = new Vector2(FindPlayer().x * moveSpeed, rb.linearVelocity.y);

            if ((FindPlayer().x > 0.01f && !facingRight) || (FindPlayer().x < -0.01f && facingRight))
            {
                Flip();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isAttacking)
            {
                if (timeBtwCast <= 0)
                {
                    Debug.Log("Ranged Attacking!");
                    timeBtwCast = castSpd;
                    RangedAttack();
                }
                else
                {
                    timeBtwCast -= Time.deltaTime;
                }
            }
        }
    }

    public void RangedAttack()
    {
        int randomAttack = Random.Range(1, bossAttacks.Length);
        switch (randomAttack)
        {
            case 1:
                anim.SetTrigger("Fireball");
                StartCoroutine(CastAfterDelay(randomAttack, attackPos.position));
                break;
            case 2:
                anim.SetTrigger("Lightning");
                Vector2 cloudHeight = new Vector2(playerTransform.position.x + Random.Range(-5, 5), -2);
                StartCoroutine(CastAfterDelay(randomAttack, cloudHeight));
                break;
        }
    }

    public override void Attack()
    {
        anim.SetTrigger("Melee");
        StartCoroutine(CastAfterDelay(0, attackPos.position));
    }

    protected IEnumerator CastAfterDelay(int attackIndex, Vector2 spawnPos)
    {
        yield return new WaitForSeconds(castTime);

        GameObject proj = Instantiate(bossAttacks[attackIndex], spawnPos, Quaternion.identity);
        Projectile projScript = proj.GetComponent<Projectile>();
        projScript.Init(1, playerTransform, gameObject);
    }
}
