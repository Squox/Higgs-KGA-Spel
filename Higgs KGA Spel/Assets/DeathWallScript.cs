using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWallScript : MonoBehaviour
{

    bool isDead = false;

    [SerializeField] private float ReloadTime = 10f;
    private float Reload;

    // Use this for initialization
    void Start ()
    {
        Reload = ReloadTime + Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Reload < Time.time && isDead)
        {
            restartCurrentScene();
            isDead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other);
        Reload = ReloadTime + Time.time;
        isDead = true;
    }

public void restartCurrentScene()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
