using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the game state, such as losing and retrying the game and entering new levels.
public class GameStateHandler : MonoBehaviour
{   

    public static GameStateHandler Instance { get; private set; }

    public float m_totalGameTime { get; private set; }
    public float m_currentLevelTime { get; private set; }
    public float m_lastLevelTime { get; private set; }

    [SerializeField] private string[] LevelScenes; // Array of level scene names modifiable in the inspector
    [SerializeField] private string[] checkCompletions;

    public string m_levelName { get; private set; } = "Unknown";
    public string m_loseReason { get; private set; } = "Unknown"; // Reason for losing the game, e.g., "Player fell" or "Ran out of time"

    public float[] m_completionTime { get; private set; } = new float[3];

    public void Awake()
    {   // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    { // Check If playe has won on every scene load
        SceneManager.sceneLoaded += CheckWin;
    }

    private void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // NOTE: Needed anonymous function to convert System.Predicate<String> to String through implicit conversion
        if (Array.Exists(LevelScenes, scene => scene == currentSceneName)) 
        {
            float temp = Time.deltaTime;
            m_totalGameTime += temp;
            m_currentLevelTime += temp;
        }
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect", LoadSceneMode.Single);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void WinLevel()
    {
        m_levelName = SceneManager.GetActiveScene().name; // Store the current level name before switching scenes

        if (m_levelName == "Level 1")
        {
            if (m_completionTime[0] == 0) m_completionTime[0] = m_currentLevelTime;
            else m_completionTime[0] = Math.Min(m_completionTime[0], m_currentLevelTime);
        }
        else if (m_levelName == "Level 2")
        {
            if (m_completionTime[1] == 0) m_completionTime[1] = m_currentLevelTime;
            else m_completionTime[1] = Math.Min(m_completionTime[1], m_currentLevelTime);
        }
        else if (m_levelName == "Level 3")
        {
            if (m_completionTime[2] == 0) m_completionTime[2] = m_currentLevelTime;
            else m_completionTime[2] = Math.Min(m_completionTime[2], m_currentLevelTime);
        }
        
        m_lastLevelTime = m_currentLevelTime;
        m_currentLevelTime = 0; // Reset the current level time for the next level

        //Debug.Log("WinScene loaded");
        SceneManager.LoadScene("WinLevel", LoadSceneMode.Single);
    }

    public void LoseLevel(string reason)
    {
        m_loseReason = reason; // Set the reason for losing
        m_levelName = SceneManager.GetActiveScene().name; // Store the current level name before switching scenes
        m_lastLevelTime = m_currentLevelTime; // Store the time spent in the last level
        m_currentLevelTime = 0; // Reset the current level time for the next level

        // Debug.Log("LoseScene loaded");
        SceneManager.LoadScene("LoseLevel", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void CheckWin(Scene scene, LoadSceneMode mode) // Check if player has completed every level
    {
        bool win = true;
        foreach (float time in m_completionTime)
        {
            if (time == 0)
            {
                win = false;
            }
        }

        if (win)
        {
            SceneManager.sceneLoaded -= CheckWin;

            SceneManager.LoadScene("WinGame", LoadSceneMode.Single);
        }
    }
}
