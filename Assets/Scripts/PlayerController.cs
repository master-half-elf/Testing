using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;  //переменная для хранения вектора движения
    Rigidbody2D playerRB;
    Animator animator;

    bool playerHasHorizontalSpeed;

    [SerializeField] float walkSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float climbSpeed = 5f;

    [Header("Стрельба из лука")]
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;


    CapsuleCollider2D playerBody;
    BoxCollider2D playerLegs;
    float playerGravityScale;  //храним гравитацию игрока

    [SerializeField] bool isJumping;
    bool isAlive = true;

    [SerializeField] Vector2 deathJump = new Vector2(5f, 5f);

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerBody = GetComponent<CapsuleCollider2D>();
        playerLegs = GetComponent<BoxCollider2D>();
        playerGravityScale = playerRB.gravityScale;
    }
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Walk();
        FlipSprite();
        //ClimpLadder();
        Jump();

        Die();
    }

    void Die()
    {
        if (playerBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            playerRB.linearVelocity = deathJump;

            //задержка Invoke
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (!playerLegs.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJumping", false);
            return;
        }

        if (value.isPressed)
        {
            playerRB.linearVelocity = playerRB.linearVelocity + new Vector2(0f, jumpSpeed);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        //Instantiate(what,where,rotation)
        //GameObject newArrow = Instantiate(arrow, bow.position, Quaternion.identity);
        //Rigidbody2D rbArrow = newArrow.GetComponent<Rigidbody2D>();
        //rbArrow.AddForce(new Vector2(-100, 0f), ForceMode2D.Impulse);
        Instantiate(arrow, bow.position, Quaternion.identity);
    }

    void Jump()
    {
        bool isOnGround = playerLegs.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isOnGround && playerRB.linearVelocity.y <= 0)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    /*void ClimpLadder()
    {
        if (!playerLegs.IsTouchingLayers(LayerMask.GetMask("Rope")))
        {
            playerRB.gravityScale = playerGravityScale;
            animator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(playerRB.linearVelocity.x, moveInput.y * climbSpeed);
        playerRB.linearVelocity = climbVelocity;
        playerRB.gravityScale = 0f;
        animator.SetBool("isClimbing", true);
    }*/

    void FlipSprite()
    {
        playerHasHorizontalSpeed = Mathf.Abs( playerRB.linearVelocity.x  ) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(playerRB.linearVelocity.x), 1f);
        }
    }
    void Walk()  //Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * walkSpeed, playerRB.linearVelocity.y); 
        playerRB.linearVelocity = playerVelocity;

        //bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.linearVelocity.x) > Mathf.Epsilon;
        animator.SetBool("isWalking", playerHasHorizontalSpeed && !isJumping);

    }

}
