using UnityEngine;
using TMPro;

public class GetWinLevelTimeTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with level time.

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
            float levelTime = GameStateHandler.Instance.m_lastLevelTime;
            string levelName = GameStateHandler.Instance.m_levelName;
            int minutes = Mathf.FloorToInt(levelTime / 60);
            int seconds = Mathf.FloorToInt(levelTime % 60);
            m_winLevelTimeText.text = $"{levelName}\n{minutes:00}:{seconds:00}";
        }
    }
}

