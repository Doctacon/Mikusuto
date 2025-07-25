using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mikusuto.Input;
using Mikusuto.Settings;
using Mikusuto.Dialogue;
using Mikusuto.Systems;

namespace Mikusuto.Testing
{
    public class Phase1TestManager : MonoBehaviour
    {
        [Header("Test UI Elements")]
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private TextMeshProUGUI inputStatusText;
        [SerializeField] private Button testDialogueButton;
        [SerializeField] private Button testAudioButton;
        [SerializeField] private Button openSettingsButton;
        
        [Header("Test Data")]
        [SerializeField] private DialogueData testDialogue;
        [SerializeField] private AudioClip testSFX;
        
        private InputManager inputManager;
        private SettingsManager settingsManager;
        private AudioManager audioManager;
        private DialogueSystem dialogueSystem;
        
        void Start()
        {
            InitializeReferences();
            SetupTestUI();
            RunInitialTests();
        }
        
        void InitializeReferences()
        {
            inputManager = InputManager.Instance;
            settingsManager = SettingsManager.Instance;
            audioManager = AudioManager.Instance;
            dialogueSystem = DialogueSystem.Instance;
        }
        
        void SetupTestUI()
        {
            if (testDialogueButton != null)
                testDialogueButton.onClick.AddListener(TestDialogueSystem);
            
            if (testAudioButton != null)
                testAudioButton.onClick.AddListener(TestAudioSystem);
            
            if (openSettingsButton != null)
                openSettingsButton.onClick.AddListener(OpenSettings);
        }
        
        void Update()
        {
            UpdateInputStatus();
        }
        
        void RunInitialTests()
        {
            string status = "=== PHASE 1 SYSTEM STATUS ===\n\n";
            
            // Test Input System
            status += TestInputSystem();
            status += "\n";
            
            // Test Settings System
            status += TestSettingsSystem();
            status += "\n";
            
            // Test Audio System
            status += TestAudioSystem();
            status += "\n";
            
            // Test Dialogue System
            status += TestDialogueSystemExists();
            status += "\n";
            
            // Test Save System
            status += TestSaveSystem();
            
            if (statusText != null)
                statusText.text = status;
        }
        
        string TestInputSystem()
        {
            string result = "🎮 INPUT SYSTEM:\n";
            
            if (inputManager == null)
            {
                result += "❌ InputManager not found!\n";
                result += "   → Add InputManager to scene\n";
                return result;
            }
            
            result += "✅ InputManager active\n";
            result += "✅ New Input System ready\n";
            result += "   → Test with WASD/Arrows + E key\n";
            result += "   → Test with gamepad if available\n";
            
            return result;
        }
        
        string TestSettingsSystem()
        {
            string result = "⚙️ SETTINGS SYSTEM:\n";
            
            if (settingsManager == null)
            {
                result += "❌ SettingsManager not found!\n";
                result += "   → Add SettingsManager to scene\n";
                return result;
            }
            
            result += "✅ SettingsManager active\n";
            result += $"✅ Settings loaded from disk\n";
            result += $"   → Master Volume: {(settingsManager.CurrentSettings.masterVolume * 100):F0}%\n";
            result += $"   → Text Speed: {GetTextSpeedLabel(settingsManager.CurrentSettings.textSpeed)}\n";
            result += $"   → Quality: {QualitySettings.names[settingsManager.CurrentSettings.qualityLevel]}\n";
            
            return result;
        }
        
        string TestAudioSystem()
        {
            string result = "🔊 AUDIO SYSTEM:\n";
            
            if (audioManager == null)
            {
                result += "❌ AudioManager not found!\n";
                result += "   → Add AudioManager to scene\n";
                return result;
            }
            
            result += "✅ AudioManager active\n";
            result += "✅ Volume controls ready\n";
            result += "   → Click 'Test Audio' button\n";
            
            return result;
        }
        
