using UnityEngine;

public class Enemy : MonoBehaviour
{   // This script handles the enemy movement and behaviour in the game.

    private Rigidbody2D m_enemyRb;

    public GameObject FrontCheckerObject;
    private GroundAndEnemyTriggerChecker m_frontChecker;

    public GameObject BelowCheckerObject;
    private GroundTriggerChecker m_belowChecker;

    SpriteRenderer m_spriteRenderer;

    public bool Moving = true; // Tickbox for if the enemy is moving or not
    public float EnemySpeed = 2.0f; // Speed of the enemy movement

    private bool m_xSignPositive = true; // boolean for sprite flipping and checking pos of triggers



    private void Awake()
    {
        m_enemyRb = GetComponent<Rigidbody2D>();
        if (m_enemyRb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject.");
            return;
        }

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        if (m_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
            return;
        }
    }

    private void Start()
    {
        m_frontChecker = FrontCheckerObject.GetComponent<GroundAndEnemyTriggerChecker>();
        if (m_frontChecker == null)
        {
            Debug.LogError("GroundTriggerChecker component not found on WallCheckerObject.");
            return;
        }

        m_belowChecker = BelowCheckerObject.GetComponent<GroundTriggerChecker>();
        if (m_belowChecker == null)
        {
            Debug.LogError("GroundTriggerChecker component not found on GroundCheckerObject.");
            return;
        }

        m_xSignPositive = !m_spriteRenderer.flipX;

        TriggerPos(); // Helper function to make sure the trigger objects are in the correct position
    }

    private void Update()
    {
        if (!Moving)
        {
            return; // Check if the enemy is allowed to move
        }

        if (
            m_enemyRb == null || m_belowChecker == null ||
            m_belowChecker == null || m_spriteRenderer == null
            )
        {
            return; // Exit if the rigidbody, sprite renderer, wall or ground checker is not set up
        }

        if (!m_frontChecker.IsTouchingGround() && m_belowChecker.IsTouchingGround())
        {   // Check if the enemy can move forward
            m_enemyRb.velocity = new Vector2((m_xSignPositive ? 1.0f : -1.0f) * EnemySpeed, m_enemyRb.velocity.y);
        }
        else
        {   // Make enemy turn around (change position of triggers in front of enemy)
            m_enemyRb.velocity = new Vector2(0.0f, m_enemyRb.velocity.y);
            m_xSignPositive = !m_xSignPositive;

            TriggerPos();

            m_spriteRenderer.flipX = !m_xSignPositive; // Flip the sprite to face the new direction
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   // This function handles the collision events with the player or skateboard
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Skateboard"))
        {
            GameStateHandler.Instance.LoseLevel("CollisionEnemy");
        }

        if (collision.gameObject.CompareTag("Wheel"))
        {
            Destroy(gameObject); // Destroy the enemy when hit by the skateboard wheel
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            // Debug.Log("Enemy hit a spike");
            Destroy(gameObject);
        }
    }



    private void TriggerPos() 
    {   // Helper function to set the position of the trigger objects based if sprite is flipped or not
        if (m_xSignPositive)
        {
            m_frontChecker.transform.localPosition = new Vector3(0.75f, 0, 0);
            m_belowChecker.transform.localPosition = new Vector3(0.75f, -0.75f, 0);
        }
        else
        {
            m_frontChecker.transform.localPosition = new Vector3(-0.75f, 0, 0);
            m_belowChecker.transform.localPosition = new Vector3(-0.75f, -0.75f, 0);
        }
    }
}
