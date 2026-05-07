using UnityEngine;
using UnityEngine.InputSystem;


public class TestHorse : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1.5f;  //переменная, хранящая силу, что мы прилагаем	
    Rigidbody2D rb2d;

    Vector2 moveInput;

    [SerializeField] float boostSpeed = 25f;  //скорость ускорения
    [SerializeField] float baseSpeed = 15f;  //стандартная скорость


    SurfaceEffector2D surfaceEffector2DVar;

    [SerializeField] GameObject realPlayer;  //игрок
    [SerializeField] GameObject playerCamera;  //камера игрока
    [SerializeField] GameObject horseCamera;  //камера лошади
    [SerializeField] SurfaceEffector2D surfaceEffector2D;  //сарфейс эффектор и контроль его
    bool isActivePlayer = true;

    void Start()
    {
 
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2DVar = FindFirstObjectByType<SurfaceEffector2D>();  
        //horseCamera.SetActive(false); // отключаем камеру лошади
        //surfaceEffector2D.enabled = false; // отключаем сарфейс эффектор
        //isActivePlayer = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == realPlayer)
        {
            isActivePlayer = true;
            realPlayer.SetActive(false); // выключаем игрока
            playerCamera.SetActive(false); // выключаем камеру игрока
            horseCamera.SetActive(true); // включаем камеру лошади
            surfaceEffector2D.enabled = true; // включаем сарфейс эффектор
            GetComponent<Animator>().SetBool("isActive", true);  //включение анимации
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void Update()
    {
        RotatePlayer();
        ToBoost();
    }

    void RotatePlayer()
    {
        if (moveInput.x < 0 && isActivePlayer)   //если мы нажали на левую стрелочку
        {
            rb2d.AddTorque(torqueAmount);   // прибавили силу крутяки
        }

        else if (moveInput.x > 0 && isActivePlayer)  // именно else if чтобы игрок не мог нажимать 2 кнопки
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void ToBoost()
    {
        if (moveInput.y < 0 && isActivePlayer)
        {
            surfaceEffector2DVar.speed = baseSpeed;
        }
        else if (moveInput.y > 0 && isActivePlayer)
        {
            surfaceEffector2DVar.speed = boostSpeed;
        }
    }


}



