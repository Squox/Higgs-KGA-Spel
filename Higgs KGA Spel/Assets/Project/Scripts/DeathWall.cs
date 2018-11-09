using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{
    private PolygonCollider2D playerBC;
    private Rigidbody2D playerRB;
    private GameObject player;
    private GameObject camera;
    private PlayerInput playerInputScript;
    private PlayerMovement playerMovementScript;


    public bool DeathScreenInstantiated = false;


    public bool IsDead = false;


    [SerializeField] private GameObject deathScreenprefab;
    [SerializeField] private float reloadTime = 10f;

    private float reload;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("Camera");

        playerBC = player.GetComponent<PolygonCollider2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerInputScript = player.GetComponent<PlayerInput>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (IsDead)

        {
            Vector3 DeathScreenPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 10);
            Instantiate(deathScreenprefab, DeathScreenPos, transform.rotation);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                IsDead = false;
                playerInputScript.enabled = true;
                restartCurrentScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D PlayerBC)
    {        

        
        playerInputScript.enabled = false;
        playerInputScript.MoveDirection = 0;
        playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        IsDead = true;
        

    }

    public void restartCurrentScene()
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
