using UnityEngine;
using UnityEngine.UI;

public class RetryLevelButton : MonoBehaviour
{
    private Button m_retryLevelButton;

    private void Awake()
    {
        m_retryLevelButton = GetComponent<Button>();
        if (m_retryLevelButton == null)
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    private void Start()
    {
        if (GameStateHandler.Instance != null)
        {
            m_retryLevelButton.onClick.AddListener(() =>
            {
                GameStateHandler.Instance.LoadLevel(GameStateHandler.Instance.m_LevelNumber);
            });
        }
    }
}
