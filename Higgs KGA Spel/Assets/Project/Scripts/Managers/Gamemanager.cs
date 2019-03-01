using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

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
            LastLevel = 1;
            InLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Second level")
        {
            LastLevel = 2;
            InLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Third level")
        {
            LastLevel = 3;
            InLevel = true;
        }
        else if (SceneManager.GetActiveScene().name == "Fourth level")
        {
            LastLevel = 4;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("Selection menue");
    }
}