using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DistanceController : MonoBehaviour
{
    public Text distance;
    public GameObject DistanceTextParent;
    public int distanceKm;
    public GameObject distanceRanPanel;
    private float decimalKM;
    private PlayerSpawn ps;
    private bool isGliderInScene = false;

    public InputField nameScore;

    private void Start()
    {
        distanceKm = 0;
        ps = GameObject.Find("PlayerSpawnPoint").GetComponent<PlayerSpawn>();
    }

    private void Update()
    {
        if (isGliderInScene)
        {
            DistanceStart();
            if (ps.IsPlayerAlive() == false)
            {
                StartCoroutine(OnHitGround());
            }
        }
        else
            isGliderInScene = (ps.GetPlayer() != null);
    }

    public void DistanceStart()
    {
    
        decimalKM += Time.deltaTime * 1f;
        distanceKm = Mathf.RoundToInt(decimalKM);
        distance.text = distanceKm.ToString() + " KM";
    }

    IEnumerator OnHitGround()
    {
        yield return new WaitForSeconds(2.0f);
        distanceRanPanel.SetActive(true);
        Time.timeScale = 0;
       
    }

    public void FinishDistance()
    {
        PlayerPrefs.SetInt("distanceScore", distanceKm);
        PlayerPrefs.SetString("distanceName", nameScore.text);
        SceneManager.LoadScene("GameScene_HighscoreTable");
    }
}
