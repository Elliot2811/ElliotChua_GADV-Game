using UnityEngine;
using TMPro;

public class LevelTimeTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with level time.

    private TextMeshProUGUI levelTimeText;

    private void Awake()
    {
        levelTimeText = GetComponent<TextMeshProUGUI>();
        if (levelTimeText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }
    }

    private void FixedUpdate()
    {
        if (GameStateHandler.Instance != null)
        {
            float levelTime = GameStateHandler.Instance.m_currentLevelTime;
            int minutes = Mathf.FloorToInt(levelTime / 60);
            int seconds = Mathf.FloorToInt(levelTime % 60);
            levelTimeText.text = $"Level Time: {minutes:00}:{seconds:00}";
        } 
    }
}
