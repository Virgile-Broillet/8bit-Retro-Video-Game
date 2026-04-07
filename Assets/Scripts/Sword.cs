using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private PlayerControls playerControls;
    private Animator myAnimator;

    private PlayerController playerController;

    [Header("Hitboxes")]
    [SerializeField] private SwordHitbox hitboxRight;
    [SerializeField] private SwordHitbox hitboxLeft;

    [SerializeField] private int damage = 1;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();

        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Combat.Attack.started += OnAttack;
    }

    private void OnDisable()
    {
        playerControls.Combat.Attack.started -= OnAttack;
        playerControls.Disable();
    }

    private void Update()
    {
        UpdateHitboxes();
    }

    private void UpdateHitboxes()
    {
        if (playerController == null || hitboxRight == null || hitboxLeft == null)
            return;

        bool isRight = !playerController.GetComponent<SpriteRenderer>().flipX;

        hitboxRight.gameObject.SetActive(isRight);
        hitboxLeft.gameObject.SetActive(!isRight);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        bool isRight = true;

        if (playerController != null)
        {
            SpriteRenderer playerSprite = playerController.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
                isRight = !playerSprite.flipX;
        }

        // 🔥 animation
        myAnimator.SetBool("IsRight", isRight);
        myAnimator.SetTrigger("Attack");

        // 🔥 dégâts
        if (isRight && hitboxRight != null)
        {
            hitboxRight.DealDamage(damage);
        }
        else if (!isRight && hitboxLeft != null)
        {
            hitboxLeft.DealDamage(damage);
        }
    }
}