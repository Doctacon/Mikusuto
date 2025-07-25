using UnityEngine;
using UnityEngine.InputSystem;

namespace Mikusuto.Input
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;
        public static InputManager Instance => instance;
        
        private PlayerInputActions inputActions;
        
        [Header("Input Settings")]
        [SerializeField] private bool enableGamepadSupport = true;
        
        public PlayerInputActions InputActions => inputActions;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeInput();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void InitializeInput()
        {
            inputActions = new PlayerInputActions();
            
            if (enableGamepadSupport)
            {
                SetupGamepadSettings();
            }
        }
        
        void SetupGamepadSettings()
        {
            // Configure gamepad settings
            var movementAction = inputActions.Player.Movement;
            // Additional gamepad configuration can be added here if needed
        }
        
        void OnEnable()
        {
            if (inputActions != null)
            {
                inputActions.Enable();
            }
        }
        
        void OnDisable()
        {
            if (inputActions != null)
            {
                inputActions.Disable();
            }
        }
        
        void OnDestroy()
        {
            if (inputActions != null)
            {
                inputActions.Dispose();
            }
        }
        
        // Helper methods for accessing input values
        public Vector2 GetMovementInput()
        {
            return inputActions.Player.Movement.ReadValue<Vector2>();
        }
        
        public bool GetInteractPressed()
        {
            return inputActions.Player.Interact.WasPressedThisFrame();
        }
        
        public bool GetPausePressed()
        {
            return inputActions.Player.Pause.WasPressedThisFrame();
        }
        
        // Method to switch between control schemes
        public void SwitchToKeyboard()
        {
            inputActions.bindingMask = InputBinding.MaskByGroup("Keyboard");
        }
        
        public void SwitchToGamepad()
        {
            inputActions.bindingMask = InputBinding.MaskByGroup("Gamepad");
        }
        
        public void EnableAllInputs()
        {
            inputActions.bindingMask = null; // Enable all inputs
        }
        
        // Methods for temporarily disabling input (during cutscenes, etc.)
        public void DisablePlayerInput()
        {
            inputActions.Player.Disable();
        }
        
        public void EnablePlayerInput()
        {
            inputActions.Player.Enable();
        }
        
        public void DisableUIInput()
        {
            inputActions.UI.Disable();
        }
        
        public void EnableUIInput()
        {
            inputActions.UI.Enable();
        }
    }
}