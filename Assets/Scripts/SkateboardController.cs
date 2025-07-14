using UnityEngine;

public class SkateboardController : MonoBehaviour
{
    public GameObject board;
    private Rigidbody2D m_boardRb;

    public GameObject frontWheel;
    private WheelGroundChecker m_frontWheelScript;

    public GameObject backWheel;
    private WheelGroundChecker m_backWheelScript;

    private void Start()
    {
        m_boardRb = board.GetComponent<Rigidbody2D>();
        m_frontWheelScript = frontWheel.GetComponent<WheelGroundChecker>();
        m_backWheelScript = backWheel.GetComponent<WheelGroundChecker>();
    }

    public bool IsFlatOnGround()
    {
        return m_frontWheelScript.IsOnGround() && m_backWheelScript.IsOnGround();
    }

    public bool IsFrontWheelOnGround()
    {
        return m_frontWheelScript.IsOnGround();
    }

    public bool IsBackWheelOnGround()
    {
        return m_backWheelScript.IsOnGround();
    }

    // Note: Force applied not on world x space but local x space
    public void move(float xforce, float ForceInDirection)
    {
        m_boardRb.AddRelativeForce(new Vector2(xforce * ForceInDirection * Time.deltaTime, 0.0f));
    }
}
