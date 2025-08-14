using Unity.VisualScripting;
using UnityEngine;

public class SkateboardController : MonoBehaviour
{   // This script handles forces applied on skateboard such as movement
    // 
    private Rigidbody2D m_boardRb;

    public GameObject frontWheel;
    private GroundTriggerChecker m_frontWheelScript;

    public GameObject backWheel;
    private GroundTriggerChecker m_backWheelScript;

    public float maxSpd = 10.0f; // Maximum speed of the skateboard

    private SpriteRenderer m_spriteRenderer;

    private void Start()
    {
        m_boardRb = GetComponent<Rigidbody2D>();
        if (m_boardRb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
            return;
        }

        m_frontWheelScript = frontWheel.GetComponent<GroundTriggerChecker>();
        if (m_frontWheelScript == null)
        {
            Debug.LogError("GroundTriggerChecker component not found on front wheel GameObject.");
            return;
        }

        m_backWheelScript = backWheel.GetComponent<GroundTriggerChecker>();
        if (m_backWheelScript == null)
        {
            Debug.LogError("GroundTriggerChecker component not found on back wheel GameObject.");
            return;
            
        }

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        if (m_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }


    public void move(float xforce, float ForceInDirection)
    {
        if (m_boardRb == null)
        {
            return;
        }

        //Adds forces to the skateboard in the x direction
        // Note: Forces are applied not on world x space but local x space
        m_boardRb.AddForce(new Vector2(xforce * ForceInDirection * Time.deltaTime, 0.0f));

        // Cap the skateboard's velocity to the maximum speed
        float velocityX = m_boardRb.velocity.x;
        if (velocityX > maxSpd)
        {
            velocityX = maxSpd;
        }
        else if (velocityX < -maxSpd)
        {
            velocityX = -maxSpd;
        } 
        else
        {
            // No need to modify the velocity if it's within the limits
            return;
        }

        m_boardRb.velocity = new Vector2(velocityX, m_boardRb.velocity.y);
    }
    
    public void SkateboardSpriteDirection(float playerXVelocity)
    {
        if (playerXVelocity > 0)
        {
            m_spriteRenderer.flipX = false;
        }
        else if (playerXVelocity < 0)
        {
            m_spriteRenderer.flipX = true;
        }
    }

    public bool IsFrontWheelOnGround()
    {
        return m_frontWheelScript.IsTouchingGround();
    }

    public bool IsBackWheelOnGround()
    {
        return m_backWheelScript.IsTouchingGround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Debug.Log("Player hit a spike (skateboard)");
            GameStateHandler.Instance.Lose();
        }
    }
}
