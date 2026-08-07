using UnityEngine;
using PlayerState;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int currentHP;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerStates stateManager;

    private bool isBlocking = false;
    private bool wasBlocking = false;

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

    public bool IsBlocking
    {
        get { return isBlocking; }
        set { isBlocking = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlocking && !wasBlocking) 
        {
            Debug.Log("starting block");
            anim.SetTrigger("Block");
            anim.SetBool("IdleBlock", true);
            if (stateManager.CurrentState != States.InDialogue)
                stateManager.SetState(States.Blocking);

        }
        else if (!isBlocking && wasBlocking)
        { 
            anim.SetBool("IdleBlock", false);
            if (stateManager.CurrentState == States.Blocking)
                stateManager.SetState(States.Normal);
        }
        wasBlocking = isBlocking;
    }

    public void TakeDamage(int damage)
    {
        if (!isBlocking)
        {
            currentHP -= damage;
            anim.SetTrigger("Hurt");
            Debug.Log("Ow!");
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        // Handle player death (e.g., play animation, restart level)
        Debug.Log("Player died");
        anim.SetTrigger("Death");
    }
}