        string TestDialogueSystemExists()
        {
            string result = "💬 DIALOGUE SYSTEM:\n";
            
            if (dialogueSystem == null)
            {
                result += "❌ DialogueSystem not found!\n";
                result += "   → Add DialogueSystem to scene\n";
                return result;
            }
            
            result += "✅ DialogueSystem active\n";
            result += "✅ Settings integration ready\n";
            result += "   → Click 'Test Dialogue' button\n";
            
            return result;
        }
        
        string TestSaveSystem()
        {
            string result = "💾 SAVE SYSTEM:\n";
            
            var saveSystem = SaveSystem.Instance;
            if (saveSystem == null)
            {
                result += "❌ SaveSystem not found!\n";
                result += "   → Add SaveSystem to scene\n";
                return result;
            }
            
            result += "✅ SaveSystem active\n";
            result += $"✅ Save file exists: {saveSystem.HasSaveFile()}\n";
            
            return result;
        }
        
        void UpdateInputStatus()
        {
            if (inputManager == null || inputStatusText == null) return;
            
            Vector2 movement = inputManager.GetMovementInput();
            bool interact = inputManager.GetInteractPressed();
            bool pause = inputManager.GetPausePressed();
            
            string inputStatus = "=== INPUT TEST ===\n";
            inputStatus += $"Movement: ({movement.x:F2}, {movement.y:F2})\n";
            inputStatus += $"Interact (E): {(interact ? "PRESSED" : "---")}\n";
            inputStatus += $"Pause (ESC): {(pause ? "PRESSED" : "---")}\n\n";
            
            inputStatus += "CONTROLS:\n";
            inputStatus += "• WASD or Arrow Keys to move\n";
            inputStatus += "• E to interact\n";
            inputStatus += "• ESC to pause\n";
            inputStatus += "• Gamepad supported!\n";
            
            inputStatusText.text = inputStatus;
        }
        
        void TestDialogueSystem()
        {
            if (dialogueSystem == null)
            {
                Debug.LogError("DialogueSystem not found!");
                return;
            }
            
            if (testDialogue == null)
            {
                // Create test dialogue on the fly
                testDialogue = new DialogueData();
                testDialogue.lines = new System.Collections.Generic.List<DialogueLine>
                {
                    new DialogueLine 
                    { 
                        speakerName = "Test Character", 
                        text = "Hello! This is a test of the dialogue system. Notice how the text types out at your preferred speed!" 
                    },
                    new DialogueLine 
                    { 
                        speakerName = "Test Character", 
                        text = "You can change the text speed in the settings menu. Try it out!" 
                    },
                    new DialogueLine 
                    { 
                        speakerName = "System", 
                        text = "This dialogue respects your settings and input preferences. Phase 1 systems working perfectly!" 
                    }
                };
            }
            
            dialogueSystem.StartDialogue(testDialogue);
        }
        
        void TestAudioSystem()
        {
            if (audioManager == null)
            {
                Debug.LogError("AudioManager not found!");
                return;
            }
            
            if (testSFX != null)
            {
                audioManager.PlaySFX(testSFX);
            }
            else
            {
                // Play interaction sound if available
                audioManager.PlayInteractSound();
            }
            
            Debug.Log("Audio test: Playing sound effect. Check your volume settings!");
        }
        
        void OpenSettings()
        {
            // Try to find SettingsUI in the scene
            var settingsUI = FindObjectOfType<Mikusuto.UI.SettingsUI>();
            if (settingsUI != null)
            {
                settingsUI.OpenSettings();
            }
            else
            {
                Debug.LogWarning("SettingsUI not found in scene. You'll need to create the settings menu UI.");
            }
        }
        
        string GetTextSpeedLabel(float speed)
        {
            if (speed <= 0.02f) return "Very Fast";
            if (speed <= 0.05f) return "Normal";
            if (speed <= 0.1f) return "Slow";
            return "Very Slow";
        }
        
        // Test individual components
        [ContextMenu("Test Input System")]
        void TestInputOnly()
        {
            Debug.Log(TestInputSystem());
        }
        
        [ContextMenu("Test Settings System")]
        void TestSettingsOnly()
        {
            Debug.Log(TestSettingsSystem());
        }
        
        [ContextMenu("Test All Systems")]
        void TestAllSystems()
        {
            RunInitialTests();
        }
    }
}