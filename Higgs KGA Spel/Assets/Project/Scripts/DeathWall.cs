using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{
    private PolygonCollider2D PlayerBC;
    private Rigidbody2D PlayerRB;
    private GameObject Player;
    private PlayerInput PlayerInputScript;
    private PlayerMovement PlayerMovementScript;

    public bool isDead = false;

    [SerializeField] private GameObject DeathScreenprefab;
    [SerializeField] private float ReloadTime = 10f;

    private float Reload;

    // Use this for initialization
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        PlayerBC = Player.GetComponent<PolygonCollider2D>();
        PlayerRB = Player.GetComponent<Rigidbody2D>();
        PlayerInputScript = Player.GetComponent<PlayerInput>();
        PlayerMovementScript = Player.GetComponent<PlayerMovement>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isDead == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDead = false;
                PlayerInputScript.enabled = true;
                restartCurrentScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D PlayerBC)
    {
        PlayerInputScript.enabled = false;
        PlayerInputScript.MoveDirection = 0;      
        PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        Player.transform.position = new Vector2(25, 0);
        Instantiate(DeathScreenprefab, Player.transform.position, transform.rotation);
        isDead = true;
        
    }

    public void restartCurrentScene()
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
