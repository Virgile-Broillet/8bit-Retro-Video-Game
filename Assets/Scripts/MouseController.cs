using System.Collections;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float fleeSpeed = 6f;
    public float detectionDistance = 3f;

    public LayerMask obstacleLayer;

    private Transform player;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;

    private Vector2 lastPosition;
    private float stuckTimer;

    private bool isFleeing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // même logique que ton chat
        if (CharacterSelection.selectedCharacter == 0)
            player = GameObject.Find("Player").transform;
        else
            player = GameObject.Find("PlayerW").transform;

        StartCoroutine(RandomMovement());
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        HandleBehavior();

        animator.SetFloat("moveX", rb.linearVelocity.x);
        animator.SetFloat("moveY", rb.linearVelocity.y);

        if (rb.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rb.linearVelocity.x > 0)
            spriteRenderer.flipX = false;

        AvoidObstacle();
        CheckIfStuck();
    }

    void HandleBehavior()
    {
        if (player == null)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // 🐭 FUITE
        if (distance < detectionDistance)
        {
            isFleeing = true;

            Vector2 fleeDir = (transform.position - player.position).normalized;
            rb.linearVelocity = fleeDir * fleeSpeed;
        }
        else
        {
            isFleeing = false;
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            // Si elle fuit, on ne change pas la direction
            if (!isFleeing)
            {
                moveDirection = Random.insideUnitCircle.normalized;
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            if (!isFleeing)
            {
                moveDirection = Vector2.zero;
                yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
            }
        }
    }

    void AvoidObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 0.5f, obstacleLayer);

        if (hit.collider != null)
            moveDirection = Random.insideUnitCircle.normalized;
    }

    void CheckIfStuck()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);

        if (distanceMoved < 0.001f && moveDirection != Vector2.zero)
        {
            stuckTimer += Time.fixedDeltaTime;

            if (stuckTimer > 0.3f)
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