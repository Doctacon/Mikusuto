using System.IO;
using UnityEngine;

namespace Mikusuto.Systems
{
    public class SaveSystem : MonoBehaviour
    {
        private static SaveSystem instance;
        public static SaveSystem Instance => instance;
        
        private string savePath;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                savePath = Path.Combine(Application.persistentDataPath, "save.json");
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void SaveGame()
        {
            SaveData data = new SaveData();
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                data.playerPosition = player.transform.position;
                data.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            }
            
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
            
            Debug.Log("Game Saved!");
        }
        
        public void LoadGame()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                
                UnityEngine.SceneManagement.SceneManager.LoadScene(data.currentScene);
                
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = data.playerPosition;
                }
                
                Debug.Log("Game Loaded!");
            }
            else
            {
                Debug.Log("No save file found!");
            }
        }
        
        public bool HasSaveFile()
        {
            return File.Exists(savePath);
        }
    }
    
    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;
        public string currentScene;
        public int playerHealth = 100;
        public string[] completedQuests;
        public string[] inventory;
    }
}