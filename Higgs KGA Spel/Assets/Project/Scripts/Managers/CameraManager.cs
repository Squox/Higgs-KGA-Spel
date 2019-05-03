using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private static int showTimeCounter;

    [SerializeField] private static CinemachineVirtualCamera standardCam;

    [SerializeField] private float defaultCamDistance = 3;

    private void Awake()
    {
        MakeSingelton();

        standardCam = FindObjectOfType<CinemachineVirtualCamera>();
        standardCam.m_Lens.OrthographicSize = defaultCamDistance;
    }

    // Update is called once per frame
    void Update ()
    {
        standardCam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public static IEnumerator showEvent(CinemachineVirtualCamera CmVCam, float showTime)
    {
        int CmVCamPriority = 0;

        CmVCamPriority = CmVCam.GetComponent<CinemachineVirtualCamera>().Priority;  
        CmVCam.GetComponent<CinemachineVirtualCamera>().Priority = standardCam.GetComponent<CinemachineVirtualCamera>().Priority + 1;

        yield return new WaitForSeconds(showTime);

        CmVCam.GetComponent<CinemachineVirtualCamera>().Priority = CmVCamPriority;
    }

    private void MakeSingelton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
