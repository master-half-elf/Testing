using UnityEngine;


public class EnemyController : MonoBehaviour
{
    Rigidbody2D lukRB;

    [SerializeField] float moveSpeed = 1.2f;

    bool isChangingDirection;

    void OnEnable()
    {
        // —брос флагов
        isChangingDirection = false;

        // ќстановка всех Invoke (очень важно)
        CancelInvoke();

        // —брос скорости (например всегда вправо)
        moveSpeed = Mathf.Abs(moveSpeed);

        // —брос физики
        if (lukRB == null)
            lukRB = GetComponent<Rigidbody2D>();

        lukRB.linearVelocity = Vector2.zero;

        // —брос поворота
        transform.localScale = new Vector2(1f, 1f);
    }

    void Start()
    {
        lukRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lukRB.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ( !isChangingDirection )
        {
            isChangingDirection = true;

            moveSpeed = -moveSpeed;
            EnemyFacing();

            Invoke("ResetDirectionChange", 0.12f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrows"))
        {
            gameObject.SetActive(false);
        }
    }

    void ResetDirectionChange()
    {
        isChangingDirection = false;
    }

    void EnemyFacing()
    {
        transform.localScale = new Vector2( -(Mathf.Sign(lukRB.linearVelocity.x)),  1f );
    }

}
