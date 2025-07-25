using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mikusuto.Settings;

namespace Mikusuto.UI
{
    public class SettingsUI : MonoBehaviour
    {
        [Header("Audio UI")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private TextMeshProUGUI masterVolumeText;
        [SerializeField] private TextMeshProUGUI musicVolumeText;
        [SerializeField] private TextMeshProUGUI sfxVolumeText;
        
        [Header("Graphics UI")]
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Toggle vsyncToggle;
        
        [Header("Gameplay UI")]
        [SerializeField] private Slider textSpeedSlider;
        [SerializeField] private TextMeshProUGUI textSpeedText;
        [SerializeField] private Toggle skipDialogueToggle;
        [SerializeField] private Toggle subtitlesToggle;
        [SerializeField] private TMP_Dropdown languageDropdown;
        
        [Header("Buttons")]
        [SerializeField] private Button applyButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button defaultsButton;
        [SerializeField] private Button closeButton;
        
        [Header("Panels")]
        [SerializeField] private GameObject settingsPanel;
        
        private SettingsManager settingsManager;
        private bool isInitializing = false;
        
        void Start()
        {
            settingsManager = SettingsManager.Instance;
            
            if (settingsManager == null)
            {
                Debug.LogError("SettingsManager not found! Make sure SettingsManager is in the scene.");
                return;
            }
            
            InitializeUI();
            SetupEventListeners();
            LoadCurrentSettings();
        }
        
        void InitializeUI()
        {
            isInitializing = true;
            
            // Initialize Quality Dropdown
            if (qualityDropdown != null)
            {
                qualityDropdown.ClearOptions();
                qualityDropdown.AddOptions(new System.Collections.Generic.List<string>(settingsManager.GetQualityLevelNames()));
            }
            
            // Initialize Resolution Dropdown
            if (resolutionDropdown != null)
            {
                resolutionDropdown.ClearOptions();
                Resolution[] resolutions = settingsManager.GetAvailableResolutions();
                System.Collections.Generic.List<string> resolutionStrings = new System.Collections.Generic.List<string>();
                
                foreach (Resolution res in resolutions)
                {
                    resolutionStrings.Add($"{res.width} x {res.height}");
                }
                
                resolutionDropdown.AddOptions(resolutionStrings);
            }
            
            // Initialize Language Dropdown (placeholder)
            if (languageDropdown != null)
            {
                languageDropdown.ClearOptions();
                languageDropdown.AddOptions(new System.Collections.Generic.List<string> { "English", "Japanese" });
            }
            
            isInitializing = false;
        }
        
        void SetupEventListeners()
        {
            // Audio Sliders
            if (masterVolumeSlider != null)
                masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            if (musicVolumeSlider != null)
                musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            if (sfxVolumeSlider != null)
                sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            
            // Graphics Controls
            if (qualityDropdown != null)
                qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
            if (resolutionDropdown != null)
                resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
            if (fullscreenToggle != null)
                fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
            if (vsyncToggle != null)
                vsyncToggle.onValueChanged.AddListener(OnVSyncChanged);
            
            // Gameplay Controls
            if (textSpeedSlider != null)
                textSpeedSlider.onValueChanged.AddListener(OnTextSpeedChanged);
            if (skipDialogueToggle != null)
                skipDialogueToggle.onValueChanged.AddListener(OnSkipDialogueChanged);
            if (subtitlesToggle != null)
                subtitlesToggle.onValueChanged.AddListener(OnSubtitlesChanged);
            if (languageDropdown != null)
                languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
            
            // Buttons
            if (applyButton != null)
                applyButton.onClick.AddListener(OnApplyClicked);
            if (cancelButton != null)
                cancelButton.onClick.AddListener(OnCancelClicked);
            if (defaultsButton != null)
                defaultsButton.onClick.AddListener(OnDefaultsClicked);
            if (closeButton != null)
                closeButton.onClick.AddListener(OnCloseClicked);
        }
        
        void LoadCurrentSettings()
        {
            if (settingsManager == null) return;
            
            isInitializing = true;
            GameSettings settings = settingsManager.CurrentSettings;
            
            // Audio Settings
            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.value = settings.masterVolume;
                UpdateVolumeText(masterVolumeText, settings.masterVolume);
            }
            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = settings.musicVolume;
                UpdateVolumeText(musicVolumeText, settings.musicVolume);
            }
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = settings.sfxVolume;
                UpdateVolumeText(sfxVolumeText, settings.sfxVolume);
            }
            
