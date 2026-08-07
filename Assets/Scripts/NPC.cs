using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    protected bool playerInRange = false;
    [SerializeField] protected SpriteRenderer interactSprite;
    

    protected void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    protected virtual bool OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactSprite.enabled = true;
            playerInRange = true;
            return true;
        }
        return false;
    }

    protected virtual bool OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactSprite.enabled = false;
            playerInRange = false;
            return true;
        }
        return false;
    }

    public abstract void Interact();
}
