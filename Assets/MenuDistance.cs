using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDistance : MonoBehaviour
{
    DistanceController dc;
    public InputField nameScore;

    public static bool isDistanceMode = false;
    public static bool isExploreMode = false;
    public static bool isTimerMode = false;

    public void Start()
    {
        dc = GameObject.Find("Canvas").GetComponent<DistanceController>();
    }
    public void Endless()
    {
        isExploreMode = true;
        SceneManager.LoadScene("PlaneSelectScreen");
        Debug.Log("exploremode is "+ isExploreMode);
        Time.timeScale = 1;
       
    }

    public void Distance()
    {
        isDistanceMode = true;
        Debug.Log("distance mode is " + isDistanceMode);
        SceneManager.LoadScene("PlaneSelectScreen");
        
    
    }

    public void Timer()
    {
        isTimerMode = true;
        SceneManager.LoadScene("PlaneSelectScreen");
        Debug.Log("distance mode is " + isTimerMode);
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void DistanceScoreBoard()
    {
        PlayerPrefs.SetInt("distanceScore", dc.distanceKm);
        PlayerPrefs.SetString("distanceName", nameScore.text);
        SceneManager.LoadScene("DistanceHighScore");
    }

  

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
