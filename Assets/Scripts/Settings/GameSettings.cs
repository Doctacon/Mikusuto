using UnityEngine;
using UnityEngine.Audio;

namespace Mikusuto.Settings
{
    [System.Serializable]
    public class GameSettings
    {
        [Header("Audio Settings")]
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 0.7f;
        [Range(0f, 1f)] public float sfxVolume = 1f;
        [Range(0f, 1f)] public float voiceVolume = 1f;
        
        [Header("Graphics Settings")]
        public int qualityLevel = 2; // 0=Low, 1=Medium, 2=High
        public bool fullscreen = true;
        public int resolutionIndex = 0;
        public bool vsync = true;
        public int targetFrameRate = 60;
        
        [Header("Gameplay Settings")]
        public float textSpeed = 0.05f; // Dialogue typing speed
        public bool skipReadDialogue = false;
        public bool showSubtitles = true;
        public string language = "English";
        
        [Header("Control Settings")]
        public float mouseSensitivity = 1f;
        public bool invertYAxis = false;
        
        // Constructor with default values
        public GameSettings()
        {
            SetDefaults();
        }
        
        public void SetDefaults()
        {
            masterVolume = 1f;
            musicVolume = 0.7f;
            sfxVolume = 1f;
            voiceVolume = 1f;
            
            qualityLevel = QualitySettings.GetQualityLevel();
            fullscreen = Screen.fullScreen;
            resolutionIndex = GetCurrentResolutionIndex();
            vsync = QualitySettings.vSyncCount > 0;
            targetFrameRate = 60;
            
            textSpeed = 0.05f;
            skipReadDialogue = false;
            showSubtitles = true;
            language = "English";
            
            mouseSensitivity = 1f;
            invertYAxis = false;
        }
        
        private int GetCurrentResolutionIndex()
        {
            Resolution currentRes = Screen.currentResolution;
            Resolution[] resolutions = Screen.resolutions;
            
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == currentRes.width && 
                    resolutions[i].height == currentRes.height)
                {
                    return i;
                }
            }
            return 0;
        }
        
        // Copy constructor for creating backups
        public GameSettings(GameSettings other)
        {
            masterVolume = other.masterVolume;
            musicVolume = other.musicVolume;
            sfxVolume = other.sfxVolume;
            voiceVolume = other.voiceVolume;
            
            qualityLevel = other.qualityLevel;
            fullscreen = other.fullscreen;
            resolutionIndex = other.resolutionIndex;
            vsync = other.vsync;
            targetFrameRate = other.targetFrameRate;
            
            textSpeed = other.textSpeed;
            skipReadDialogue = other.skipReadDialogue;
            showSubtitles = other.showSubtitles;
            language = other.language;
            
            mouseSensitivity = other.mouseSensitivity;
            invertYAxis = other.invertYAxis;
        }
    }
}