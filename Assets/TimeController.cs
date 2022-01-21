using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public Text timer;
    public int timerSeconds;
    public Text timeScore;

    //panels
    public GameObject completePanel;
    public GameObject failPanel;

    private float timerF;
    private PlayerSpawn ps;
    private bool isGliderInScene = false;
    private bool hasCrashed = false;

    public InputField nameScore;

    public GameObject player;

    private void Start()
    {
        timerSeconds = 0;
        ps = GameObject.Find("PlayerSpawnPoint").GetComponent<PlayerSpawn>();
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<Rigidbody>().useGravity == true)
        {
            TimeStart();
        }

        if (isGliderInScene)
        {
            if (ps.IsPlayerAlive() == false)
            {
                StartCoroutine(OnHitGround());
                
            }
        }
        else
            isGliderInScene = (ps.GetPlayer() != null);

        
    }


    public void TimeStart()
    {
        if (ps.IsPlayerAlive() == true)
        {
            timerF += Time.deltaTime * 1f;
            timerSeconds = Mathf.RoundToInt(timerF);
            timer.text = timerSeconds.ToString();
        }
    }

    public IEnumerator OnComplete()
    {
        yield return new WaitForSeconds(2.0f);
        completePanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0;
        
    }

    IEnumerator OnHitGround()
    {
        yield return new WaitForSeconds(2.0f);
        failPanel.SetActive(true);
        
    }

    public void FinishTimer()
    {
        PlayerPrefs.SetInt("timerScore", timerSeconds);
        PlayerPrefs.SetString("scoreName", nameScore.text);
        SceneManager.LoadScene("TimerHighScore");
    }

    public void Retry()
    {
        SceneManager.LoadScene(Application.loadedLevel);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
