using UnityEngine;

public class PlayerController : MonoBehaviour
{    // This script handles forces appled on player such as jumping and rotation.
     // It also handles the player sprite flipping based on movement direction.

    private Rigidbody2D m_playerRb;

    public GameObject Skateboard;

    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        if (m_playerRb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
            return;
        }

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        if (m_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    
    public void RotatePlayer(float m_horizontalInput, float TorqueInDirection, float maxRotationSpeed = 270.0f)
    {
        if (m_playerRb != null)
        {
            // When player goes right, they expect clockwise rotation. Neg sign for rotation direction to be expected
            m_playerRb.AddTorque(-m_horizontalInput * TorqueInDirection * Time.deltaTime, ForceMode2D.Force);
            float angularVelocity = m_playerRb.angularVelocity;

            if (angularVelocity > maxRotationSpeed)
            {
                angularVelocity = maxRotationSpeed; // Cap max rotation speed (counter-clockwise)
            }
            else if (angularVelocity < -maxRotationSpeed)
            {
                angularVelocity = -maxRotationSpeed; // Cap max rotation speed (clockwise)
            }
            else
            {   
                // No change in angular velocity
                return; 
            }

            m_playerRb.angularVelocity = angularVelocity;
        }
        
    }

    public void Jump(float JumpVelocity)
    {
        if (m_playerRb != null)
        {
            // Finds the vector between the local up of the player in world space
            Vector2 direction = transform.TransformDirection(Vector2.up);
            direction.Normalize();

            m_playerRb.velocity = new Vector2(m_playerRb.velocity.x + direction.x * JumpVelocity, direction.y * JumpVelocity);
        }
    }

    public float PlayerSpriteDirection()
    {   // Flips the player sprite based on the direction of movement
        if (m_playerRb != null && m_spriteRenderer != null)
        {   
            float temp = m_playerRb.velocity.x;

            if (temp > 0)
            {
                m_spriteRenderer.flipX = false;
            }
            else if (temp < 0)
            {
                m_spriteRenderer.flipX = true;
            }

            return temp;
        }
        return 0.0f; // Return 0 if Rigidbody2D or SpriteRenderer is not found
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("Player hit a spike (player)");
            GameStateHandler.Instance.Lose();
        }
    }
}
