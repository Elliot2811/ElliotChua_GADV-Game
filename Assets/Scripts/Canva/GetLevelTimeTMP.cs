using UnityEngine;
using TMPro;

public class LevelTimeTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with level time.

    private TextMeshProUGUI m_levelTimeText;

    private void Awake()
    {
        m_levelTimeText = GetComponent<TextMeshProUGUI>();
        if (m_levelTimeText == null)
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
            m_levelTimeText.text = $"Level Time: {minutes:00}:{seconds:00}";
        } 
    }
}
