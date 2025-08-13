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

    private void Start()
    {
        m_boardRb = GetComponent<Rigidbody2D>();
        m_frontWheelScript = frontWheel.GetComponent<GroundTriggerChecker>();
        m_backWheelScript = backWheel.GetComponent<GroundTriggerChecker>();
    }


    public void move(float xforce, float ForceInDirection)
    {
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
