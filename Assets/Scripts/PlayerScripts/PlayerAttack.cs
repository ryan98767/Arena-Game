using UnityEngine;
using UnityEngine.InputSystem;
using PlayerMovementNameSpace;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtwAttack;

    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMovement playerMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack > 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }

    public void BasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("Attack1");
                timeBtwAttack = startTimeBtwAttack;
            }
        }
    }

    public void DealDamage() 
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(1);
            Debug.Log("Damaging enemy: " + enemiesToDamage[i].name + ", " + enemiesToDamage[i].GetComponent<Enemy>().CurrentHP + " remaining.");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
