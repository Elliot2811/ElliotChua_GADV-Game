using UnityEngine;
using UnityEngine.UI;

public class RetryLevelButton : MonoBehaviour
{ // This script updates the button.onClick to retry a level

    private Button m_retryLevelButton;

    private void Awake()
    {
        m_retryLevelButton = GetComponent<Button>();
        if (m_retryLevelButton == null)
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        if (GameStateHandler.Instance != null)
        {
            m_retryLevelButton.onClick.AddListener(() =>
            {
                string levelSceneName = GameStateHandler.Instance.m_levelName;
                //Debug.Log($"Current scene: {levelSceneName}");
                GameStateHandler.Instance.LoadLevel(levelSceneName);
            });
        }
    }
}
