using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public GameObject LoadingScreen;
    public Slider Slider;
    public Text ProgressText;

    private Audiomanager audiomanagerScript;
    private UIManager uiManagerScript;

    public Vector3 LastCheckpointPosition;
    public Vector3 PlayerPosition;

    public int LastLevel = 0;
    public int HighestLevel = 0;
    public int CheckPointCounter = 0;
    public int DeathCounter = 0;
    public int PlayerHealth = 3;
    public int PlayerMaxHealth = 3;

    public bool InLevel = false;
    public bool PlayerDead = false;

    private void MakeSingelton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        MakeSingelton();

        audiomanagerScript = FindObjectOfType<Audiomanager>();
        uiManagerScript = FindObjectOfType<UIManager>();

        LastCheckpointPosition = new Vector3(0, 0, 0);

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (LastLevel > HighestLevel)
        {
            HighestLevel = LastLevel;
        }

        if (PlayerHealth < 1)
        {
            PlayerDead = true;
        }

        checkLevel();
    }

    private void checkLevel()
    {
        if (SceneManager.GetActiveScene().name == "First level")
        {
            InLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Second level")
        {
            InLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Third level")
        {
            InLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Fourth level")
        {
            InLevel = true;
        }
        else
        {
            InLevel = false;
        }
    }

    public void SavePlayer(PlayerActions player, Gamemanager gamemanager)
    {
        SaveSystem.SavePlayer(player, gamemanager);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        LastLevel = data.CurrentLevel;
        HighestLevel = data.HighestLevel;
        PlayerHealth = data.Health;
        PlayerPosition = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
    }

    public void RestartGame()
    {
        StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().name, LoadingScreen, Slider, ProgressText));
    }

    public void ExitLevel()
    {
        StartCoroutine(LoadAsyncronously("Selection menue", LoadingScreen, Slider, ProgressText));
    }

    public IEnumerator LoadAsyncronously(string level, GameObject loadingScreen, Slider slider, Text progressText)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(level);

        loadingScreen.SetActive(true);

        while (!load.isDone)
        {
            float progress = Mathf.Clamp01(load.progress / 0.9f);

            slider.value = progress;
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }
    }
}