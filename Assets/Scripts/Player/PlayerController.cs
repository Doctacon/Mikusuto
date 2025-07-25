using UnityEngine;
using Mikusuto.Input;

namespace Mikusuto.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float interactionRange = 2f;
        
        private Rigidbody2D rb;
        private Vector2 movement;
        private Animator animator;
        private InputManager inputManager;
        
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            inputManager = InputManager.Instance;
            
            if (inputManager == null)
            {
                Debug.LogError("InputManager not found! Make sure InputManager is in the scene.");
            }
        }
        
        void Update()
        {
            if (inputManager == null) return;
            
            // Get movement input from new Input System
            movement = inputManager.GetMovementInput();
            
            if (animator != null)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }
            
            // Check for interaction input
            if (inputManager.GetInteractPressed())
            {
                CheckForInteractables();
            }
        }
        
        void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        
        void CheckForInteractables()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
            
            foreach (Collider2D collider in colliders)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    break;
                }
            }
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
    }
}