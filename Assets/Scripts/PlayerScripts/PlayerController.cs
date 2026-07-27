using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravity = -9.8f;

    private InputAction m_moveAction;
    private InputAction m_jumpAction;

    private Vector3 up;
    private Animator animator;

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        m_moveAction = InputActions.FindAction("Move");
        m_jumpAction = InputActions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
