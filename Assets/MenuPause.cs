using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    private bool isPaused;
    private float original_timescale;
    private Transform panel;
    private Button buttonMenu;
    private Button buttonResume;
    private Button buttonQuit;

    private void Awake()
    {
        // initialize buttons and set menu to inactive
        if (panel == null)
        {
            panel = transform.GetChild(0);
            buttonMenu = panel.GetChild(0).GetComponent<Button>();
            buttonMenu.onClick.AddListener(() => ClickedMenu());
            buttonResume = panel.GetChild(1).GetComponent<Button>();
            buttonResume.onClick.AddListener(() => ClickedResume());
            buttonQuit = panel.GetChild(2).GetComponent<Button>();
            buttonQuit.onClick.AddListener(() => ClickedQuit());
            isPaused = false;
            panel.gameObject.SetActive(isPaused);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // unpause
                Time.timeScale = original_timescale;
                isPaused = false;
                panel.gameObject.SetActive(isPaused);
            }
            else
            {
                // pause
                original_timescale = Time.timeScale;
                Time.timeScale = 0;
                isPaused = true;
                panel.gameObject.SetActive(isPaused);
            }
        }
    }

    private void ClickedMenu()
    {
        Time.timeScale = original_timescale;
        SceneManager.LoadScene(0);
        SoundManager.instance.StopPlaying("GameMusic");
        SoundManager.instance.Play("MenuMusic");
    }

    private void ClickedResume()
    {
        Time.timeScale = original_timescale;
        isPaused = false;
        panel.gameObject.SetActive(isPaused);
    }

    private void ClickedQuit()
    {
        Time.timeScale = original_timescale;
        Application.Quit();
    }
}
