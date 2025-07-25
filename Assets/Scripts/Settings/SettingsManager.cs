using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using Mikusuto.Systems;

namespace Mikusuto.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        private static SettingsManager instance;
        public static SettingsManager Instance => instance;
        
        [Header("Audio")]
        [SerializeField] private AudioMixer mainAudioMixer;
        
        [Header("Settings")]
        [SerializeField] private GameSettings currentSettings;
        
        private string settingsPath;
        private GameSettings backupSettings; // For reverting changes
        
        // Events for when settings change
        public System.Action<GameSettings> OnSettingsChanged;
        public System.Action<float> OnMasterVolumeChanged;
        public System.Action<float> OnMusicVolumeChanged;
        public System.Action<float> OnSFXVolumeChanged;
        public System.Action<float> OnTextSpeedChanged;
        
        public GameSettings CurrentSettings => currentSettings;
        
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeSettings();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void InitializeSettings()
        {
            settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
            
            // Load existing settings or create defaults
            if (File.Exists(settingsPath))
            {
                LoadSettings();
            }
            else
            {
                currentSettings = new GameSettings();
                SaveSettings();
            }
            
            ApplyAllSettings();
        }
        
        public void SaveSettings()
        {
            try
            {
                string json = JsonUtility.ToJson(currentSettings, true);
                File.WriteAllText(settingsPath, json);
                Debug.Log("Settings saved successfully");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to save settings: {e.Message}");
            }
        }
        
        public void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsPath))
                {
                    string json = File.ReadAllText(settingsPath);
                    currentSettings = JsonUtility.FromJson<GameSettings>(json);
                    Debug.Log("Settings loaded successfully");
                }
                else
                {
                    currentSettings = new GameSettings();
                    Debug.Log("No settings file found, using defaults");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load settings: {e.Message}");
                currentSettings = new GameSettings();
            }
        }
        
        public void ApplyAllSettings()
        {
            ApplyAudioSettings();
            ApplyGraphicsSettings();
            ApplyGameplaySettings();
            
            OnSettingsChanged?.Invoke(currentSettings);
        }
        
        public void ApplyAudioSettings()
        {
            if (mainAudioMixer != null)
            {
                // Convert 0-1 range to decibel range (-80 to 0)
                float masterDB = currentSettings.masterVolume > 0 ? 
                    Mathf.Log10(currentSettings.masterVolume) * 20 : -80f;
                float musicDB = currentSettings.musicVolume > 0 ? 
                    Mathf.Log10(currentSettings.musicVolume) * 20 : -80f;
                float sfxDB = currentSettings.sfxVolume > 0 ? 
                    Mathf.Log10(currentSettings.sfxVolume) * 20 : -80f;
                
                mainAudioMixer.SetFloat("MasterVolume", masterDB);
                mainAudioMixer.SetFloat("MusicVolume", musicDB);
                mainAudioMixer.SetFloat("SFXVolume", sfxDB);
            }
            
            // Update AudioManager if it exists
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMasterVolume(currentSettings.masterVolume);
                AudioManager.Instance.SetMusicVolume(currentSettings.musicVolume);
                AudioManager.Instance.SetSFXVolume(currentSettings.sfxVolume);
            }
            
            OnMasterVolumeChanged?.Invoke(currentSettings.masterVolume);
            OnMusicVolumeChanged?.Invoke(currentSettings.musicVolume);
            OnSFXVolumeChanged?.Invoke(currentSettings.sfxVolume);
        }
        
        public void ApplyGraphicsSettings()
        {
            // Quality Level
            QualitySettings.SetQualityLevel(currentSettings.qualityLevel);
            
            // Resolution
            if (currentSettings.resolutionIndex < Screen.resolutions.Length)
            {
                Resolution targetRes = Screen.resolutions[currentSettings.resolutionIndex];
                Screen.SetResolution(targetRes.width, targetRes.height, currentSettings.fullscreen);
            }
            
            // VSync
            QualitySettings.vSyncCount = currentSettings.vsync ? 1 : 0;
            
            // Target Frame Rate
            Application.targetFrameRate = currentSettings.targetFrameRate;
        }
        
        public void ApplyGameplaySettings()
        {
            OnTextSpeedChanged?.Invoke(currentSettings.textSpeed);
        }
        
        // Individual setting methods
        public void SetMasterVolume(float volume)
        {
            currentSettings.masterVolume = Mathf.Clamp01(volume);
            ApplyAudioSettings();
        }
        
        public void SetMusicVolume(float volume)
        {
            currentSettings.musicVolume = Mathf.Clamp01(volume);
            ApplyAudioSettings();
        }
        
        public void SetSFXVolume(float volume)
        {
            currentSettings.sfxVolume = Mathf.Clamp01(volume);
            ApplyAudioSettings();
        }
        
        public void SetQualityLevel(int level)
        {
            currentSettings.qualityLevel = Mathf.Clamp(level, 0, QualitySettings.names.Length - 1);
            ApplyGraphicsSettings();
        }
        
        public void SetFullscreen(bool fullscreen)
        {
            currentSettings.fullscreen = fullscreen;
            ApplyGraphicsSettings();
        }
        
        public void SetResolution(int resolutionIndex)
        {
            if (resolutionIndex >= 0 && resolutionIndex < Screen.resolutions.Length)
            {
                currentSettings.resolutionIndex = resolutionIndex;
                ApplyGraphicsSettings();
            }
        }
        
        public void SetVSync(bool enabled)
        {
            currentSettings.vsync = enabled;
            ApplyGraphicsSettings();
        }
        
        public void SetTextSpeed(float speed)
        {
            currentSettings.textSpeed = Mathf.Clamp(speed, 0.01f, 0.2f);
            ApplyGameplaySettings();
        }
        
        public void SetLanguage(string language)
        {
            currentSettings.language = language;
            // TODO: Implement localization system
        }
        
        // Settings backup/restore for "Cancel" functionality
        public void CreateBackup()
        {
            backupSettings = new GameSettings(currentSettings);
        }
        
        public void RestoreFromBackup()
        {
            if (backupSettings != null)
            {
                currentSettings = new GameSettings(backupSettings);
                ApplyAllSettings();
            }
        }
        
        public void ResetToDefaults()
        {
            currentSettings.SetDefaults();
            ApplyAllSettings();
        }
        
        // Utility methods
        public string[] GetQualityLevelNames()
        {
            return QualitySettings.names;
        }
        
        public Resolution[] GetAvailableResolutions()
        {
            return Screen.resolutions;
        }
        
        public string GetCurrentResolutionString()
        {
            Resolution current = Screen.currentResolution;
            return $"{current.width} x {current.height}";
        }
    }
}