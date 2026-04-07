using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float followDistance = 0.25f;
    public float interactDistance = 2f;

    private Transform player;

    public LayerMask obstacleLayer;
    public GameObject interactIndicator;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;
    private bool isFollowing = false;

    private PlayerControls controls;

    private Vector2 lastPosition;
    private float stuckTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        controls = new PlayerControls();
        controls.Movement.Interact.performed += ctx => OnInteract();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        // Trouver automatiquement le bon joueur
        if (CharacterSelection.selectedCharacter == 0)
        {
            player = GameObject.Find("Player").transform;
        }
        else
        {
            player = GameObject.Find("PlayerW").transform;
        }

        StartCoroutine(RandomMovement());
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (isFollowing)
            FollowPlayer();
        else
            rb.linearVelocity = moveDirection * moveSpeed;

        animator.SetFloat("moveX", rb.linearVelocity.x);
        animator.SetFloat("moveY", rb.linearVelocity.y);

        if (rb.linearVelocity.x < 0)
            spriteRenderer.flipX = true;
        else if (rb.linearVelocity.x > 0)
            spriteRenderer.flipX = false;

        AvoidObstacle();
        CheckIfStuck();
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (interactIndicator != null)
            interactIndicator.SetActive(distance < interactDistance);
    }

    private void OnInteract()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < interactDistance)
        {
            isFollowing = !isFollowing;

            if (!isFollowing)
                StartCoroutine(RandomMovement());
        }
    }

    void FollowPlayer()
    {
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

    IEnumerator RandomMovement()
    {
        while (!isFollowing)
        {
            moveDirection = Random.insideUnitCircle.normalized;
            yield return new WaitForSeconds(Random.Range(2f, 4f));

            moveDirection = Vector2.zero;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    void AvoidObstacle()
    {
        if (isFollowing) return;

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