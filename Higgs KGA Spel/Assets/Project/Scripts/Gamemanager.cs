using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    

    private GameObject player;
    private GameObject bossRat;
    private GameObject deathScreen;
    private GameObject pauseScreen;
    private GameObject victoryScreen;
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;
    private GameObject shot1;
    private GameObject shot2;
    private GameObject shot3;

    private PlayerActions playerActionsScript;
    private BossRatScript bossRatScript;

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
        bossRat = GameObject.FindGameObjectWithTag("Rat");
        bossRatScript = bossRat.GetComponent<BossRatScript>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();

        life1 = GameObject.FindGameObjectWithTag("Life 1");
        life2 = GameObject.FindGameObjectWithTag("Life 2");
        life3 = GameObject.FindGameObjectWithTag("Life 3");
        shot1 = GameObject.FindGameObjectWithTag("Shot 1");
        shot2 = GameObject.FindGameObjectWithTag("Shot 2");
        shot3 = GameObject.FindGameObjectWithTag("Shot 3");

        deathScreen = GameObject.FindGameObjectWithTag("DeathScreen");
        pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
        victoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen");

        Time.timeScale = 1;

        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        shot1.SetActive(true);
        shot2.SetActive(true);
        shot3.SetActive(true);

        victoryScreen.GetComponent<SpriteRenderer>().enabled = false;
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

        if (playerActionsScript.HasBeenHit)
        {
            if(playerActionsScript.Health == 2)
            {
                life1.SetActive(false);
            }
            else if (playerActionsScript.Health == 1)
            {
                life2.SetActive(false);
            }
            else if (playerActionsScript.Health == 0)
            {
                life3.SetActive(false);
            }
        }

        if(playerActionsScript.ShotCount == 1)
        {
            shot3.SetActive(false);
            shot2.SetActive(true);
            shot1.SetActive(true);
        }
        else if (playerActionsScript.ShotCount == 2)
        {
            shot3.SetActive(false);
            shot2.SetActive(false);
            shot1.SetActive(true);
        }
        else if (playerActionsScript.ShotCount == 3)
        {
            shot3.SetActive(false);
            shot2.SetActive(false);
            shot1.SetActive(false);
        }
        else
        {
            shot1.SetActive(true);
            shot2.SetActive(true);
            shot3.SetActive(true);
        }

        if(bossRatScript.Health < 1)
        {
            victoryScreen.GetComponent<SpriteRenderer>().enabled = true;
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