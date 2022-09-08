// STYLE SHEET EXAMPLE

// GENERAL:
// - This is an example Style Guide for use with your Unity project.
// - Omit or add to these rules to fit your team's preferences.
// - Use an example style guide to start:
// - Microsoft's Framework Design Guidelines are here: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/
// - Google also maintains a style guide here: https://google.github.io/styleguide/csharp-style.html

// - Customize these rules to build your own team style guide, then apply consistently. 
// - When in doubt, your team's style guide prevails.

// NAMING/CASING:
// - Use Pascal case (e.g. ExamplePlayerController, MaxHealth, etc.) unless noted otherwise
// - Use camel case (e.g. examplePlayerController, maxHealth, etc.) for local/private variables, parameters.
// - Avoid snake case, kebab case, Hungarian notation
// - If you have a Monobehaviour in a file, the source file name must match. 

// FORMATTING:
// - Choose K&R (opening curly braces on same line) or Allman (opening curly braces on a new line) style braces.
// - Keep lines short. Consider horizontal whitespace. Define a standard line width in your style guide (80-120 characters). 
// - Use a single space before flow control conditions, e.g. while (x == y)
// - Avoid spaces inside brackets, e.g. x = dataArray[index]
// - Use a single space after a comma between function arguments.
// - Don’t add a space after the parenthesis and function arguments, e.g. CollectItem(myObject, 0, 1);
// - Don’t use spaces between a function name and parenthesis, e.g. DropPowerUp(myPrefab, 0, 1);
// - Use vertical spacing (extra blank line) for visual separation. 

// COMMENTS:
// - Rather than simply answering "what" or "how," comments can fill in the gaps and tell us "why."
// - Use the // comment to keep the explanation next to the logic.
// - Use a Tooltip instead of a comment for serialized fields. 
// - Avoid Regions. They encourage large class sizes. Collapsed code is more difficult to read. 
// - Use a link to an external reference for legal information or licensing to save space.
// - Use a summary XML tag in front of public methods or functions for output documentation/Intellisense.


// USING LINES:
// - Keep using lines at the top of your file.
// - Remove unsed lines.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// NAMESPACES:
// - Pascal case, without special symbols or underscores.
// - Add using line at the top to avoid typing namespace repeatedly.
// - Create sub-namespaces with the dot (.) operator, e.g. MyApplication.GameFlow, MyApplication.AI, etc.
namespace StyleSheetExample
{

    // ENUMS:
    // - Use a singular type name.
    // - No prefix or suffix.
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    // FLAGS ENUMS:
    // - Use a plural type name 
    // - No prefix or suffix.
    // - Use column-alignment for binary values
    [Flags]
    public enum AttackModes
    {
        // Decimal                         // Binary
        None = 0,                          // 000000
        Melee = 1,                         // 000001
        Ranged = 2,                        // 000010
        Special = 4,                       // 000100

        MeleeAndSpecial = Melee | Special  // 000101
    }

    // INTERFACES:
    // - Name interfaces with adjective phrases.
    // - Use the 'I' prefix.
    public interface IDamageable
    {
        string damageTypeName { get; }
        float damageValue { get; }

        // METHODS:
        // - Start a methods name with a verbs or verb phrases to show an action.
        // - Parameter names are camelCase.
        bool ApplyDamage(string description, float damage, int numberOfHits);
    }

    public interface IDamageable<T>
    {
        void Damage(T damageTaken);
    }

    // CLASSES or STRUCTS:
    // - Name them with nouns or noun phrases.
    // - Avoid prefixes.
    // - One Monobehaviour per file. If you have a Monobehaviour in a file, the source file name must match. 
    public class StyleExample : MonoBehaviour
    {

        // FIELDS: 
        // - Avoid special characters (backslashes, symbols, Unicode characters); these can interfere with command line tools.
        // - Use nouns for names, but prefix booleans with a verb.
        // - Use meaningful names. Make names searchable and pronounceable. Don’t abbreviate (unless it’s math).
        // - Use Pascal case for public fields. Use camel case for private variables.
        // - Add an underscore (_) in front of private fields to differentiate from local variables
        // - You can alternatively use more explicit prefixes: m_ = member variable, s_ = static, k_ = const
        // - Specify (or omit) the default access modifier; just be consistent with your style guide.

        private int _elapsedTimeInDays;

        // Use [SerializeField] attribute if you want to display a private field in Inspector.
        // Booleans ask a question that can be answered true or false.
        [SerializeField] private bool _isPlayerDead;

