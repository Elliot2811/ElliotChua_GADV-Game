using UnityEngine;

public class CassetteTape : MonoBehaviour
{   // This script handles player interaction with cassette tape/the goal in the game.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Skateboard") ||
            collision.gameObject.CompareTag("Wheel")
            )
        {
            GameStateHandler.Instance.WinStage();
        }
    }
}
