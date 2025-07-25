using UnityEngine;

namespace Mikusuto.Dialogue
{
    public class NPC : MonoBehaviour, IInteractable
    {
        [Header("NPC Settings")]
        [SerializeField] private string npcName = "Villager";
        [SerializeField] private DialogueData dialogue;
        
        public string NPCName => npcName;
        
        [Header("Visual Feedback")]
        [SerializeField] private GameObject interactionPrompt;
        
        private bool playerInRange;
        
        void Start()
        {
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
                if (interactionPrompt != null)
                    interactionPrompt.SetActive(true);
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                if (interactionPrompt != null)
                    interactionPrompt.SetActive(false);
            }
        }
        
        public void Interact()
        {
            if (playerInRange && dialogue != null)
            {
                DialogueSystem.Instance.StartDialogue(dialogue);
            }
        }
    }
}