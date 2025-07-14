using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_playerRb;

    public GameObject Skateboard;
    private SkateboardController m_skateboardController; // To handle acceleration and decceleration of the skateboard

    private FixedJoint2D m_fixedJoint;
    private bool m_onBoard = true; // TODO check if on board

    // Values in unit applied per second
    public float ForceInDirection = 5000.0f;
    public float ForceOppositeDirection = 10000.0f;
    public float TorqueInDirection = 1000.0f;

    public float JumpHeight = 30.0f; // How high player can jump when on skateboard

    // Global values for functions to use
    private float m_horizontalInput = 0;

    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_skateboardController = Skateboard.GetComponent<SkateboardController>();
        m_fixedJoint = GetComponent<FixedJoint2D>();
    }

    private void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");

        // Check if player is on board to do tricks
        if (m_onBoard)
        {
            // Check if player can accelerate or deccelerate (requires player to be flat and ready to push/power slide)
            if (IsFlatOnGround())
            {
                // Check if from rest or pushing in direction of board
                if (m_playerRb.velocity.x == 0.0f ||
                    Mathf.Sign(m_playerRb.velocity.x) < 0.0f && m_horizontalInput < 0.0f ||
                    Mathf.Sign(m_playerRb.velocity.x) > 0.0f && m_horizontalInput > 0.0f)
                {
                    AccelerateSkateboard();
                }
                else
                {
                    DeccelerateSkateboard();
                }

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
            else if (IsFrontWheelOnGround() || IsBackWheelOnGround()) // Check if player is doing a manual
            {   
                AccelerateSkateboard(0.75f);
                RotatePlayer();
            }
            else // If player is not on both wheels, or one of the wheels, then player is upsidedown or in the air
            {
                RotatePlayer();
            }
        }
    }

    // Accelerating is harder than deccelerating, hence different force values
    private void AccelerateSkateboard(float mult = 1.0f) // For added control over movement when doing a manual
    {
        m_skateboardController.move(m_horizontalInput, ForceInDirection * mult);
    }
    private void DeccelerateSkateboard()
    {
        m_skateboardController.move(m_horizontalInput, ForceOppositeDirection);
    }

    // When player goes right, they expect clockwise rotation, hence the negative sign
    private void RotatePlayer(float mult = 1.0f) // mult it to make manualling easier
    {
        m_playerRb.AddTorque(-m_horizontalInput * TorqueInDirection * mult * Time.deltaTime, ForceMode2D.Force);
    }

    private void Jump()
    {
        Vector2 localUp = transform.TransformDirection(Vector2.up);
        localUp.Normalize();

        m_playerRb.velocity = new Vector2 (m_playerRb.velocity.x + localUp.x * JumpHeight, localUp.y * JumpHeight);
    }

    // m_skateboardController.<function> to make code cleaner
    private bool IsFlatOnGround()
    {
        return m_skateboardController.IsFlatOnGround();
    }

    public bool IsFrontWheelOnGround()
    {
        return m_skateboardController.IsFrontWheelOnGround();
    }

    public bool IsBackWheelOnGround()
    {
        return m_skateboardController.IsBackWheelOnGround();
    }
}
