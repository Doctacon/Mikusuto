using UnityEngine;
using UnityEngine.SceneManagement;
using Mikusuto.Input;

namespace Mikusuto.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        
        [Header("Game State")]
        [SerializeField] private bool isPaused;
        
        private InputManager inputManager;
        
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        void Start()
        {
            inputManager = InputManager.Instance;
        }
        
        void Update()
        {
            if (inputManager != null && inputManager.GetPausePressed())
            {
                TogglePause();
            }
        }
        
        public void TogglePause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
        }
        
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        
        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}