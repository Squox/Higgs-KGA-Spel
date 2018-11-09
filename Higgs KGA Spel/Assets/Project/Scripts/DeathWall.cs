using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : MonoBehaviour
{
    private PolygonCollider2D PlayerBC;
    private Rigidbody2D PlayerRB;
    private GameObject Player;
    private GameObject camera;
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
        camera = GameObject.FindGameObjectWithTag("Camera");

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
            Vector3 DeathScreenPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 10);
            Instantiate(DeathScreenprefab, DeathScreenPos, transform.rotation);

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
        Vector3 DeathScreenPos = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 10);
        PlayerInputScript.enabled = false;
        PlayerInputScript.MoveDirection = 0;
        PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        isDead = true;
        
    }

    public void restartCurrentScene()
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
