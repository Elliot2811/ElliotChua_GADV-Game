using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{ // This script updates the button.onClick to the next level

    private Button m_nextLevelButton;

    private void Awake()
    {
        m_nextLevelButton = GetComponent<Button>();
        if (m_nextLevelButton == null)
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    private void OnEnable()
    {
        if (GameStateHandler.Instance != null)
        {
            string levelSceneName = GameStateHandler.Instance.m_levelName;

            if (levelSceneName == "Level 3")
            {
                m_nextLevelButton.onClick.AddListener(() =>
                {
                    GameStateHandler.Instance.LoadLevelSelect();
                });
                return;
            }

            if (levelSceneName == "Level 2")
            {
                levelSceneName = "Level 3";
            }

            if (levelSceneName == "Level 1")
            {
                levelSceneName = "Level 2";
            }

            m_nextLevelButton.onClick.AddListener(() =>
            {
                GameStateHandler.Instance.LoadLevel(levelSceneName);
            });
        }
    }
}
