using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject lockBlock;
    private Lock lockScr;

    private void Start()
    {
        if (lockBlock != null)
        {
            lockScr = lockBlock.GetComponent<Lock>();
            if (lockScr == null)
            {
                Debug.LogError("Lock script not found on the lock block.");
            }
        }
        else
        {
            Debug.LogError("Lock block is not assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lockBlock == null || lockScr == null)
        {
            return;
        }

        if (collision.CompareTag("Player") |
            collision.CompareTag("Skateboard") ||
            collision.CompareTag("Wheel")
            )
        {
            lockScr.unlock();
        }

        Destroy(gameObject);
    }
}
