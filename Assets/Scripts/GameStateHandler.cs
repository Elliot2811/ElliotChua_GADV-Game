using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{   // This script handles the game state, such as losing and retrying the game and entering new levels.

    // Singleton instance
    public static GameStateHandler Instance;

    public void Awake()
    {
        Instance = this;        
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
