using UnityEngine;

public class MobEnemy : Enemy
{
    [SerializeField] private float attackSpd = 1f;
    private float timeBtwAttack;
    private Vector2 directionToPlayer;

    void Update()
    {
        directionToPlayer = FindPlayer();
        
        if ((directionToPlayer.x > 0.01f && !facingRight) || (directionToPlayer.x < -0.01f && facingRight))
        {
            Flip();
        }

        //Debug.Log("Chasing Player: " + chasingPlayer + ", Is Attacking: " + isAttacking + ", Time Between Attack: " + timeBtwAttack + ", Facing Right: " + facingRight + ", Player Position: " + FindPlayer());
        if (chasingPlayer)
        {
            anim.SetBool("IsRunning", true);
            rb.linearVelocity = new Vector2(directionToPlayer.x * moveSpeed, rb.linearVelocity.y);

            
        }
        else
        {
            anim.SetBool("IsRunning", false);
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

    public override void Attack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag + ", Is Attacking: " + isAttacking + ", Chasing Player: " + chasingPlayer);
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
            isAttacking = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            directionToPlayer = FindPlayer();
        }
    }
}
