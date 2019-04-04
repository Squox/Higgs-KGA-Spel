using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public static GameObject LoadingScreen;
    public static Slider Slider;
    public static Text ProgressText;

    public static Vector3 LastCheckpointPosition;
    public static Vector3 SavedPlayerPosition;

    public static int LastLevel;
    public static int HighestLevel;
    public static int PlayerHealth;
    public static int CheckPointCounter = 0;
    public static int DeathCounter = 0;    
    public static int PlayerMaxHealth = 3;

    public static bool InLevel = false;
    public static bool PlayerDead = false;

    private void MakeSingelton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        LoadPlayer();
        MakeSingelton();
        Time.timeScale = 1;
    }

    private void Update()
    {
        Debug.Log("From Gamemanager perspective HightsLevel == " + HighestLevel);
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

    public static object LevelStringIntConversion(object level)
    {
        if (level.GetType() == typeof(int))
        {
            if ((int)level == 1)
                return "First Level";
            else if ((int)level == 2)
                return "Second Level";
            else if ((int)level == 3)
                return "Third Level";
            else if ((int)level == 4)
                return "Fourth Level";
            else
                return null;
        }
        else if (level.GetType() == typeof(string))
        {
            if ((string)level == "First Level")
                return 1;
            else if ((string)level == "Second Level")
                return 2;
            else if ((string)level == "Third Level")
                return 3;
            else if ((string)level == "Fourth Level")
                return 4;
            else
                return null;
        }
        else
            return null;
    }

    public static void SavePlayer(PlayerController player = null, int level = 0)
    {
        SaveSystem.SavePlayer(player);
    }

    public static void LoadPlayer(int level = 0)
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data == null)
            return;

        Debug.Log("Loading in Highets level as: " + data.HighestLevel);

        LastLevel = data.CurrentLevel;
        HighestLevel = data.HighestLevel;
        PlayerHealth = data.Health;
        SavedPlayerPosition = new Vector3(data.Position[level,0], data.Position[level,1], data.Position[level,2]);
    }

    public static void RestartGame()
    {
        Instance.StartCoroutine(LoadAsyncronously(SceneManager.GetActiveScene().name, LoadingScreen, Slider, ProgressText));
    }

    public static void ExitLevel()
    {
        Instance.StartCoroutine(LoadAsyncronously("Selection menue", LoadingScreen, Slider, ProgressText));
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