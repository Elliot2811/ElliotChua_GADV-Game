using System.Collections.Generic;
using UnityEngine;

public class WheelGroundChecker : MonoBehaviour
{
    // Stores all currently tocuhed objects that have the "Ground" tag
    private List<GameObject> touchingGroundObj = new List<GameObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGroundObj.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGroundObj.Remove(collision.gameObject);
        }
    }

    public bool IsOnGround()
    {
        return touchingGroundObj.Count > 0;
    }
}
