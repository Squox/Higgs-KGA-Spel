using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Script : MonoBehaviour
{
    private Audiomanager audiomanagerScript;
    private Gamemanager gamemanagerScript;
    private GameObject player;    
    private Transform playerTF;

    private float checkRange = 1f;

    private bool lastLife;
    private bool rightCombibation = true;
    private bool addedPress1 = false;
    private bool addedPress2 = false;
    private bool addedPress3 = false;

    private int pyramidPressurePlatePresses = 0;

    [SerializeField] private GameObject pyramidDoor;

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
        playerTF = player.GetComponent<Transform>();
        audiomanagerScript = FindObjectOfType<Audiomanager>();
        gamemanagerScript = FindObjectOfType<Gamemanager>();
        audiomanagerScript.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;

        if (gamemanagerScript.LastCheckpointPosition == new Vector3(0,0,0))
        {
            gamemanagerScript.LastCheckpointPosition = checkPoint0.transform.position;
            gamemanagerScript.CheckPointCounter = 0;
            gamemanagerScript.DeathCounter = 0;
        }
        else if (gamemanagerScript.LastCheckpointPosition == checkPoint0.transform.position)
        {
            gamemanagerScript.DeathCounter = 0;
        }
        else if (gamemanagerScript.DeathCounter > 3)
        {
            playerTF.position = checkPoint0.transform.position;
            gamemanagerScript.DeathCounter = 0;
            gamemanagerScript.CheckPointCounter = 0;
        }
        else
        {
            playerTF.position = gamemanagerScript.LastCheckpointPosition;
        }       
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(pyramidPressurePlatePresses);

        if (playerTF != null && checkPoint0 != null && checkPoint1 != null && checkPoint2 != null)
        {
            if (isInRange(playerTF, checkPoint1.transform, checkRange) && gamemanagerScript.CheckPointCounter < 1)
            {
                gamemanagerScript.LastCheckpointPosition = checkPoint1.transform.position;
                checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
                gamemanagerScript.CheckPointCounter = 1;
                gamemanagerScript.DeathCounter = 0;
            }
            else if (isInRange(playerTF, checkPoint2.transform, checkRange) && gamemanagerScript.CheckPointCounter < 2)
            {
                gamemanagerScript.LastCheckpointPosition = checkPoint2.transform.position;
                checkPoint2.GetComponent<SpriteRenderer>().color = Color.green;
                gamemanagerScript.CheckPointCounter = 2;
                gamemanagerScript.DeathCounter = 0;
            }
        }    
        
        if (pyramidPressurePlatePresses > 0)
        {
            checkPyramidPressurePlates(pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed, pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed, pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed);
        }

        countPyramidPressurePlatePresses();
        colourCheckpoints();
    }

    private void colourCheckpoints()
    {
        if(gamemanagerScript.CheckPointCounter == 1 && gamemanagerScript.DeathCounter < 3)
        {
            checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (gamemanagerScript.CheckPointCounter == 2 && gamemanagerScript.DeathCounter < 3)
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
                        openPyramid();
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

    private void openPyramid()
    {
        Destroy(pyramidDoor);
    }

    private void deactivatePyramidPressurePlates()
    {
        pyramidPressurePlate1.GetComponent<PressurePlateScript>().Active = false;
        pyramidPressurePlate2.GetComponent<PressurePlateScript>().Active = false;
        pyramidPressurePlate3.GetComponent<PressurePlateScript>().Active = false;       
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
}
