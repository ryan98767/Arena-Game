using UnityEngine;

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

    private float timeBtwAttack;
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
            anim.SetFloat("AnimState", 1f);
            if (isAttacking) 
            {
                if (timeBtwAttack > 0)
                {
                    timeBtwAttack -= Time.deltaTime;
                }

                if (timeBtwAttack <= 0)
                {
                    timeBtwAttack = attackSpd;
                    Attack();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasingPlayer = false;
            rb.linearVelocity = Vector2.zero;
            isAttacking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasingPlayer = true;
        }
    }

    public void Attack()
    {
        Collider2D playerToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange, LayerMask.GetMask("Player"));
        if (playerToDamage != null)
        {
            Debug.Log("Starting attack!");
            anim.SetTrigger("Attack");
            isAttacking = false;
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
}
