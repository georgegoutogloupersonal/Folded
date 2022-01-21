using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;

    public Sound soundObject;

    public bool isTicked = false;


    private void Start()
    {
        
    }


    private void Update()
    {
        ManageSong();

    

    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
           
        }

     
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " Not Found");
            return;
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
 
    }

    

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }



        s.source.Stop();
    }

    public void VolumeUp(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " Not Found");
            return;
        }

        s.source.volume += 0.1f * Time.deltaTime;
        if(s.source.volume >= 0.5f)
        {
            s.source.volume = 0.5f;
        }
    }

    public void VolumeDown(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " Not Found");
            return;
        }

        s.source.volume -= 0.1f * Time.deltaTime;
        if (s.source.volume <= 0f)
        {
            s.source.volume = 0f;
        }
    }

    public void VolumeOff(string name)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " Not Found");
            return;
        }


        switch (sceneName)
        {
            case "SoundSettings":
                if (SoundVolume.tog.isOn)
                {                  
                    s.source.volume = 0f;
                }
                else
                {
                    FindObjectOfType<SoundManager>().VolumeUp("MainTrack");
                }
                break;
        }
       
        
    }



    public void ManageSong()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Explore_Mode":
                //FindObjectOfType<SoundManager>().StopPlaying("menuMusic");
                //FindObjectOfType<SoundManager>().Play("exploreMusic");
                if (GliderController.acceleration < 2f)
                {
                    FindObjectOfType<SoundManager>().Play("secondTrack");
                    FindObjectOfType<SoundManager>().Play("thirdTrack");
                    FindObjectOfType<SoundManager>().Play("fourthTrack");
                    FindObjectOfType<SoundManager>().Play("fifthTrack");
                    FindObjectOfType<SoundManager>().Play("sixthTrack");
                }
                else if (GliderController.acceleration >= 2f && GliderController.acceleration < 5f)
                {
                    FindObjectOfType<SoundManager>().VolumeUp("secondTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("thirdTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fourthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fifthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("sixthTrack");


                }
                else if (GliderController.acceleration >= 5f && GliderController.acceleration < 8f)
                {
                    FindObjectOfType<SoundManager>().VolumeUp("thirdTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fourthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fifthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("sixthTrack");

                }
                else if (GliderController.acceleration >= 8f)
                {
                    FindObjectOfType<SoundManager>().VolumeUp("fourthTrack");
                    FindObjectOfType<SoundManager>().VolumeUp("fifthTrack");
                    FindObjectOfType<SoundManager>().VolumeUp("sixthTrack");
                }
                break;
            case "time_mode":
                if (GliderController.acceleration < 2f)
                {
                    FindObjectOfType<SoundManager>().Play("secondTrack");
                    FindObjectOfType<SoundManager>().Play("thirdTrack");
                    FindObjectOfType<SoundManager>().Play("fourthTrack");
                    FindObjectOfType<SoundManager>().Play("fifthTrack");
                    FindObjectOfType<SoundManager>().Play("sixthTrack");
                }
                else if (GliderController.acceleration >= 2f && GliderController.acceleration < 5f)
                {
                    FindObjectOfType<SoundManager>().VolumeUp("secondTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("thirdTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fourthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fifthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("sixthTrack");


                }
                else if (GliderController.acceleration >= 5f && GliderController.acceleration < 8f)
                {
                    FindObjectOfType<SoundManager>().VolumeUp("thirdTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fourthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("fifthTrack");
                    FindObjectOfType<SoundManager>().VolumeDown("sixthTrack");

                }
                else if (GliderController.acceleration >= 8f)
                {
                    FindObjectOfType<SoundManager>().VolumeUp("fourthTrack");
                    FindObjectOfType<SoundManager>().VolumeUp("fifthTrack");
                    FindObjectOfType<SoundManager>().VolumeUp("sixthTrack");
                }
                break;
            case "Menu":   
                FindObjectOfType<SoundManager>().Play("MainTrack");
                FindObjectOfType<SoundManager>().StopPlaying("secondTrack");
                FindObjectOfType<SoundManager>().StopPlaying("thirdTrack");
                FindObjectOfType<SoundManager>().StopPlaying("fourthTrack");
                FindObjectOfType<SoundManager>().StopPlaying("fifthTrack");
                FindObjectOfType<SoundManager>().StopPlaying("sixthTrack");
                break;
        }

        FindObjectOfType<SoundManager>().VolumeOff("MainTrack");
        FindObjectOfType<SoundManager>().VolumeOff("secondTrack");
        FindObjectOfType<SoundManager>().VolumeOff("thirdTrack");
        FindObjectOfType<SoundManager>().VolumeOff("fourthTrack");
        FindObjectOfType<SoundManager>().VolumeOff("fifthTrack");
        FindObjectOfType<SoundManager>().VolumeOff("sixthTrack");

        Debug.Log(SoundVolume.tog);


    }
}