        // This groups data from the custom PlayerStats class in the Inspector.
        [SerializeField] private PlayerStats _stats;

        // This limits the values to a Range and creates a slider in the Inspector.
        [Range(0f, 1f)] [SerializeField] private float _rangedStat;

        // A tooltip can replace a comment on a serialized field and do double duty.
        [Tooltip("This is another statistic for the player.")]
        [SerializeField] private float _anotherStat;


        // PROPERTIES:
        // - Preferable to a public field.
        // - Pascal case, without special characters.
        // - Use the expression-bodied properties to shorten, but choose your preferrred format.
        // - e.g. use expression-bodied for read-only properties but { get; set; } for everything else.
        // - Use the Auto-Implementated Property for a public property without a backing field.

        // the private backing field
        private int _maxHealth;

        // read-only, returns backing field
        public int MaxHealthReadOnly => _maxHealth;

        // equivalent to:
        // public int MaxHealth { get; private set; }

        // explicitly implementing getter and setter
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        // write-only (not using backing field)
        public int Health { private get; set; }

        // write-only, without an explicit setter
        public void SetMaxHealth(int newMaxValue) => _maxHealth = newMaxValue;

        // auto-implemented property without backing field
        public string DescriptionName { get; set; } = "Fireball";


        // EVENTS:
        // - Name with a verb phrase.
        // - Present participle means "before" and past participle mean "after."
        // - Use System.Action delegate for most events (can take 0 to 16 parameters).
        // - Define a custom EventArg only if necessary (either System.EventArgs or a custom struct).
        // - OR alternatively, use the System.EventHandler; choose one and apply consistently.
        // - Choose a naming scheme for events, event handling methods (subscriber/observer), and event raising methods (publisher/subject)
        // - e.g. event/action = "DoorOpened", event raising method = "OnDoorOpened", event handling method = "MySubject_DoorOpened"
   
        // event before
        public event Action OpeningDoor;

        // event after
        public event Action DoorOpened;     

        public event Action<int> PointsScored;
        public event Action<CustomEventArgs> ThingHappened;

        // These are event raising methods, e.g. OnDoorOpened, OnPointsScored, etc.
        public void OnDoorOpened()
        {
            DoorOpened?.Invoke();
        }

        public void OnPointsScored(int points)
        {
            PointsScored?.Invoke(points);
        }

        // This is a custom EventArg made from a struct.
        public struct CustomEventArgs
        {
            public int ObjectID { get; }
            public Color Color { get; }

            public CustomEventArgs(int objectId, Color color)
            {
                this.ObjectID = objectId;
                this.Color = color;
            }
        }


        // METHODS:
        // - Start a method name with a verbs or verb phrases to show an action.
        // - Parameter names are camel case.

        // Methods start with a verb.
        public void SetInitialPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        // Methods ask a question when they return bool.
        public bool IsNewPosition(Vector3 newPosition)
        {
            return (transform.position == newPosition);
        }

        private void FormatExamples(int someExpression)
        {
            // VAR:
            // - Use var if it helps readability, especially with long type names.
            // - Avoid var if it makes the type ambiguous.
            var powerUps = new List<PlayerStats>();
            var dict = new Dictionary<string, List<GameObject>>();


            // SWITCH STATEMENTS:
            // - The formatting can vary. Select one for your style guide and follow it.
            // - This example indents each case and the break underneath.
            switch (someExpression)
            {
                case 0:
                    // ..
                    break;
                case 1:
                    // ..
                    break;
                case 2:
                    // ..
                    break;
            }

            // BRACES: 
            // - Keep braces for clarity when using single-line statements.
            // - Or avoid single-line statement entirely for debuggability.
            // - Keep braces in nested multi-line statements.

            // This single-line statement keeps the braces...
            for (int i = 0; i < 100; i++) { DoSomething(i); }

            // ... but this is more debuggable. You can set a breakpoint on the clause.
            for (int i = 0; i < 100; i++)
            {
                DoSomething(i);
            }

            // Don't remove the braces here.
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    DoSomething(j);
                }
            }
        }

        private void DoSomething(int x)
        {
            // .. 
        }
    }

    // OTHER CLASSES:
    // - Define as many other helper/non-Monobehaviour classes in your file as needed.
    // - This is a serializable class that groups fields in the Inspector.
    [Serializable]
    public struct PlayerStats
    {
        public int MovementSpeed;
        public int HitPoints;
        public bool HasHealthPotion;
    }

}
