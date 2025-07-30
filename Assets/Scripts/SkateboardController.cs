using UnityEngine;

public class SkateboardController : MonoBehaviour
{
    public GameObject board;    
    private Rigidbody2D m_boardRb;

    public GameObject frontWheel;
    private WheelController m_frontWheelScript;

    public GameObject backWheel;
    private WheelController m_backWheelScript;

    private void Start()
    {
        m_boardRb = board.GetComponent<Rigidbody2D>();
        m_frontWheelScript = frontWheel.GetComponent<WheelController>();
        m_backWheelScript = backWheel.GetComponent<WheelController>();
    }

    public bool IsFrontWheelOnGround()
    {
        return m_frontWheelScript.IsTouchingGround();
    }

    public bool IsBackWheelOnGround()
    {
        return m_backWheelScript.IsTouchingGround();
    }

    // Note: Force applied not on world x space but local x space
    public void move(float xforce, float ForceInDirection)
    {
        m_boardRb.AddRelativeForce(new Vector2(xforce * ForceInDirection * Time.deltaTime, 0.0f));
    }
}
