using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] private GameObject lvl2Music;

    private GameObject player;
    private GameObject level;
    private GameObject bossRat;
    private GameObject deathScreen;
    private GameObject pauseScreen;
    private GameObject victoryScreen;
    private Canvas canvas;
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;

    private Audiomanager audiomanagerScript;
    private PlayerActions playerActionsScript;
    private PlayerInput playerInputScript;
    private BossRatScript bossRatScript;

    private float fadeTimer = 0f;
    private float alphaLevel = 0f;
    private float deathScreenFadeTime = 60f;
    private float victoryScreenFadeTime = 40f;
    private float pauseScreenFadeTime = 10f;

    public int LastLevel;

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
        audiomanagerScript = FindObjectOfType<Audiomanager>();
        level = GameObject.FindGameObjectWithTag("Level");
        bossRat = GameObject.FindGameObjectWithTag("Rat");
        bossRatScript = bossRat.GetComponent<BossRatScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionsScript = player.GetComponent<PlayerActions>();
        playerInputScript = player.GetComponent<PlayerInput>();

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
        level = GameObject.FindGameObjectWithTag("Level");

        if (SceneManager.GetActiveScene().name == "First level")
        {
            LastLevel = 1;
            audiomanagerScript.GetComponent<AudioSource>().clip = level.GetComponent<AudioSource>().clip;
        }
        else if (SceneManager.GetActiveScene().name == "Second level")
        {
            LastLevel = 2;
            audiomanagerScript.GetComponent<AudioSource>().clip = level.GetComponent<AudioSource>().clip;
        }
        else if (SceneManager.GetActiveScene().name == "Third level")
        {
            LastLevel = 3;
        }
        else if (SceneManager.GetActiveScene().name == "Fourth level")
        {
            LastLevel = 4;
        }

        if (IsDead)
        {
            FadeIn(deathScreen.GetComponent<SpriteRenderer>(), deathScreenFadeTime);

            if (fadeTimer > deathScreenFadeTime)
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
        }

        if (Paused)
        {
            FadeIn(pauseScreen.GetComponent<SpriteRenderer>(), pauseScreenFadeTime);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }

        if (!Paused && !IsDead && bossRatScript.Health > 0 && fadeTimer > -1)
        {
            FadeOut(pauseScreen.GetComponent<SpriteRenderer>(), pauseScreenFadeTime);
        }

        if (playerActionsScript.HasBeenHit)
        {
            if (playerActionsScript.Health == 2)
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

        if (playerActionsScript.Exit)
        {
            WinLevel();
        }     
    }

    public void KillPlayer()
    {
        player.SetActive(false);
        IsDead = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        player.SetActive(false);
        Paused = true;
    }

    public void ResumeGame()
    {
        player.SetActive(true);
        Paused = false;
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("Selection menue");
    }

    public void WinLevel()
    {
        FadeIn(victoryScreen.GetComponent<SpriteRenderer>(), victoryScreenFadeTime);

        player.SetActive(false);

        if (fadeTimer > victoryScreenFadeTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitLevel();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
    }

    public void ManageShots()
    {
        if (playerActionsScript.ShotCount == 1)
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
        else if (playerActionsScript.ShotCount == 0 && playerInputScript.ChargeTimer < 1)
        {
            shot1.SetActive(true);
            shot2.SetActive(true);
            shot3.SetActive(true);
        }
    }

    public void FadeIn(SpriteRenderer spriteRenderer, float fadeTime)
    {
        if(fadeTimer < 1)
        {
            spriteRenderer.enabled = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }

        fadeTimer++;

        if (fadeTimer < fadeTime + 1)
        {
            alphaLevel = fadeTimer / fadeTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);
        }
    }

    public void FadeOut(SpriteRenderer spriteRenderer, float fadeTime)
    {
        if (fadeTimer < 1)
        {
            spriteRenderer.enabled = false;
        }

        fadeTimer--;

        if (fadeTimer > 0)
        {
            if (fadeTimer > fadeTime + 1)
            {
                fadeTimer = fadeTime;
            }

            alphaLevel = fadeTimer / fadeTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, alphaLevel);
        }
    }
}