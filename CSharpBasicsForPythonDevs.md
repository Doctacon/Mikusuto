# C# Basics for Python Developers

## Key Differences from Python

### 1. Static Typing
```csharp
// C# - Must declare types
string name = "Andreas";
int age = 30;
float speed = 5.5f;

// Python equivalent:
// name = "Andreas"
// age = 30
// speed = 5.5
```

### 2. Classes and Methods
```csharp
// C# Class
public class Player
{
    // Properties (like Python attributes)
    public string Name { get; set; }
    private int health;
    
    // Constructor (like Python __init__)
    public Player(string name)
    {
        Name = name;
        health = 100;
    }
    
    // Method
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

// Python equivalent:
// class Player:
//     def __init__(self, name):
//         self.name = name
//         self._health = 100
//     
//     def take_damage(self, damage):
//         self._health -= damage
```

### 3. Access Modifiers
- `public` - Accessible from anywhere (like Python's default)
- `private` - Only accessible within the class (like Python's _variable convention)
- `protected` - Accessible within class and subclasses
- `[SerializeField] private` - Unity-specific: private but visible in Unity editor

### 4. Namespaces (like Python modules)
```csharp
namespace Mikusuto.Player
{
    public class PlayerController
    {
        // Code here
    }
}

// Usage:
using Mikusuto.Player;  // like Python's 'import'
```

### 5. Common Unity-Specific Patterns

```csharp
public class PlayerController : MonoBehaviour  // Inheritance
{
    // These methods are called by Unity automatically:
    void Start()      // Called once when object is created
    {
        // Like Python's __init__ but for game objects
    }
    
    void Update()     // Called every frame
    {
        // Game logic that runs 60 times per second
    }
    
    void FixedUpdate() // Called at fixed intervals for physics
    {
        // Physics calculations
    }
}
```

### 6. Variables and Properties
```csharp
// Field (simple variable)
private int score;

// Auto-property (getter/setter)
public string Name { get; set; }

// Full property with logic
private int health;
public int Health
{
    get { return health; }
    set { health = Mathf.Clamp(value, 0, 100); }
}
```

### 7. Collections
```csharp
// Arrays (fixed size)
int[] numbers = new int[5];
string[] names = {"Alice", "Bob"};

// Lists (like Python lists)
List<string> items = new List<string>();
items.Add("sword");
items.Remove("sword");

// Dictionaries
Dictionary<string, int> scores = new Dictionary<string, int>();
scores["player1"] = 100;
```

### 8. Control Flow (Similar to Python)
```csharp
// If statements
if (health > 0)
{
    // alive
}
else if (health == 0)
{
    // dead
}

// For loops
for (int i = 0; i < 10; i++)
{
    // Do something
}

// Foreach (like Python's for-in)
foreach (string item in items)
{
    Console.WriteLine(item);
}

// While loops
while (isRunning)
{
    // Game loop
}
```

### 9. Null Handling
```csharp
// Check for null (like Python's None)
if (player != null)
{
    player.Move();
}

// Null-conditional operator
player?.Move();  // Only calls Move() if player isn't null

// Null-coalescing operator
string name = inputName ?? "Default";  // Use "Default" if inputName is null
```

### 10. Unity-Specific Attributes
```csharp
[Header("Movement Settings")]      // Creates a header in Unity Inspector
[SerializeField]                  // Shows private field in Inspector
[Range(0, 100)]                  // Creates a slider in Inspector
public float speed = 5f;

[Tooltip("This is helpful text")]
public int damage = 10;
```

## Quick Reference

| Python | C# |
|--------|-----|
| `def function():` | `void Function()` |
| `return value` | `return value;` |
| `self` | `this` |
| `None` | `null` |
| `True/False` | `true/false` |
| `and/or/not` | `&&/||/!` |
| `import module` | `using Namespace;` |
| `len(list)` | `list.Count` |
| `"string"` | `"string"` |
| `f"Hello {name}"` | `$"Hello {name}"` |
| `# comment` | `// comment` |

## Common Unity MonoBehaviour Methods

- `Awake()` - Called once, before Start()
- `Start()` - Called once, when enabled
- `Update()` - Called every frame
- `FixedUpdate()` - Called at fixed intervals (physics)
- `OnCollisionEnter2D()` - When collision starts
- `OnTriggerEnter2D()` - When entering trigger area
- `OnDestroy()` - When object is destroyed