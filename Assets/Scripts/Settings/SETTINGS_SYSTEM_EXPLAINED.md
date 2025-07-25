# Settings System - Complete Implementation Guide

## What is the Settings System?

A **Settings System** manages all player preferences and game configuration. Think of it as your game's "control panel" where players can:

- Adjust audio levels (master, music, sound effects)
- Change graphics quality and resolution
- Customize gameplay preferences (dialogue speed, subtitles)
- Configure controls and accessibility options

## Components We Created

### 1. GameSettings.cs (The Data Structure)
This is like a **configuration file** that stores all settings:

```csharp
[System.Serializable]
public class GameSettings
{
    public float masterVolume = 1f;      // Audio settings
    public int qualityLevel = 2;         // Graphics settings  
    public float textSpeed = 0.05f;      // Gameplay settings
    public string language = "English";  // Localization
}
```

**Why Serializable?** 
- Unity can save/load it as JSON
- Shows up nicely in Unity Inspector
- Easy to backup and restore

### 2. SettingsManager.cs (The Controller)
This script **manages** all the settings:

```csharp
// Load settings from disk
SettingsManager.Instance.LoadSettings();

// Change a setting
SettingsManager.Instance.SetMasterVolume(0.8f);

// Apply all settings to the game
SettingsManager.Instance.ApplyAllSettings();

// Save settings to disk  
SettingsManager.Instance.SaveSettings();
```

**Key Features:**
- **Singleton Pattern**: Only one SettingsManager exists
- **Event System**: Other scripts get notified when settings change
- **Backup/Restore**: Players can cancel changes
- **Auto-Apply**: Settings take effect immediately

### 3. SettingsUI.cs (The Interface)
This creates the **settings menu** that players interact with:

```csharp
// UI Elements
[SerializeField] private Slider masterVolumeSlider;
[SerializeField] private Toggle fullscreenToggle;
[SerializeField] private TMP_Dropdown qualityDropdown;

// When player moves slider:
void OnMasterVolumeChanged(float value)
{
    settingsManager.SetMasterVolume(value);  // Update setting
    UpdateVolumeText(value);                 // Update UI text
}
```

## How Everything Connects

```
[Player moves slider] → [SettingsUI detects change] → [SettingsManager updates setting] 
                                                                ↓
[AudioManager gets notified] ← [Event system triggers] ← [Setting applied to game]
```

### Example: Player Changes Master Volume

1. **Player**: Moves volume slider to 50%
2. **SettingsUI**: Detects slider change, calls `OnMasterVolumeChanged(0.5f)`
3. **SettingsManager**: Updates `currentSettings.masterVolume = 0.5f`
4. **SettingsManager**: Calls `ApplyAudioSettings()`
5. **AudioManager**: Receives event, adjusts actual game volume
6. **Player**: Hears volume change immediately!

## Settings Categories Explained

### Audio Settings
```csharp
// Volume levels (0.0 to 1.0)
public float masterVolume = 1f;    // Overall game volume
public float musicVolume = 0.7f;   // Background music
public float sfxVolume = 1f;       // Sound effects
public float voiceVolume = 1f;     // Character voices (future)
```

**How Volume Works:**
- Unity uses **decibel scale** (-80dB to 0dB)
- We convert **linear scale** (0-1) to decibels
- 0 = Silent, 1 = Full volume

### Graphics Settings
```csharp
public int qualityLevel = 2;       // Unity's built-in quality levels
public bool fullscreen = true;     // Windowed vs fullscreen
public int resolutionIndex = 0;    // Which resolution from available list
public bool vsync = true;          // Prevent screen tearing
public int targetFrameRate = 60;   // FPS cap
```

**Quality Levels:**
- 0 = Low (potato PCs)
- 1 = Medium (average PCs)  
- 2 = High (good PCs)
- 3+ = Ultra (high-end PCs)

### Gameplay Settings
```csharp
public float textSpeed = 0.05f;        // Dialogue typing speed
public bool skipReadDialogue = false;  // Skip previously read text
public bool showSubtitles = true;      // Show dialogue text
public string language = "English";    // Localization language
```

**Text Speed:**
- 0.01f = Very Fast
- 0.05f = Normal
- 0.1f = Slow
- 0.2f = Very Slow

## File Storage System

### Where Settings Are Saved
```csharp
// Path example: 
// Windows: C:\Users\Username\AppData\LocalLow\YourCompany\Mikusuto\settings.json
// Mac: ~/Library/Application Support/YourCompany/Mikusuto/settings.json
string settingsPath = Path.Combine(Application.persistentDataPath, "settings.json");
```

### Settings File Format (JSON)
```json
{
    "masterVolume": 0.8,
    "musicVolume": 0.6,
    "sfxVolume": 1.0,
    "qualityLevel": 2,
    "fullscreen": true,
    "textSpeed": 0.05,
    "language": "English"
}
```

**Benefits of JSON:**
- Human-readable
- Easy to debug
- Cross-platform compatible
- Can be edited by advanced users

## Event System Explained

The settings system uses **C# Events** to notify other systems when settings change:

