using UnityEngine;
using TMPro;

public class GetTotalTimeTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with level time.

    private TextMeshProUGUI m_totalTimeText;
    private TextMeshProUGUI m_winLevelTimeText;

    private void Awake()
    {
        //Debug.Log("LevelTimeTMP Awake called.");

        m_winLevelTimeText = GetComponent<TextMeshProUGUI>();
        if (m_winLevelTimeText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        if (GameStateHandler.Instance != null)
        {
            float levelTime = GameStateHandler.Instance.m_totalGameTime;
            int minutes = Mathf.FloorToInt(levelTime / 60);
            int seconds = Mathf.FloorToInt(levelTime % 60);
            m_winLevelTimeText.text = $"Total game time:\n{minutes:00}:{seconds:00}";
        }
    }
}

