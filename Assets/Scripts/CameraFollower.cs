using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject Player;
    private Vector3 m_playerPos;

    private void Start()
    {
        if (Player != null)
        {
            m_playerPos = Player.transform.position;
        }
    }

    private void LateUpdate()
    {
        if (Player != null)
        {
            m_playerPos = Player.transform.position;
            m_playerPos.z = transform.position.z;
            transform.position = m_playerPos; 
        }
    }
}
