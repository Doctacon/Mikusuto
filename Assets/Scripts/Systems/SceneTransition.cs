using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mikusuto.Systems
{
    public class SceneTransition : MonoBehaviour
    {
        [Header("Transition Settings")]
        [SerializeField] private string targetSceneName;
        [SerializeField] private Vector2 targetPosition;
        [SerializeField] private bool useTargetPosition = true;
        
        [Header("Fade Settings")]
        [SerializeField] private float fadeOutDuration = 0.5f;
        [SerializeField] private float fadeInDuration = 0.5f;
        
        private static SceneTransition instance;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(TransitionToScene());
            }
        }
        
        IEnumerator TransitionToScene()
        {
            yield return StartCoroutine(FadeOut());
            
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            if (useTargetPosition)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = targetPosition;
                }
            }
            
            yield return StartCoroutine(FadeIn());
        }
        
        IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(fadeOutDuration);
        }
        
        IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(fadeInDuration);
        }
    }
}