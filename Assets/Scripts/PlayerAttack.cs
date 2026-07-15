using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    private float timeBtwAttack;

    [SerializeField] private float startTimeBtwAttack;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemies;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Debug.Log("Basic Attack");
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        Debug.Log("Struck enemy");
                    }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
