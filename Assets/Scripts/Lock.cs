using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Lock : MonoBehaviour
{   // This script is attached to the lock block (not the lock) to destroy the lock and itself when the player unlocks it

    public GameObject LockSprites;
    public AnimatorController SpriteAnimator;

    public void unlock()
    {
        StartCoroutine(DestroyLock());
    }

    private IEnumerator DestroyLock()
    {
        if (LockSprites != null && SpriteAnimator != null)
        {
            Animator animator = LockSprites.AddComponent<Animator>();
            animator.runtimeAnimatorController = SpriteAnimator;

            yield return new WaitForSeconds(1.0f);
            Destroy(LockSprites, 0.5f);
        }

        Destroy(gameObject);
    }
}
