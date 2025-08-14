using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundTriggerChecker : MonoBehaviour
{   // This script checks if the object is touching any ground objects

    // Stores all currently tocuhed objects that have the "Ground" tag
    private List<GameObject> m_touchingGroundObj = new List<GameObject>();



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Add the object to the list of touching ground objects
            m_touchingGroundObj.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Remove the object from the list of touching ground objects
            m_touchingGroundObj.Remove(collision.gameObject);
        }
    }



    public bool IsTouchingGround()
    {
        // Return true if there are any objects in the touchingGroundObj list
        return m_touchingGroundObj.Count > 0;
    }
}
