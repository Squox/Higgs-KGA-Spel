using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWallScript : MonoBehaviour
{
    private PolygonCollider2D PlayerBC;
    private GameObject Player;

    public bool isDead = false;

    [SerializeField] private GameObject DeathScreenprefab;
    [SerializeField] private float ReloadTime = 10f;

    private float Reload;

    // Use this for initialization
    void Start ()
    {
        Reload = ReloadTime + Time.time;

        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerBC = Player.GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Reload < Time.time && isDead)
        {
            isDead = false;
            restartCurrentScene();        
        }
    }

    private void OnTriggerEnter2D(Collider2D PlayerBC)
    {
        Instantiate(DeathScreenprefab, transform.position, transform.rotation);
        if (isDead == false)
        {
            Reload = ReloadTime + Time.time;
        }
        isDead = true;
    }

public void restartCurrentScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
