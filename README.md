# Mikusuto - An Edo Period Narrative Adventure

A 2D narrative adventure game inspired by Pentiment, set in Japan during the Edo period. Walk through historical Japanese towns, interact with townsfolk, and uncover mysteries while experiencing authentic period culture.

## Project Structure

```
Assets/
   Scripts/
      Player/
         PlayerController.cs - 2D character movement and interaction
         CameraFollow.cs - Smooth camera following system
      Dialogue/
         DialogueSystem.cs - Typewriter-style dialogue system
         NPC.cs - Interactable NPCs with dialogue
      Managers/
         GameManager.cs - Core game state management
      IInteractable.cs - Interface for interactive objects
   Prefabs/
   Sprites/
   Audio/
   Scenes/
```

## Core Features

- **2D Character Movement**: WASD/Arrow keys movement with Rigidbody2D physics
- **Dialogue System**: Typewriter effect dialogue with character names
- **NPC Interactions**: Press E to interact with NPCs and objects
- **Camera System**: Smooth following camera with optional boundary constraints

## Getting Started

1. Open project in Unity 2021.3 or later
2. Create a new 2D scene
3. Add PlayerController to your player GameObject
4. Set up UI Canvas with dialogue panel elements
5. Add DialogueSystem to a GameObject in the scene
6. Create NPCs with the NPC component and dialogue data

## Controls

- **Movement**: WASD or Arrow Keys
- **Interact**: E
- **Pause**: Escape