using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour
{
   public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
