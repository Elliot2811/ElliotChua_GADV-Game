using UnityEngine;
using TMPro;

public class GetCompletionTimeTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with level completion time

    private TextMeshProUGUI m_levelCompletionTimeText;
    public int levelNumber = 0;

    private void Awake()
    {
        m_levelCompletionTimeText = GetComponent<TextMeshProUGUI>();
        if (m_levelCompletionTimeText == null )
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        if (GameStateHandler.Instance != null && levelNumber >= 1 && levelNumber <= 3)
        {
            float completionTime = GameStateHandler.Instance.m_completionTime[levelNumber - 1];

            if (completionTime == 0)
            {
                m_levelCompletionTimeText.text = "To complete";
                return;
            }

            int minutes = Mathf.FloorToInt(completionTime / 60);
            int seconds = Mathf.FloorToInt(completionTime % 60);
            m_levelCompletionTimeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
