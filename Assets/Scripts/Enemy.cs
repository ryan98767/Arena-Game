using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHP = 10;
    [SerializeField] Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackSpd = 1f;
    [SerializeField] private float castSpd = 5f;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private GameObject[] bossAttacks;
    [SerializeField] private float castTime = 0.75f;

    private float timeBtwAttack;
    private float timeBtwCast;
    private Transform playerTransform;
    private PlayerHealth playerHealth;
    private bool chasingPlayer = true;
    private bool facingRight = false;
    private bool isAttacking = false;


    private int currentHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        playerTransform = player.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (chasingPlayer)
        {
            anim.SetBool("IsRunning", true);
            rb.linearVelocity = new Vector2(FindPlayer().x * moveSpeed, rb.linearVelocity.y);

            if ((FindPlayer().x > 0.01f && !facingRight) || (FindPlayer().x < -0.01f && facingRight))
            {
                Flip();
            }
        }
        else
        {
            anim.SetBool("IsRunning", false);
            anim.SetInteger("AnimState", 1);
            if (isAttacking) 
            {
                if (timeBtwAttack > 0)
                {
                    timeBtwAttack -= Time.deltaTime;
                }

                if (timeBtwAttack <= 0)
                {
                    Collider2D playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, LayerMask.GetMask("Player"));
                    if (playerToDamage)
                    {
                        timeBtwAttack = attackSpd;
                        Attack();
                    }
                    
                }
                
            }
        }
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle enemy death (e.g., play animation, destroy object)
        anim.SetTrigger("Death");
        Debug.Log("Enemy died");
    }

    private Vector2 FindPlayer()
    {
        Vector2 playerDir = playerTransform.position - transform.position;
        playerDir = playerDir.normalized;
        return playerDir;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void OnPlayerDetected()
    {
        chasingPlayer = false;
        rb.linearVelocity = Vector2.zero;
        isAttacking = true;
    }

    public void OnPlayerStay()
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
    public void OnPlayerLost()
    {
        chasingPlayer = true;
        
    }

    public void Attack()
    {
        Collider2D playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, LayerMask.GetMask("Player"));
        if (!isBoss)
        {
            if (playerToDamage != null)
            {
                Debug.Log("Starting attack!");
                anim.SetTrigger("Attack");
                isAttacking = false;
            }
        }
        else if (isBoss)
        {
            anim.SetTrigger("Melee");
            StartCoroutine(CastAfterDelay(0, attackPos.position));
        }
    }


    public void RangedAttack()
    {
        int randomAttack = Random.Range(1, bossAttacks.Length);
        Debug.Log("Starting boss attack: " + randomAttack.ToString());
        switch (randomAttack)
        {
            case 1:
                anim.SetTrigger("Fireball");
                StartCoroutine(CastAfterDelay(randomAttack, attackPos.position));
                break;
            case 2:
                anim.SetTrigger("Lightning");
                Vector2 cloudHeight = new Vector2(playerTransform.position.x + Random.Range(-5, 5), -1);
                StartCoroutine(CastAfterDelay(randomAttack, cloudHeight));
                break;
            default:
                break;
        }
    }

    public void DealDamage(int damage)
    {
        Collider2D playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, LayerMask.GetMask("Player"));
        if (playerToDamage != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void EndAttack()
    {
        Debug.Log("End attack");
        isAttacking = true;
    }

    private IEnumerator CastAfterDelay(int attackIndex, Vector2 spawnPos)
    {
        yield return new WaitForSeconds(castTime);

        GameObject proj = Instantiate(bossAttacks[attackIndex], spawnPos, Quaternion.identity);
        Projectile projScript = proj.GetComponent<Projectile>();
        projScript.Init(1, playerTransform, gameObject);
    }
}
