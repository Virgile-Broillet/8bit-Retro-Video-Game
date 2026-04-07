using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatControllerFollow : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float followDistance = 0.25f;

    private Transform player;

    public LayerMask obstacleLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;

    private PlayerControls controls;

    private Vector2 lastPosition;
    private float stuckTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        controls = new PlayerControls();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        // Trouver automatiquement le bon joueur
        if (CharacterSelection.selectedCharacter == 0)
            player = GameObject.Find("Player").transform;
        else
            player = GameObject.Find("PlayerW").transform;

        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        FollowPlayer();

        animator.SetFloat("moveX", rb.linearVelocity.x);
        animator.SetFloat("moveY", rb.linearVelocity.y);

        if (rb.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rb.linearVelocity.x > 0)
            spriteRenderer.flipX = false;

        CheckIfStuck();
    }

    void FollowPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            moveDirection = (player.position - transform.position).normalized;

            if (distance < 0.5f)
                rb.linearVelocity = moveDirection * (moveSpeed * 0.5f);
            else
                rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            moveDirection = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void CheckIfStuck()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);

        if (distanceMoved < 0.001f && moveDirection != Vector2.zero)
        {
            stuckTimer += Time.fixedDeltaTime;

            if (stuckTimer > 0.5f)
            {
                moveDirection = Random.insideUnitCircle.normalized;
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }

        lastPosition = transform.position;
    }
}