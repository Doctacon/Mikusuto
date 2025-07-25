// Here's a direct comparison of Python vs C# for your game

/* PYTHON VERSION:
class NPC:
    def __init__(self, name, dialogue):
        self.name = name
        self.dialogue = dialogue
        self.is_talking = False
    
    def talk(self):
        if not self.is_talking:
            self.is_talking = True
            for line in self.dialogue:
                print(f"{self.name}: {line}")
            self.is_talking = False
*/

// C# VERSION:
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Properties with types declared
    public string npcName;
    public List<string> dialogue;
    private bool isTalking = false;
    
    // No __init__ needed - Unity handles object creation
    // Use Start() for initialization
    void Start()
    {
        // Any setup code here
    }
    
    public void Talk()
    {
        if (!isTalking)  // ! means "not" (instead of Python's 'not')
        {
            isTalking = true;
            
            // foreach instead of for-in
            foreach (string line in dialogue)
            {
                // $ for string interpolation (like Python's f-strings)
                Debug.Log($"{npcName}: {line}");
            }
            
            isTalking = false;
        }
    }
}

/* KEY DIFFERENCES:
1. Must declare types (string, bool, List<string>)
2. Use curly braces {} instead of indentation
3. Semicolons ; end statements
4. public/private instead of Python's conventions
5. void means "returns nothing" (like Python functions that don't return)
6. MonoBehaviour is Unity's base class for game objects
*/