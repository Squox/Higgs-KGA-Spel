using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private int showTimeCounter;

    [SerializeField] private CinemachineVirtualCamera standardCam;

    // Use this for initialization
    void Start ()
    {
        
    }

    private void Awake()
    {
        MakeSingelton();
    }

    // Update is called once per frame
    void Update ()
    {
        standardCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void showEvent(CinemachineVirtualCamera CmVCam, float showTime)
    {
        int CmVCamPriority = 0;

        if (CmVCam.GetComponent<CinemachineVirtualCamera>().Priority < standardCam.GetComponent<CinemachineVirtualCamera>().Priority)
        {
            CmVCamPriority = CmVCam.GetComponent<CinemachineVirtualCamera>().Priority;
        }       

        CmVCam.GetComponent<CinemachineVirtualCamera>().Priority = standardCam.GetComponent<CinemachineVirtualCamera>().Priority + 1;

        showTimeCounter++;

        if (showTimeCounter > showTime)
        {
            CmVCam.GetComponent<CinemachineVirtualCamera>().Priority = CmVCamPriority;
            showTimeCounter = 0;
        }
    }

    private void MakeSingelton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