            // Graphics Settings
            if (qualityDropdown != null)
                qualityDropdown.value = settings.qualityLevel;
            if (resolutionDropdown != null)
                resolutionDropdown.value = settings.resolutionIndex;
            if (fullscreenToggle != null)
                fullscreenToggle.isOn = settings.fullscreen;
            if (vsyncToggle != null)
                vsyncToggle.isOn = settings.vsync;
            
            // Gameplay Settings
            if (textSpeedSlider != null)
            {
                textSpeedSlider.value = settings.textSpeed;
                UpdateTextSpeedText(settings.textSpeed);
            }
            if (skipDialogueToggle != null)
                skipDialogueToggle.isOn = settings.skipReadDialogue;
            if (subtitlesToggle != null)
                subtitlesToggle.isOn = settings.showSubtitles;
            
            isInitializing = false;
        }
        
        // Audio Event Handlers
        void OnMasterVolumeChanged(float value)
        {
            if (isInitializing) return;
            settingsManager.SetMasterVolume(value);
            UpdateVolumeText(masterVolumeText, value);
        }
        
        void OnMusicVolumeChanged(float value)
        {
            if (isInitializing) return;
            settingsManager.SetMusicVolume(value);
            UpdateVolumeText(musicVolumeText, value);
        }
        
        void OnSFXVolumeChanged(float value)
        {
            if (isInitializing) return;
            settingsManager.SetSFXVolume(value);
            UpdateVolumeText(sfxVolumeText, value);
        }
        
        // Graphics Event Handlers
        void OnQualityChanged(int value)
        {
            if (isInitializing) return;
            settingsManager.SetQualityLevel(value);
        }
        
        void OnResolutionChanged(int value)
        {
            if (isInitializing) return;
            settingsManager.SetResolution(value);
        }
        
        void OnFullscreenChanged(bool value)
        {
            if (isInitializing) return;
            settingsManager.SetFullscreen(value);
        }
        
        void OnVSyncChanged(bool value)
        {
            if (isInitializing) return;
            settingsManager.SetVSync(value);
        }
        
        // Gameplay Event Handlers
        void OnTextSpeedChanged(float value)
        {
            if (isInitializing) return;
            settingsManager.SetTextSpeed(value);
            UpdateTextSpeedText(value);
        }
        
        void OnSkipDialogueChanged(bool value)
        {
            if (isInitializing) return;
            settingsManager.CurrentSettings.skipReadDialogue = value;
        }
        
        void OnSubtitlesChanged(bool value)
        {
            if (isInitializing) return;
            settingsManager.CurrentSettings.showSubtitles = value;
        }
        
        void OnLanguageChanged(int value)
        {
            if (isInitializing) return;
            string[] languages = { "English", "Japanese" };
            if (value < languages.Length)
            {
                settingsManager.SetLanguage(languages[value]);
            }
        }
        
        // Button Event Handlers
        void OnApplyClicked()
        {
            settingsManager.SaveSettings();
            Debug.Log("Settings applied and saved");
        }
        
        void OnCancelClicked()
        {
            settingsManager.RestoreFromBackup();
            LoadCurrentSettings();
            Debug.Log("Settings restored from backup");
        }
        
        void OnDefaultsClicked()
        {
            settingsManager.ResetToDefaults();
            LoadCurrentSettings();
            Debug.Log("Settings reset to defaults");
        }
        
        void OnCloseClicked()
        {
            CloseSettings();
        }
        
        // UI Helper Methods
        void UpdateVolumeText(TextMeshProUGUI text, float value)
        {
            if (text != null)
            {
                text.text = Mathf.RoundToInt(value * 100) + "%";
            }
        }
        
        void UpdateTextSpeedText(float value)
        {
            if (textSpeedText != null)
            {
                string speedLabel = value <= 0.02f ? "Fast" : 
                                  value <= 0.05f ? "Normal" : 
                                  value <= 0.1f ? "Slow" : "Very Slow";
                textSpeedText.text = speedLabel;
            }
        }
        
        // Public Methods
        public void OpenSettings()
        {
            if (settingsPanel != null)
            {
                settingsManager.CreateBackup(); // Create backup for cancel functionality
                settingsPanel.SetActive(true);
                LoadCurrentSettings();
            }
        }
        
        public void CloseSettings()
        {
            if (settingsPanel != null)
            {
                settingsPanel.SetActive(false);
            }
        }
        
        public void ToggleSettings()
        {
            if (settingsPanel != null)
            {
                if (settingsPanel.activeSelf)
                {
                    CloseSettings();
                }
                else
                {
                    OpenSettings();
                }
            }
        }
    }
}