using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerMoney = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI moneyText;

    void Awake()
    {
        int numbersOfGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        if (numbersOfGameSessions > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = "Lives: " + playerLives.ToString();
        moneyText.text = "Money: " + playerMoney.ToString();
    }


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToMoney(int moneyToAdd)
    {
        playerMoney = playerMoney + moneyToAdd;
        moneyText.text = "Money: " + playerMoney.ToString();
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);  //жизни кончались идем на 1 уровень
        Destroy(gameObject); //удаляем данные в этом GameSession иначе 0 hp перейдет в первый уровень
    }

    void TakeLife()
    {
        //playerLives = playerLives - 1;
        //playerLives -= 1;
        playerLives--;  //декремент
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  //на каком мы уровне
        SceneManager.LoadScene(currentSceneIndex);  //перезапускаем этот самый уровень
        livesText.text = "Lives: " + playerLives.ToString();

    }
}
