# Phase 1 Testing Guide

## How to Test Your Systems

### 1. Create Test Scene in Unity

**Step-by-Step Setup:**

1. **Create New Scene**: File → New Scene → Save as "Phase1Test"

2. **Add Core Managers** (Create empty GameObjects with these scripts):
   ```
   GameManager (GameManager.cs)
   InputManager (InputManager.cs) 
   SettingsManager (SettingsManager.cs)
   AudioManager (AudioManager.cs)
   SaveSystem (SaveSystem.cs)
   DialogueSystem (DialogueSystem.cs)
   ```

3. **Add Test Manager**:
   - Create empty GameObject
   - Add `Phase1TestManager.cs` script
   - Create UI Canvas with TextMeshPro elements

### 2. Manual Testing Checklist

#### ✅ Input System Tests
- [ ] **WASD Movement**: Press WASD keys, check console/UI for input values
- [ ] **Arrow Keys**: Test alternative movement keys  
- [ ] **E Key Interaction**: Press E, should show "PRESSED" in UI
- [ ] **Escape Pause**: Press ESC, should trigger pause
- [ ] **Gamepad Support**: If you have a controller, test all inputs

#### ✅ Settings System Tests
- [ ] **Settings Save/Load**: 
  - Change volume → Close Unity → Reopen → Volume should be saved
  - Check: `%USERPROFILE%\AppData\LocalLow\DefaultCompany\mikusuto\settings.json`
- [ ] **Volume Controls**: 
  - Change master volume → Audio should adjust immediately
  - Test music and SFX sliders separately
- [ ] **Graphics Settings**: 
  - Change quality level → Visual changes should apply
  - Toggle fullscreen → Window mode should change
- [ ] **Text Speed**: 
  - Change dialogue speed → Test dialogue should type faster/slower

#### ✅ Audio System Tests  
- [ ] **Volume Response**: Audio levels change with settings
- [ ] **SFX Playback**: Test sound effects play correctly
- [ ] **Multiple Audio Sources**: Music and SFX play simultaneously

#### ✅ Dialogue System Tests
- [ ] **Basic Dialogue**: Text appears with typewriter effect
- [ ] **Settings Integration**: Text speed respects player preference
- [ ] **Multiple Lines**: Can progress through dialogue sequence
- [ ] **Interaction**: E key advances dialogue properly

#### ✅ Save System Tests
- [ ] **File Creation**: Save file appears in correct directory
- [ ] **Data Persistence**: Settings survive Unity restart
- [ ] **Error Handling**: Graceful failure if save file corrupted

### 3. Quick Console Commands

Use these in Unity Console (Window → General → Console):

```csharp
// Test input detection
InputManager.Instance.GetMovementInput()

// Test settings save
SettingsManager.Instance.SaveSettings()

// Test audio playback  
AudioManager.Instance.PlayInteractSound()

// Check save file exists
SaveSystem.Instance.HasSaveFile()
```

### 4. Common Issues & Solutions

#### "InputManager not found"
- **Problem**: InputManager script not in scene
- **Solution**: Create GameObject, add InputManager.cs

#### "Settings not saving"
- **Problem**: File permissions or path issues
- **Solution**: Check Unity Console for errors, verify write permissions

#### "Audio not playing"
- **Problem**: No AudioSource components or missing clips
- **Solution**: Ensure AudioManager has AudioSource components attached

#### "Dialogue not displaying"
- **Problem**: Missing UI elements or DialogueSystem setup
- **Solution**: Create UI Canvas with Text/TextMeshPro components

### 5. Performance Tests

#### Frame Rate Test
```csharp
// In Update() method, check performance
void Update()
{
    if (Time.frameCount % 60 == 0) // Every 60 frames
    {
        Debug.Log($"FPS: {1.0f / Time.deltaTime:F1}");
    }
}
```

#### Memory Usage Test
- **Window → Analysis → Profiler**
- Run game for 5+ minutes
- Check for memory leaks in systems

### 6. Integration Tests

#### Settings → Audio Chain
1. Open settings menu
2. Change master volume to 50%
3. Play test sound
4. Verify volume is actually 50%

#### Settings → Dialogue Chain  
1. Change text speed to "Fast"
2. Start test dialogue
3. Verify typing speed is faster

### 7. Build Test (Optional)
```csharp
// Test in actual build, not just editor
// File → Build Settings → Build
// Run executable and test all systems
```

### 8. Expected Test Results

**All Green ✅:**
```
🎮 INPUT SYSTEM: ✅ InputManager active
⚙️ SETTINGS SYSTEM: ✅ SettingsManager active  
🔊 AUDIO SYSTEM: ✅ AudioManager active
💬 DIALOGUE SYSTEM: ✅ DialogueSystem active
💾 SAVE SYSTEM: ✅ SaveSystem active
```

**Ready for Phase 2!** 🚀

### 9. Troubleshooting Tips

- **Check Unity Console** for error messages
- **Verify Script References** in Inspector
- **Test One System at a Time** to isolate issues
- **Use Debug.Log()** liberally to trace execution
- **Check File Paths** for save system issues

### 10. What Success Looks Like

When everything works:
- Input responds to keyboard and gamepad
- Settings menu opens and controls work
- Audio plays and respects volume settings  
- Dialogue displays with correct typing speed
- Settings persist between Unity sessions
- No errors in console
- Smooth 60fps performance

**Once all tests pass, you're ready for Phase 2: Core Gameplay Systems!** 🎯