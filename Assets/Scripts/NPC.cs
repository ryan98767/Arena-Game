using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{
    private bool playerInRange = false;
    [SerializeField] private SpriteRenderer interactSprite;
    

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    private bool OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactSprite.enabled = true;
            playerInRange = true;
            return true;
        }
        return false;
    }

    private bool OnTriggerExit2D(Collider2D collision)
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
