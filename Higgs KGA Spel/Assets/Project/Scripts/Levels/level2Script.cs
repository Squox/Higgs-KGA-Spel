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
        else
        {
            playerTF.position = gamemanagerScript.LastCheckpointPosition;
        }

        if (gamemanagerScript.DeathCounter > 2)
        {
            gamemanagerScript.LastCheckpointPosition = checkPoint0.transform.position;
            gamemanagerScript.CheckPointCounter = 0;
            lastLife = true;
        }
        else
        {
            lastLife = false;
        }

        checkTakenCheckpoints();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerTF != null && checkPoint0 != null && checkPoint1 != null && checkPoint2 != null)
        {
            if (isInRange(playerTF, checkPoint1.transform, checkRange) && gamemanagerScript.CheckPointCounter < 1 && gamemanagerScript.DeathCounter < 3 && !lastLife)
            {
                gamemanagerScript.LastCheckpointPosition = checkPoint1.transform.position;
                checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
                gamemanagerScript.CheckPointCounter = 1;
                gamemanagerScript.DeathCounter = 0;
            }
            else if (isInRange(playerTF, checkPoint2.transform, checkRange) && gamemanagerScript.CheckPointCounter < 2 && gamemanagerScript.DeathCounter < 3 && !lastLife)
            {
                gamemanagerScript.LastCheckpointPosition = checkPoint2.transform.position;
                checkPoint2.GetComponent<SpriteRenderer>().color = Color.green;
                gamemanagerScript.CheckPointCounter = 2;
                gamemanagerScript.DeathCounter = 0;
            }
        }    
        
        if (pressedPyramidPressurePlates() > 0)
        {
            checkPyramidPressurePlates(pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed, pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed, pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed);
        }
    }

    private void checkTakenCheckpoints()
    {
        if(gamemanagerScript.CheckPointCounter == 1)
        {
            checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (gamemanagerScript.CheckPointCounter == 2)
        {
            checkPoint1.GetComponent<SpriteRenderer>().color = Color.green;
            checkPoint2.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private int pressedPyramidPressurePlates()
    {
        if (pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && !pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && !pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 1;
        }
        else if (!pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && !pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 1;
        }
        else if (!pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && !pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 1;
        }
        else if (pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && !pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 2;
        }
        else if (pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && !pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 2;
        }
        else if (!pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 2;
        }
        else if (pyramidPressurePlate1.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate2.GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlate3.GetComponent<PressurePlateScript>().Pressed)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    private void checkPyramidPressurePlates(bool pressed1, bool pressed2, bool pressed3)
    {       
        if (pressed3)
        {
            if (pressed1 && pressedPyramidPressurePlates() > 1)
            {
                if (pressed2 && pressedPyramidPressurePlates() > 2)
                {
                    if (rightCombibation)
                    {
                        openPyramid();
                    }
                    else
                    {
                        deactivatePyramidPressurePlates();
                        rightCombibation = true;
                    }
                }
            }
            else if (pressedPyramidPressurePlates() > 1)
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
