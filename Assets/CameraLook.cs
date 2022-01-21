using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLook : MonoBehaviour
{

    public GameObject camObj;
    public CinemachineVirtualCamera freeLook;
    public CinemachineComposer comp;

    private void Start()
    {

        camObj = GameObject.FindWithTag("MainCamera");
        freeLook = GetComponent<CinemachineVirtualCamera>();
        comp = freeLook.GetCinemachineComponent<CinemachineComposer>();
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            comp.m_TrackedObjectOffset.x = 7 ;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            comp.m_TrackedObjectOffset.x = -7;
        }
        else
        {
            comp.m_TrackedObjectOffset.x = 0;
        }

        
    }

}
