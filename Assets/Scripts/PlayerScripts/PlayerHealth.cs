using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int currentHP;
    [SerializeField] private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
    }

    public int CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        anim.SetTrigger("Hurt");
        Debug.Log("Ow!");
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Handle player death (e.g., play animation, restart level)
        Debug.Log("Player died");
        anim.SetTrigger("Death");
    }
}
