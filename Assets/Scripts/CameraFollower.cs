using UnityEngine;

public class CameraFollower : MonoBehaviour
{   // This script is attached to the camera and makes it follow the player

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
        // Basic camera follow logic (camera x and y and constantly following player)
        if (Player != null)
        {
            m_playerPos = Player.transform.position;
            m_playerPos.z = transform.position.z;
            transform.position = m_playerPos; 
        }
    }
}
 