using UnityEngine;
using TMPro;

public class GetLastLevelTimeTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with level time.

    private TextMeshProUGUI m_lastLevelTimeText;

    private void Awake()
    {
        //Debug.Log("LevelTimeTMP Awake called.");

        m_lastLevelTimeText = GetComponent<TextMeshProUGUI>();
        if (m_lastLevelTimeText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        if (GameStateHandler.Instance != null)
        {
            float levelTime = GameStateHandler.Instance.m_lastLevelTime;
            int minutes = Mathf.FloorToInt(levelTime / 60);
            int seconds = Mathf.FloorToInt(levelTime % 60);
            m_lastLevelTimeText.text = $"Time Spent: {minutes:00}:{seconds:00}";
        }
    }
}
