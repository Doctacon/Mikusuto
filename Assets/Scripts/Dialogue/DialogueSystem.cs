using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mikusuto.Settings;

namespace Mikusuto.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private Text nameText;
        [SerializeField] private Text dialogueText;
        [SerializeField] private Button continueButton;
        
        [Header("Dialogue Settings")]
        [SerializeField] private float defaultTypingSpeed = 0.05f;
        
        private Queue<DialogueLine> lines;
        private bool isTyping;
        private string currentSentence;
        private float currentTypingSpeed;
        
        private static DialogueSystem instance;
        public static DialogueSystem Instance => instance;
        
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            
            lines = new Queue<DialogueLine>();
            currentTypingSpeed = defaultTypingSpeed;
        }
        
        void Start()
        {
            dialoguePanel.SetActive(false);
            continueButton.onClick.AddListener(DisplayNextLine);
            
            // Subscribe to settings changes
            if (SettingsManager.Instance != null)
            {
                SettingsManager.Instance.OnTextSpeedChanged += OnTextSpeedChanged;
                currentTypingSpeed = SettingsManager.Instance.CurrentSettings.textSpeed;
            }
        }
        
        void OnDestroy()
        {
            // Unsubscribe from events
            if (SettingsManager.Instance != null)
            {
                SettingsManager.Instance.OnTextSpeedChanged -= OnTextSpeedChanged;
            }
        }
        
        void OnTextSpeedChanged(float newSpeed)
        {
            currentTypingSpeed = newSpeed;
        }
        
        public void StartDialogue(DialogueData dialogue)
        {
            dialoguePanel.SetActive(true);
            lines.Clear();
            
            foreach (DialogueLine line in dialogue.lines)
            {
                lines.Enqueue(line);
            }
            
            DisplayNextLine();
        }
        
        public void DisplayNextLine()
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                isTyping = false;
                return;
            }
            
            if (lines.Count == 0)
            {
                EndDialogue();
                return;
            }
            
            DialogueLine line = lines.Dequeue();
            nameText.text = line.speakerName;
            
            StopAllCoroutines();
            StartCoroutine(TypeSentence(line.text));
        }
        
        IEnumerator TypeSentence(string sentence)
        {
            isTyping = true;
            currentSentence = sentence;
            dialogueText.text = "";
            
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(currentTypingSpeed);
            }
            
            isTyping = false;
        }
        
        void EndDialogue()
        {
            dialoguePanel.SetActive(false);
        }
    }
    
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        [TextArea(3, 5)]
        public string text;
    }
    
    [System.Serializable]
    public class DialogueData
    {
        public List<DialogueLine> lines;
    }
}