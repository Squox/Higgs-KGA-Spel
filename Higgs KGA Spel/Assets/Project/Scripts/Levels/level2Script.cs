using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class level2Script : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    
    private float checkRange = 1f;
    private float musicVolume = 0.5f;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;
    [SerializeField] private Text progressText;
    
    [SerializeField] private AudioSource audioSource;   

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        LevelSetup.SetPlayerValues(checkPoints[0]);
        LevelSetup.LoadPlayer(2, checkPoints);
        LevelSetup.SetUpLevel(2, loadingScreen, slider, progressText, audioSource, musicVolume);                 
    }

	void Update ()
    {       
        checkPyramidPressurePlates();
        countPyramidPressurePlatePresses();
        colourCheckpoints();
        checkIfPlayerCanTakeCheckPoint();
    }

    //------------------------------------------------>>
    #region CheckPoint system

    [SerializeField] private GameObject[] checkPoints;

    private void checkIfPlayerCanTakeCheckPoint()
    {
        if (checkPoints != null)
        {
            if (Utility.IsInRange(PlayerPhysics.PlayerTransform, checkPoints[1].transform, checkRange) && Gamemanager.CheckPointCounter < 1)
            {
                takeCheckpoint(checkPoints[1], 1);
            }
            else if (Utility.IsInRange(PlayerPhysics.PlayerTransform, checkPoints[2].transform, checkRange) && Gamemanager.CheckPointCounter < 2)
            {
                takeCheckpoint(checkPoints[2], 2);
            }
        }
    }

    private void takeCheckpoint(GameObject cp, int cpIndex)
    {
        Gamemanager.LastCheckpointPosition = cp.transform.position;
        cp.GetComponent<SpriteRenderer>().color = Color.green;
        Gamemanager.CheckPointCounter = cpIndex;
        Gamemanager.DeathCounter = 0;
        Gamemanager.SavePlayer(playerController, Gamemanager.LastLevel);
    }

    private void colourCheckpoints()
    {
        for (int i = 0; i < Gamemanager.CheckPointCounter; i++)
        {
            //cp0 has no SpriteRenderer, so it's color must not be set: therefore the "i + 1".
            if (Gamemanager.DeathCounter < 3)           
                checkPoints[i + 1].GetComponent<SpriteRenderer>().color = Color.green;
            else
                checkPoints[i + 1].GetComponent<SpriteRenderer>().color = Color.red;

        }
    }

    #endregion
    //------------------------------------------------<<


    //------------------------------------------------>>
    #region PyramidDoorMechanism

    private bool addedPress1 = false;
    private bool addedPress2 = false;
    private bool addedPress3 = false;
    private bool rightCombibation = true;

    private int pyramidPressurePlatePresses = 0;
    private int doorShowTime = 5;
    private int cameraBlendTime = 2;

    [SerializeField] private CinemachineVirtualCamera pyramidDoorCam;
    [SerializeField] private GameObject pyramidDoor;

    [SerializeField] private GameObject[] pyramidPressurePlates;

    private void countPyramidPressurePlatePresses()
    {
        if (pyramidPressurePlatePresses < 3)
        {
            if (pyramidPressurePlates[0].GetComponent<PressurePlateScript>().Pressed && !addedPress1)
            {
                pyramidPressurePlatePresses++;
                addedPress1 = true;
            }
            else if (pyramidPressurePlates[1].GetComponent<PressurePlateScript>().Pressed && !addedPress2)
            {
                pyramidPressurePlatePresses++;
                addedPress2 = true;
            }
            else if (pyramidPressurePlates[2].GetComponent<PressurePlateScript>().Pressed && !addedPress3)
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

    private void checkPyramidPressurePlates()
    {       
        if (pyramidPressurePlates[2].GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlatePresses > 0)
        {
            if (pyramidPressurePlates[0].GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlatePresses > 1)
            {
                if (pyramidPressurePlates[1].GetComponent<PressurePlateScript>().Pressed && pyramidPressurePlatePresses > 2)
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
        else if (pyramidPressurePlatePresses > 0)
        {
            rightCombibation = false;
        }
    }

    private void deactivatePyramidPressurePlates()
    {
        foreach (GameObject go in pyramidPressurePlates)
        {
            go.GetComponent<PressurePlateScript>().Active = false;
        }
    }

    private IEnumerator showDoor()
    {
        StartCoroutine(CameraManager.showEvent(pyramidDoorCam, doorShowTime));

        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(doorShowTime / 2);

        Destroy(pyramidDoor);

        yield return new WaitForSeconds(doorShowTime / 2 + cameraBlendTime);

        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    #endregion
    //------------------------------------------------<<
}
