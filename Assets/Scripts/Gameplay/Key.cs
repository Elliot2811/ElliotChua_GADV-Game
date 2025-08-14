using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{ // This script handles the key logic (i.e. tells lockBlock object to unlock)

    public GameObject lockBlock;
    private Lock lockScript;



    private void Start()
    {
        if (lockBlock != null)
        {
            lockScript = lockBlock.GetComponent<Lock>();
            if (lockScript == null)
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
        if (lockBlock == null || lockScript == null)
        {
            return;
        }

        if (collision.CompareTag("Player") |
            collision.CompareTag("Skateboard") ||
            collision.CompareTag("Wheel")
            )
        {
            lockScript.unlock();
        }

        Destroy(gameObject);
    }
}