```csharp
// In SettingsManager
public System.Action<float> OnMasterVolumeChanged;

// When volume changes
OnMasterVolumeChanged?.Invoke(newVolume);

// In DialogueSystem  
void Start()
{
    SettingsManager.Instance.OnTextSpeedChanged += OnTextSpeedChanged;
}

void OnTextSpeedChanged(float newSpeed)
{
    currentTypingSpeed = newSpeed;  // Update dialogue speed
}
```

**Why Events?**
- **Decoupled**: Systems don't need to know about each other
- **Efficient**: Only interested systems get notified
- **Flexible**: Easy to add new systems that react to settings

## UI Integration

### Setting up the Settings Menu

1. **Create UI Elements** in Unity:
   ```csharp
   // Audio Section
   Slider masterVolumeSlider;
   Text masterVolumeText;
   
   // Graphics Section  
   Dropdown qualityDropdown;
   Toggle fullscreenToggle;
   
   // Buttons
   Button applyButton;
   Button cancelButton;
   Button defaultsButton;
   ```

2. **Link to SettingsUI Script**:
   - Drag UI elements to script's serialized fields
   - Script automatically sets up event listeners

3. **Button Functionality**:
   ```csharp
   Apply Button    → Save settings to disk
   Cancel Button   → Restore from backup
   Defaults Button → Reset to factory settings
   Close Button    → Hide settings menu
   ```

### UI Feedback Systems

**Real-time Updates:**
```csharp
// Volume slider shows percentage
masterVolumeText.text = Mathf.RoundToInt(value * 100) + "%";

// Text speed shows descriptive label
string speedLabel = value <= 0.02f ? "Fast" : 
                   value <= 0.05f ? "Normal" : "Slow";
```

**Visual Feedback:**
- Sliders show current values as percentages
- Dropdowns show current selection
- Toggles reflect current on/off state

## Integration with Game Systems

### How DialogueSystem Uses Settings
```csharp
// Before: Fixed typing speed
yield return new WaitForSeconds(0.05f);  // ❌ Not customizable

// After: Uses settings  
yield return new WaitForSeconds(currentTypingSpeed);  // ✅ Player controlled
```

### How AudioManager Uses Settings
```csharp
// Before: Hard-coded volumes
musicSource.volume = 0.7f;  // ❌ No player control

// After: Uses settings
musicSource.volume = musicVolume * masterVolume;  // ✅ Respects player preference
```

## Advanced Features

### Backup and Restore System
```csharp
// When opening settings menu
settingsManager.CreateBackup();

// If player clicks "Cancel"
settingsManager.RestoreFromBackup();

// If player clicks "Apply"  
settingsManager.SaveSettings();
```

**Why This Matters:**
- Players can experiment with settings
- "Cancel" undoes all changes since opening menu
- "Apply" makes changes permanent

### Auto-Detection Features
```csharp
// Automatically detect best quality level
public void DetectOptimalSettings()
{
    if (SystemInfo.systemMemorySize < 4096)
        qualityLevel = 0;  // Low quality for weak PCs
    else if (SystemInfo.systemMemorySize < 8192) 
        qualityLevel = 1;  // Medium quality
    else
        qualityLevel = 2;  // High quality
}
```

### Validation and Safety
```csharp
// Ensure volumes stay in valid range
public void SetMasterVolume(float volume)
{
    currentSettings.masterVolume = Mathf.Clamp01(volume);  // Force 0-1 range
}

// Ensure quality level exists
public void SetQualityLevel(int level)  
{
    currentSettings.qualityLevel = Mathf.Clamp(level, 0, QualitySettings.names.Length - 1);
}
```

## Troubleshooting Common Issues

### "Settings Not Saving"
1. Check if `Application.persistentDataPath` is writable
2. Verify JSON serialization isn't failing
3. Look for file permission errors in console

### "Settings Not Loading on Startup"
1. Ensure SettingsManager is in the first scene
2. Check if settings file exists at expected path
3. Verify SettingsManager.Start() calls LoadSettings()

### "UI Not Updating When Settings Change"
1. Ensure UI elements are linked in inspector
2. Check if event listeners are properly set up
3. Verify LoadCurrentSettings() is called after changes

## Future Enhancements

### Keybinding System
```csharp
public class InputBinding
{
    public string actionName;
    public KeyCode keyCode;
    public string gamepadButton;
}
```

### Graphics Presets
```csharp
public enum GraphicsPreset
{
    Performance,  // Low quality, high FPS
    Balanced,     // Medium quality, stable FPS  
    Quality,      // High quality, lower FPS
    Custom        // Player-defined settings
}
```

### Accessibility Options
```csharp
public bool colorBlindSupport = false;
public float textSize = 1f;
public bool highContrast = false;
public bool reducedMotion = false;
```

## Why This Matters for Your Edo Period Game

### Professional Polish
- Modern games have comprehensive settings
- Shows respect for player preferences
- Increases accessibility for diverse players

### Historical Accuracy Features
```csharp
public bool showHistoricalNotes = true;     // Educational tooltips
public bool authenticLanguageMode = false;  // Period-appropriate speech
public float ambientSoundLevel = 0.8f;      // Edo period soundscape
```

### Performance Optimization
- Players with older PCs can reduce quality
- Stable frame rates improve experience
- Customizable audio reduces system load

Your settings system is now **production-ready** and provides the foundation for a professional, accessible gaming experience! 🎮⚙️