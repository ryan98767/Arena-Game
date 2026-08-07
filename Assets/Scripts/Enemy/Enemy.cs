using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHP = 10;
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected Transform attackPos;
    [SerializeField] protected float attackRange = 0.33f;
   
    protected Transform playerTransform;
    protected PlayerHealth playerHealth;
    protected bool chasingPlayer = true;
    protected bool facingRight = false;
    protected bool isAttacking = false;
    protected int currentHP;
    
    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        currentHP = maxHP;
        playerTransform = player.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public virtual void TakeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        anim.SetTrigger("Death");
    }

    protected Vector2 FindPlayer()
    {
        return (playerTransform.position - transform.position).normalized;
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    
    public void OnPlayerLost()
    {
        chasingPlayer = true;
        
    }

    public abstract void Attack();

    public virtual void DealDamage(int damage)
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

    public virtual void EndAttack()
    {
        Debug.Log("End attack");
        isAttacking = false;
    }
}
