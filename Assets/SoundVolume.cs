using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    
    public static Toggle tog;
    public GameObject toggle;

    private void Update()
    {
        toggle = GameObject.FindGameObjectWithTag("GameSound");
        tog = toggle.GetComponent<Toggle>();
    }

}
