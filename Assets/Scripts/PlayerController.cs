using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        if (m_playerRb == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    // When player goes right, they expect clockwise rotation, hence the negative sign
    public void RotatePlayer(float m_horizontalInput, float TorqueInDirection, float maxRotationSpeed = 180.0f)
    {
        if (m_playerRb != null)
        {
            m_playerRb.AddTorque(-m_horizontalInput * TorqueInDirection * Time.deltaTime, ForceMode2D.Force);
            float rotation = m_playerRb.angularVelocity;

            if (rotation > maxRotationSpeed)
            {
                rotation = maxRotationSpeed;
            }
            else if (rotation < -maxRotationSpeed)
            {
                rotation = -maxRotationSpeed;
            }
            else
            {
                return; // No need to set the torque if it's within bounds
            }

            m_playerRb.angularVelocity = rotation;
        }
        
    }

    public void Jump(float JumpForce)
    {
        if (m_playerRb != null)
        {
            Vector2 localUp = transform.TransformDirection(Vector2.up);
            localUp.Normalize();

            m_playerRb.velocity = new Vector2(m_playerRb.velocity.x + localUp.x * JumpForce, localUp.y * JumpForce);
        }
    }

    public void playerDirection()
    {
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
        }
    }
}
