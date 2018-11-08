using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{
    private PolygonCollider2D PlayerBC;
    private GameObject Player;
    private PlayerInput PlayerInputScript;
    private PlayerMovement PlayerMovementScript;

    public bool isDead = false;

    Vector3 DeathScreenPos;

    [SerializeField] private GameObject DeathScreenprefab;
    [SerializeField] private float ReloadTime = 10f;

    private float Reload;

    // Use this for initialization
    void Start ()
    {
        Reload = ReloadTime + Time.time;

        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerBC = Player.GetComponent<PolygonCollider2D>();
        PlayerInputScript = Player.GetComponent<PlayerInput>();
        PlayerMovementScript = Player.GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Reload < Time.time && isDead)
        {
            isDead = false;
            PlayerInputScript.enabled = true;
            restartCurrentScene();        
        }
    }

    private void OnTriggerEnter2D(Collider2D PlayerBC)
    {

        //SceneManager.LoadScene("DeathScreen", LoadSceneMode.Additive);
        if (PlayerMovementScript.IsFacingRight)
        {
            DeathScreenPos = new Vector3(Player.transform.position.x -2, Player.transform.position.y, Player.transform.position.z - 3);
        }
        else
        {
            DeathScreenPos = new Vector3(Player.transform.position.x +2, Player.transform.position.y, Player.transform.position.z - 3);
        }

        Instantiate(DeathScreenprefab, DeathScreenPos, transform.rotation);

        PlayerInputScript.enabled = false;
        PlayerInputScript.MoveDirection = 0;

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
