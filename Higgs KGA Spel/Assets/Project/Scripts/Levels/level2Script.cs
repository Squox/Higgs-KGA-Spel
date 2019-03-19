using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class level2Script : MonoBehaviour
{
    private GameObject player;
    private PlayerActions playerActionScript;
    private Transform playerTF;
    
    private float checkRange = 1f;
    private float musicVolume = 0.5f;

    private bool lastLife;
    private bool rightCombibation = true;
    private bool addedPress1 = false;
    private bool addedPress2 = false;
    private bool addedPress3 = false;

    private int pyramidPressurePlatePresses = 0;
    private int doorShowTime = 5;
    private int cameraBlendTime = 2;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;

    [SerializeField] private CinemachineVirtualCamera pyramidDoorCam;

    [SerializeField] private GameObject pyramidDoor;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject checkPoint0;
    [SerializeField] private GameObject checkPoint1;
    [SerializeField] private GameObject checkPoint2;

    [SerializeField] private GameObject pyramidPressurePlate1;
    [SerializeField] private GameObject pyramidPressurePlate2;
    [SerializeField] private GameObject pyramidPressurePlate3;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerActionScript = player.GetComponent<PlayerActions>();
        playerTF = player.GetComponent<Transform>();

        Gamemanager.LoadPlayer();
        Gamemanager.LoadingScreen = loadingScreen;
        Gamemanager.Slider = slider;
        Gamemanager.ProgressText = progressText;
        Gamemanager.LastLevel = 2;      

        Audiomanager.PlayMusic(audioSource, musicVolume);

        if (Gamemanager.LastCheckpointPosition == checkPoint0.transform.position)
        {
            Gamemanager.DeathCounter = 0;
        }
        else if (Gamemanager.DeathCounter > 3)
        {
            Gamemanager.LastCheckpointPosition = checkPoint0.transform.position;
            Gamemanager.DeathCounter = 0;
            Gamemanager.CheckPointCounter = 0;
        }
  
        LoadPlayer();     
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerTF != null && checkPoint1 != null && checkPoint2 != null)
        {
            if (isInRange(playerTF, checkPoint1.transform, checkRange) && Gamemanager.CheckPointCounter < 1)
            {
                takeCheckpoint(checkPoint1, 1);
            }
            else if (isInRange(playerTF, checkPoint2.transform, checkRange) && Gamemanager.CheckPointCounter < 2)
            {
                takeCheckpoint(checkPoint2, 2);
            }
        }    
        
        if (pyramidPressurePlatePresses > 0)
        {
            checkPyramidPressurePlates(pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed, pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed, pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed);
        }

        countPyramidPressurePlatePresses();
        colourCheckpoints();
    }

    private void takeCheckpoint(GameObject cp, int cpIndex)
    {
        Gamemanager.LastCheckpointPosition = cp.transform.position;
        cp.GetComponent<SpriteRenderer>().color = Color.green;
        Gamemanager.CheckPointCounter = cpIndex;
        Gamemanager.DeathCounter = 0;
        Gamemanager.SavePlayer(playerActionScript);
    }

    private void colourCheckpoints()
    {
        if(Gamemanager.CheckPointCounter == 1 && Gamemanager.DeathCounter < 3)
        {
            checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (Gamemanager.CheckPointCounter == 2 && Gamemanager.DeathCounter < 3)
        {
            checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
            checkPoint2.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void countPyramidPressurePlatePresses()
    {
        if (pyramidPressurePlatePresses < 3)
        {
            if (pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && !addedPress1)
            {
                pyramidPressurePlatePresses++;
                addedPress1 = true;
            }
            else if (pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && !addedPress2)
            {
                pyramidPressurePlatePresses++;
                addedPress2 = true;
            }
            else if (pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed && !addedPress3)
            {
                pyramidPressurePlatePresses++;
                addedPress3 = true;
            }
        }
        else
        {
            pyramidPressurePlatePresses = 0;
        }
    }

    private void checkPyramidPressurePlates(bool pressed1, bool pressed2, bool pressed3)
    {       
        if (pressed3)
        {
            if (pressed1 && pyramidPressurePlatePresses > 1)
            {
                if (pressed2 && pyramidPressurePlatePresses > 2)
                {
                    if (rightCombibation)
                    {
                        StartCoroutine(showDoor());
                    }
                    else
                    {                      
                        rightCombibation = true;
                        addedPress1 = false;
                        addedPress2 = false;
                        addedPress3 = false;
                        deactivatePyramidPressurePlates();
                    }
                }
            }
            else if (pyramidPressurePlatePresses > 1)
            {
                rightCombibation = false;
            }
        }
        else
        {
            rightCombibation = false;
        }
    }

    private void deactivatePyramidPressurePlates()
    {
        pyramidPressurePlate1.GetComponent<PressurePlateScript>().Active = false;
        pyramidPressurePlate2.GetComponent<PressurePlateScript>().Active = false;
        pyramidPressurePlate3.GetComponent<PressurePlateScript>().Active = false;       
    }

    private IEnumerator showDoor()
    {
        StartCoroutine(CameraManager.showEvent(pyramidDoorCam, doorShowTime));

        player.GetComponent<PlayerActions>().enabled = false;
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(doorShowTime / 2);

        Destroy(pyramidDoor);

        yield return new WaitForSeconds(doorShowTime / 2 + cameraBlendTime);

        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<PlayerActions>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private bool isInRange(Transform transform1, Transform transform2, float range)
    {
        if (Mathf.Abs(transform1.position.x - transform2.position.x) < range && Mathf.Abs(transform1.position.y - transform2.position.y) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadPlayer()
    {
        if (Gamemanager.PlayerDead)
        {
            player.transform.position = Gamemanager.LastCheckpointPosition;
            Gamemanager.PlayerHealth = Gamemanager.PlayerMaxHealth;
            Gamemanager.SavePlayer(playerActionScript);
            Gamemanager.PlayerDead = false;
        }
        else if (Gamemanager.HighestLevel < 2)
        {
            player.transform.position = checkPoint0.transform.position;
            Gamemanager.PlayerHealth = Gamemanager.PlayerMaxHealth;
            Gamemanager.LastCheckpointPosition = checkPoint0.transform.position;
            Gamemanager.SavePlayer(playerActionScript);
            Gamemanager.HighestLevel = 2;
        }
        else
        {
            player.transform.position = Gamemanager.PlayerPosition;
            playerActionScript.Health = Gamemanager.PlayerHealth;
        }
    }
}
