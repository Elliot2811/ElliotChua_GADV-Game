using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private Rigidbody2D m_playerRb;

    private PlayerController m_playerController;

    public GameObject skateboard; // Reference to skateboard player is currently on
    private SkateboardController m_skateboardController;
    private bool m_onBoard = false;

    // Input values for skateboard movement
    public float AccelerationForce = 5000.0f; // Force applied to skateboard when accelerating
    public float DecelerationForce = 10000.0f; // Force applied to skateboard when decelerating
    public float JumpVelocity = 30.0f; // How high player can jump when on skateboard
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
            if (m_skateboardController != null)
            {
                m_onBoard = true; // Player is on a skateboard
            }
            else
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
        m_horizontalInput = Input.GetAxis("Horizontal");

        if (m_onBoard)
        {
            OnBoard();
        }
        else // Player is off the board
        {
            OffBoard();
        }
    }

    private void OnBoard()
    {
        if (m_skateboardController.IsFrontWheelOnGround() && m_skateboardController.IsBackWheelOnGround()) // Player is on flat ground
        {
            // Debug.Log("Pkayer on flat ground");
            if (m_horizontalInput != 0.0f) // Player is trying to move
            {
               // Debug.Log("Player moving");
               moveBoard();
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                m_playerController.Jump(JumpVelocity);
            }
        }
        else if (m_skateboardController.IsFrontWheelOnGround() || m_skateboardController.IsBackWheelOnGround()) // Player is doing a manual
        {

        }
        else // Player is in the air
        {
            m_playerController.RotatePlayer(m_horizontalInput);
        }
    }

    private void OffBoard()
    {
        m_playerController.RotatePlayer(m_horizontalInput);
    }

    private void moveBoard()
    {
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
