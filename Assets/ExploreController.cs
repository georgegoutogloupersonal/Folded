using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExploreController : MonoBehaviour
{

    private PlayerSpawn ps;
    private bool isGliderInScene = false;

    private void Start()
    {
      
        ps = GameObject.Find("PlayerSpawnPoint").GetComponent<PlayerSpawn>();
    }

    private void Update()
    {
        if (isGliderInScene)
        {
           
            if (ps.IsPlayerAlive() == false)
            {
                OnHitGround();
            }
        }
        else
            isGliderInScene = (ps.GetPlayer() != null);
    }

    public void OnHitGround()
    {
        SceneManager.LoadScene(Application.loadedLevel);

    }

  
}
