using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [Header("Canvas")]
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject settingsCanvas;

    [Header("Sound")]
    [SerializeField] Slider volumeSlider;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Загружаем сохраненную громкость
        //float volume = PlayerPrefs.GetFloat("Volume", 1f);
        //AudioListener.volume = volume;

        if (volumeSlider != null)
        {
            //volumeSlider.value = volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        CalculateFlips();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        //PlayerPrefs.SetFloat("Volume", value);
    }


    public void QuitGame()
    {
        Debug.Log("Exit game");

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
    }
}


public class RaceManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;

    public void AddScore(int additionalScore)
    {
        score += additionalScore;
        scoreText.text = "Score: " + score;
    }
}