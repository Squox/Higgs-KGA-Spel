using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    

    private GameObject player;
    private GameObject deathScreen;
    private GameObject pauseScreen;
    public bool IsDead = false;
    public bool Paused = false;

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
        player = GameObject.FindGameObjectWithTag("Player");
        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");

        Time.timeScale = 1;
    }


    private void Update()
    {
        if (IsDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Selection menue");
            }
        }

        if (Paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    public void KillPlayer()
    {
        player.SetActive(false);
        deathScreen.GetComponent<SpriteRenderer>().enabled = true;
        IsDead = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        player.SetActive(false);
        pauseScreen.GetComponent<SpriteRenderer>().enabled = true;
        Paused = true;
    }

    public void ResumeGame()
    {
        player.SetActive(true);
        pauseScreen.GetComponent<SpriteRenderer>().enabled = false;
        Paused = false;
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("Start menu");
    }
}