//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public SO_DayNightCycle dayNightCycle_Settings;
    private float timeCurrent = 12f;
    private float timeSpeed;
    private Gradient lightIntensity;
    private Gradient lightSkyfog;
    private Gradient lightAmbience;
    private Light directionalLight;
    private SkyboxBlender blendScript;

    private void Start()
    {
        // make a safe copy of skybox/material to avoid merge conflicts
        Material safecopy = new Material(RenderSettings.skybox);
        RenderSettings.skybox = Instantiate(safecopy);
        blendScript = GetComponent<SkyboxBlender>();
        if (blendScript)
        {
            RenderSettings.skybox = blendScript.blendedSkybox;
            if (directionalLight)
                RenderSettings.sun = directionalLight;
        }
        //FindObjectOfType<SoundManager>().StopPlaying("MenuMusic");
        //FindObjectOfType<SoundManager>().Play("GameMusic");
        // SoundManager.instance.StopPlaying("MenuMusic");
        // SoundManager.instance.Play("GameMusic");
    }

    void Awake()
    {
        directionalLight = this.transform.GetComponent<Light>();
    }

    void Update()
    {
        if (dayNightCycle_Settings == null)
        {
            Debug.LogWarning("DayNightCycle has no Settings attached!");
            return;
        }
        UpdateSettings();
        UpdateLighting();
    }

    private void UpdateSettings()
    {
        timeSpeed = dayNightCycle_Settings.timeSpeed;
        lightIntensity = dayNightCycle_Settings.lightIntensity;
        lightSkyfog = dayNightCycle_Settings.lightSkyfog;
        lightAmbience = dayNightCycle_Settings.lightAmbience;
    }

    void UpdateLighting()
    {
        timeCurrent += Time.deltaTime * timeSpeed;
        timeCurrent %= 24;
        float timeFloat = timeCurrent / 24f;
        RenderSettings.fogColor = lightSkyfog.Evaluate(timeFloat);
        RenderSettings.ambientLight = lightAmbience.Evaluate(timeFloat);
        RenderSettings.skybox.SetColor("_Tint", lightSkyfog.Evaluate(timeFloat));
        //RenderSettings.skybox.SetFloat("_Rotation", timeFloat * 360f);
        if (blendScript)
        {
            blendScript.rotation = timeFloat * 360f;
            if (timeFloat < 0.5f)
            {
                // remap (0 - 0.5) to (0 - 1) 
                blendScript.blend = timeFloat * 2;
            }
            else
            {
                // remap (0.5 - 1) to (1 - 0) 
                blendScript.blend = 1f - ((timeFloat * 2)-1);
            }
        }
        if (directionalLight)
        {
            transform.localRotation = Quaternion.Euler(new Vector3((timeFloat * 360f) -90f,270f,0f));
            directionalLight.color = lightIntensity.Evaluate(timeFloat);
        }
    }
}
