using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private Rigidbody2D m_playerRb;

    private PlayerController m_playerController;

    public GameObject skateboard; // Reference to skateboard player is currently on
    private SkateboardController m_skateboardController;


    // Input values for skateboard movement
    public float AccelerationForce = 5000.0f; // Force applied to skateboard when accelerating
    public float DecelerationForce = 10000.0f; // Force applied to skateboard when decelerating
    public float TorqueInDirection = 1000.0f; // Torque applied to skateboard when turning
    public float maxRotationSpeed = 180.0f; // Maximum rotation speed in degrees per second

    public float JumpForce = 30.0f; // How high player can jump when on skateboard
    // End of Input values for skateboard movement

    // Temp values for input
    private float m_horizontalInput = 0.0f;
    // End of Temp values for input

    private void Start()
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
            return;
        }

        if (skateboard != null)
        {
            m_skateboardController = skateboard.GetComponent<SkateboardController>();
            if (m_skateboardController == null)
            {
                Debug.LogError("SkateboardController component not found on skateboard GameObject.");
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
            return; // Early return
        }

        m_horizontalInput = Input.GetAxis("Horizontal");

        if (m_skateboardController.IsFrontWheelOnGround() && m_skateboardController.IsBackWheelOnGround()) // Player is on flat ground
        {
            // Debug.Log("Pkayer on flat ground");
            if (m_horizontalInput != 0.0f) // Player is trying to move
            {
                // Debug.Log("Player moving");
                moveBoard();
                m_playerController.playerDirection();
            }

            if (Input.GetButtonDown("Jump"))
            {
                m_playerController.Jump(JumpForce);
            }
        }
        else if (m_skateboardController.IsFrontWheelOnGround() || m_skateboardController.IsBackWheelOnGround()) 
        {   // Player is performing a manual
            m_playerController.RotatePlayer(m_horizontalInput, TorqueInDirection, 15.0f);
        }
        else // Player is in the air
        {
            m_playerController.RotatePlayer(m_horizontalInput, TorqueInDirection);
        }
    }

    private void moveBoard()
    {
        if (m_playerController == null || m_skateboardController == null)
        {
            return; // Early return
        }

        if (m_playerRb.velocity.x == 0.0f ||
            (Mathf.Sign(m_playerRb.velocity.x) < 0.0f && m_horizontalInput < 0.0f) ||
            (Mathf.Sign(m_playerRb.velocity.x) > 0.0f && m_horizontalInput > 0.0f))
        {
            m_skateboardController.move(m_horizontalInput, AccelerationForce);
        }
        else
        {
            m_skateboardController.move(m_horizontalInput, DecelerationForce);
        }
    }
}
