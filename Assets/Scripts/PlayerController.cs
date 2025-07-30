using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_playerRb;

    public GameObject Skateboard;
    private SkateboardController m_skateboardController; // To handle acceleration and decceleration of the skateboard

    // Values in unit applied per second
    public float ForceInDirection = 5000.0f;
    public float ForceOppositeDirection = 10000.0f;
    public float TorqueInDirection = 1000.0f;

    public float JumpHeight = 30.0f; // How high player can jump when on skateboard


    private void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_skateboardController = Skateboard.GetComponent<SkateboardController>();
    }

    // When player goes right, they expect clockwise rotation, hence the negative sign
    public void RotatePlayer(float m_horizontalInput)
    {
        m_playerRb.AddTorque(-m_horizontalInput * TorqueInDirection * Time.deltaTime, ForceMode2D.Force);
    }

    public void Jump(float JumpVelocity)
    {
        Vector2 localUp = transform.TransformDirection(Vector2.up);
        localUp.Normalize();

        m_playerRb.velocity = new Vector2 (m_playerRb.velocity.x + localUp.x * JumpVelocity, localUp.y * JumpVelocity);
    }
}
