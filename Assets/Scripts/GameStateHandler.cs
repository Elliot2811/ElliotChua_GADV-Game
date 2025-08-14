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

    public void LoadLevelSelectScreen()
    {
        SceneManager.LoadScene("LevelSelectScreen", LoadSceneMode.Single);
    }

    public void LoadLevel(int level)
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
                break;
            default:
                
                break;
        }
    }

    public void WinStage()
    {
        SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
    }

    public void Lose()
    {
        // Load the "LoseScene" when the player loses (player falls or run out of time)
        // Debug.Log("LoseScene loaded");
        SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
    }

    public void Retry()
    {
        // Debug.Log("TestScene Loaded");
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }
}
