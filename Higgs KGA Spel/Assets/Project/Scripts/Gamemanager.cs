using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    private Audiomanager audiomanagerScript;
    private UIManager uiManagerScript;

    public int LastLevel = 0;

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

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "First level")
        {
            LastLevel = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Second level")
        {
            LastLevel = 2;
        }
        else if (SceneManager.GetActiveScene().name == "Third level")
        {
            LastLevel = 3;
        }
        else if (SceneManager.GetActiveScene().name == "Fourth level")
        {
            LastLevel = 4;
        }
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