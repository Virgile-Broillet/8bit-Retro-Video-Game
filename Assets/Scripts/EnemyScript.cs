using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    [SerializeField] private Collider2D teleportCollider;
    [SerializeField] private PauseMenu pauseMenu;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (animator != null)
            animator.SetTrigger("Damage");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (teleportCollider != null)
            teleportCollider.isTrigger = true;

        if (pauseMenu != null)
            pauseMenu.SendMessage("PauseGame");

        Destroy(gameObject);
    }
}