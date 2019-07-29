using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    internal static SpawnManager instance;

    float xOffset;

    [SerializeField, Tooltip("Using Vectors to easily store 2 floats. X = time between spawns, Y = time this difficulty is active")]
    Vector2[] spawnTimers;

    [SerializeField]
    float difficultyWaitTime = 1f;

    int difficultyIndex = 0;

    float spawnClock;
    float difficultyClock;

    [SerializeField]
    GameObject cyanObstaclePrefab;

    [SerializeField]
    GameObject pinkObstaclePrefab;

    [SerializeField]
    GameObject whiteObstaclePrefab;

    [SerializeField]
    GameObject orangeObstaclePrefab;

    [SerializeField]
    Vector3[] positions;

    internal class Level
    {
        internal Section[] sections;
        internal Level(Section[] sections)
        {
            this.sections = sections;
        }
    }

    internal class Section
    {
        internal string pattern;
        internal float delay;
        internal float speed;

        internal Section(string pattern, float delay, float speed)
        {
            this.pattern = pattern;
            this.delay = delay;
            this.speed = speed;
        }
    }

    int currentLevelIndex = 0;
    int currentSectionIndex = 0;

    List<Level> levels = new List<Level>();

    bool waiting;
    float waitClock = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Section[] level1Sections = {
            new Section("c  ", 1, 9),
            new Section("p  ", 1, 9),
            new Section("c p", 1, 9),
            new Section("  p", 1, 9),
            new Section("c p", 1, 9),
            new Section("p c", 1, 9),
            new Section("c p", 1, 9),
            new Section("  c", 1, 9),
            new Section("c p", 1, 9),
            new Section("p c", 1, 9),
            new Section("c p", 1, 9),
            new Section("p c", 1, 9),
            new Section("c p", 1, 9),
            new Section("p c", 1, 9),
            new Section("c p", 1, 9),
            new Section("c p", 1, 9),
        };
        levels.Add(new Level(level1Sections));

        Section[] level2Sections =
        {
            new Section("cwp", 1, 10),
            new Section("pwc", 1, 10),
            new Section("c p", 1, 10),
            new Section("p c", 1, 10),
            new Section("w w", 1, 10),
            new Section("cwp", 1, 10),
            new Section("w w", 1, 10),
            new Section(" w ", 1, 10),
            new Section("w w", 1, 10),
            new Section(" w ", 1, 10),
            new Section(" cp", 1, 10),
            new Section("pw ", 1, 10),
            new Section("p w", 1, 10),
            new Section("c p", 1, 10),
            new Section("pwc", 1, 10),
            new Section("cwp", 1, 10),
        };
        levels.Add(new Level(level2Sections));

        Section[] level3Sections = {
            new Section("wow", 1, 9),
            new Section(" o ", 1, 9),
            new Section("o o", 1, 9),
            new Section(" o ", 1, 9),
            new Section("cwp", 1, 9),
            new Section("cop", 1, 9),
            new Section("cow", 1, 9),
            new Section("woc", 1, 9),
            new Section("ooo", 1, 9),
            new Section("cwp", 1, 9),
            new Section("pwc", 1, 9),
            new Section("p w", 1, 9),
            new Section("c p", 1, 9),
            new Section(" o ", 1, 9),
            new Section("woc", 1, 9),
            new Section("pow", 1, 9),
        };
        levels.Add(new Level(level3Sections));
    }

    private void Update()
    {
        if (waitClock > 0)
        {
            waitClock -= Time.deltaTime;
        }
        else
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Debug.Log(levels);

        var section = levels[currentLevelIndex].sections[currentSectionIndex];
        var pattern = section.pattern;
        var delay = section.delay;
        var speed = section.speed;
        var patternChars = pattern.ToCharArray();

        IncrementSection();

        waitClock = delay;
        
        Debug.Log(pattern + ", " + delay + ", " + speed);

        // Read each letter in order
        // Spawn a particular obstacle based on letter and position
        for (var i = 0; i < patternChars.Length; i++)
        {
            switch(patternChars[i])
            {
                case 'c':
                    SpawnPrefab(cyanObstaclePrefab, positions[i], speed);
                    break;
                case 'p':
                    SpawnPrefab(pinkObstaclePrefab, positions[i], speed);
                    break;
                case 'w':
                    SpawnPrefab(whiteObstaclePrefab, positions[i], speed);
                    break;
                case 'o':
                    SpawnPrefab(orangeObstaclePrefab, positions[i], speed);
                    break;
                default:
                    break;
            }

        }
    }

    void SpawnPrefab(GameObject prefab, Vector3 position, float speed)
    {
            var go = Instantiate(prefab, position, Quaternion.identity);
            go.GetComponent<MoveObject>().SetSpeed(speed);
    }

    void IncrementSection()
    {
        if (currentSectionIndex == levels[currentLevelIndex].sections.Length - 1)
        {
            currentSectionIndex = 0;
            currentLevelIndex++;
        }
        else
        {
            currentSectionIndex++;
        }
    }
}
