using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   // This script handles player input and player logic (weather the player will jump or rotate)

    private Rigidbody2D m_playerRb;

    private PlayerController m_playerController;

    public GameObject skateboard; // Reference to skateboard player is currently on
    private SkateboardController m_skateboardController;

    // Input values for skateboard movement
    public float AccelerationForce = 2500.0f; // Force applied to skateboard when accelerating
    public float DecelerationForce = 4000.0f; // Force applied to skateboard when decelerating
    public float TorqueInDirection = 1500.0f; // Torque applied to skateboard when turning
    public float maxRotationSpeed = 180.0f; // Maximum rotation speed in degrees per second
    public float JumpForce = 40.0f; // How high player can jump when on skateboard
    // End of Input values for skateboard movement

    private float m_horizontalInput = 0.0f;



    private void Awake()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        if (m_playerRb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
            return;
        }

        m_playerController = GetComponent<PlayerController>();
        if (m_playerController == null)
        {
            Debug.LogError("PlayerController component not found on this GameObject.");
        }
    }

    private void Start()
    {
        if (skateboard != null)
        {
            m_skateboardController = skateboard.GetComponent<SkateboardController>();
            if (m_skateboardController == null)
            {
                Debug.LogError("SkateboardController component not found on skateboard GameObject.");
                return;
            }
        }
        else
        {
            Debug.LogError("Skateboard GameObject reference is not set.");
        }
    }

    private void Update()
    {
        if (m_skateboardController == null || m_playerController == null)
        {
            return;
        }

        // Get horizontal input from player
        m_horizontalInput = Input.GetAxis("Horizontal");

        // Gets information about which wheels are on the ground
        bool frontGrounded = m_skateboardController.IsFrontWheelOnGround();
        bool backGrounded = m_skateboardController.IsBackWheelOnGround();

        if (frontGrounded && backGrounded) // Player is on flat ground  
        {
            // Debug.Log("Player on flat ground");
            if (m_horizontalInput != 0.0f) // Checks if the player is trying to move
            {
                // Debug.Log("Player moving");
                moveBoard();
                m_playerController.PlayerSpriteDirection(); // Updates player sprite direction based on movement
                m_skateboardController.SkateboardSpriteDirection(); // Updates skateboard sprite direction based on movement
            }

            if (Input.GetButtonDown("Jump")) // Checks if player uses jump button
            {
                m_playerController.Jump(JumpForce);
            }
        }
        else if (frontGrounded || backGrounded) 
        {   // Player is performing a manual
            moveBoard(0.25f);
            m_playerController.RotatePlayer(m_horizontalInput, TorqueInDirection * 1.5f, 15.0f);
        }
        else 
        {   // Player is in the air
            m_playerController.RotatePlayer(m_horizontalInput, TorqueInDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { // Check if the player (not the skateboard) collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameStateHandler.Instance.LoseLevel("CollisionGround");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            GameStateHandler.Instance.LoseLevel("TriggerSpike");
        }
    }



    private void moveBoard(float mult = 1)
    {
        if (m_playerController == null || m_skateboardController == null)
        {
            return;
        }

        // Checks if the player is accelerating or decelerating on x axis
        // Acceleration and deceleration forces in change is different
        if (m_playerRb.velocity.x == 0.0f ||
            (Mathf.Sign(m_playerRb.velocity.x) < 0.0f && m_horizontalInput < 0.0f) ||
            (Mathf.Sign(m_playerRb.velocity.x) > 0.0f && m_horizontalInput > 0.0f))
        {
            m_skateboardController.move(m_horizontalInput, AccelerationForce * mult);
        }
        else
        {
            m_skateboardController.move(m_horizontalInput, DecelerationForce * mult);
        }
    }
}
