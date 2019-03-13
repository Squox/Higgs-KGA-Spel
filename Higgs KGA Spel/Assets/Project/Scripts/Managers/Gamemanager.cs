using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public static GameObject LoadingScreen;
    public static Slider Slider;
    public static Text ProgressText;

    public static Vector3 LastCheckpointPosition;
    public static Vector3 PlayerPosition;

    public static int LastLevel = 0;
    public static int HighestLevel = 0;
    public static int PlayerHealth = 3;
    public static int CheckPointCounter = 0;
    public static int DeathCounter = 0;
    
    public static int PlayerMaxHealth = 3;

    public static bool InLevel = false;
    public static bool PlayerDead = false;

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

        LastCheckpointPosition = new Vector3(0, 0, 0);

        Time.timeScale = 1;
    }

    private void Update()
    {
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

    public static void SavePlayer(PlayerActions player)
    {
        SaveSystem.SavePlayer(player);
    }

    public static void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        LastLevel = data.CurrentLevel;
        HighestLevel = data.HighestLevel;
        PlayerHealth = data.Health;
        PlayerPosition = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
    }

    public static void RestartGame()
    {
        instance.StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().name, LoadingScreen, Slider, ProgressText));
    }

    public static void ExitLevel()
    {
        instance.StartCoroutine(LoadAsyncronously("Selection menue", LoadingScreen, Slider, ProgressText));
    }

    public static IEnumerator LoadAsyncronously(string level, GameObject loadingScreen, Slider slider, Text progressText)
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