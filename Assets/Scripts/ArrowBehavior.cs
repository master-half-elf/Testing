using Unity.VisualScripting;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 2f;
    Rigidbody2D rb;

    PlayerController playerController;
    float xSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindFirstObjectByType<PlayerController>();
        xSpeed = Mathf.Sign(playerController.transform.localScale.x) * arrowSpeed;
        

        transform.localScale = new Vector2(Mathf.Sign(playerController.transform.localScale.x), 1f);

        //В момент старта через толчок
        //float xTurnForce = playerController.transform.localScale.x * arrowSpeed * 100;
        //rb.AddForce(new Vector2(-xTurnForce, 0f), ForceMode2D.Impulse);
    }

    void Update()
    {       //В каждом кадре немного толкаем стрелу
        rb.linearVelocity = new Vector2(-xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }*/
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

}
