using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    private GameObject player;
    private GameObject deathScreen;
    public bool IsDead = false;
    
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
    }


    private void Update()
    {
        if (IsDead && Input.GetKey(KeyCode.Space))
        {
            RestartGame();
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
}