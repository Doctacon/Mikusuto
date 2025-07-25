# CLAUDE.md - Mikusuto Project Guide

## Project Overview
**Game**: Mikusuto - An Edo period narrative adventure inspired by Pentiment
**Genre**: 2D narrative adventure with historical focus
**Setting**: Japan during the Edo period
**Core Mechanics**: Exploration, dialogue, character interaction

## Technical Stack
- **Engine**: Unity 2021.3+ (using Unity Personal)
- **Language**: C# 
- **IDE**: Cursor for code editing
- **Version Control**: Git

## Project Structure
```
Assets/
├── Scripts/
│   ├── Player/           # Movement, camera, controls
│   ├── Dialogue/         # Conversation system, NPCs
│   ├── Systems/          # Save, audio, scene transitions
│   ├── Managers/         # Game state, scene management
│   └── UI/              # Menus, HUD, dialogue UI
├── Prefabs/             # Reusable game objects
├── Sprites/             # 2D art assets
├── Audio/               # Music, SFX, ambient sounds
└── Scenes/              # Unity scene files
```

## Completed Features
- ✅ Basic player movement system (PlayerController.cs)
- ✅ Camera follow system (CameraFollow.cs)
- ✅ Dialogue system with typewriter effect (DialogueSystem.cs)
- ✅ NPC interaction system (NPC.cs, IInteractable.cs)
- ✅ Scene transition system (SceneTransition.cs)
- ✅ Save/Load system (SaveSystem.cs)
- ✅ Audio manager (AudioManager.cs)
- ✅ Game manager for pause/quit (GameManager.cs)

## Next Steps
1. **Inventory System** - For collecting items and gifts
2. **Quest/Task System** - Track story objectives and side quests
3. **Time of Day System** - Visual changes for morning/afternoon/evening
4. **Art Style Guide** - Define ukiyo-e inspired visual style
5. **First Scene Layout** - Create the initial town area

## Art Direction Notes
- Inspired by ukiyo-e woodblock prints
- Muted color palette with selective bright accents
- Flat perspective similar to traditional Japanese art
- Character designs based on Edo period clothing

## Dialogue & Writing Style
- Historical accuracy in speech patterns
- Honorifics system (san, sama, dono)
- Period-appropriate topics and concerns
- Multiple dialogue choices affect relationships

## Unity Workflow Reminders
1. **Before Running**: Check Console for compilation errors
2. **Testing**: Use Play Mode to test, don't edit in Play Mode
3. **Prefabs**: Create prefabs for reusable objects (NPCs, items)
4. **Scenes**: Save scenes frequently (Ctrl+S)
5. **Version Control**: Commit working builds before major changes

## Common Commands
```bash
# Git workflow
git add .
git commit -m "Description of changes"
git push

# Unity typically opened via Unity Hub, not command line
```

## Performance Considerations
- Use object pooling for frequently spawned objects
- Optimize sprite sizes (power of 2 when possible)
- Limit active NPCs in scene
- Use LOD for background elements

## Testing Checklist
- [ ] Player movement feels responsive
- [ ] Dialogue displays correctly
- [ ] Scene transitions work smoothly
- [ ] Save/Load maintains game state
- [ ] Audio plays at appropriate volumes
- [ ] No console errors during play

## Resources
- Unity Docs: https://docs.unity3d.com
- C# for Unity: https://learn.unity.com/tutorial/variables-and-functions
- Edo Period Reference: Research historical accuracy
- Japanese Architecture: Reference for building designs