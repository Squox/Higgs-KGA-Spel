﻿using System.Collections;
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

    public IEnumerator showEvent(CinemachineVirtualCamera CmVCam, float showTime)
    {
        int CmVCamPriority = 0;

        CmVCamPriority = CmVCam.GetComponent<CinemachineVirtualCamera>().Priority;  
        CmVCam.GetComponent<CinemachineVirtualCamera>().Priority = standardCam.GetComponent<CinemachineVirtualCamera>().Priority + 1;

        yield return new WaitForSeconds(showTime);

        CmVCam.GetComponent<CinemachineVirtualCamera>().Priority = CmVCamPriority;
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
