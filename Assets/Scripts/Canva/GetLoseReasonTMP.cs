using UnityEngine;
using TMPro;

public class GetLoseReasonTMP : MonoBehaviour
{ // This script updates the TextMeshProUGUI component with the reason for losing the game.

    private TextMeshProUGUI m_loseReasonText;
    private void Awake()
    {
        m_loseReasonText = GetComponent<TextMeshProUGUI>();
        if (m_loseReasonText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
        }
    }
    private void FixedUpdate()
    {
        if (GameStateHandler.Instance != null)
        {
            string text = GameStateHandler.Instance.m_loseReason;

            if (text == "CollisionGround")
            {
                m_loseReasonText.text = "You crashed into the ground";
            }
            else if (text == "TriggerSpike")
            {
                m_loseReasonText.text = "Stay away from sharp spikes";
            }
            else if (text == "CollisionEnemy")
            {
                m_loseReasonText.text = "Enemy killed you";
            }
            else
            {
                m_loseReasonText.text = "You lost for an unknown reason";
            }
        }
    }

}
