using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject SkateBoard;
    private Rigidbody m_rb;
    private FixedJoint m_fixedJoint;

    public float ForceInDirection = 5000.0f;
    public float ForceOppositeDirection = 10000.0f;
    public float TorqueInDirection = 1000.0f;
    public float JumpHeight = 30.0f;

    private bool on_board = true; // TODO create function to check if on board

    private void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_fixedJoint = GetComponent<FixedJoint>();
    }


}
