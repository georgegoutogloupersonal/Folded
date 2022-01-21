using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public SO_GlobalSettings globalSettings;
    public Button buttonExplore;
    //public Button buttonDistance;
    public Button buttonTime;
    public Button buttonControls;
    public Button buttonQuit;

    void Start()
    {
        buttonExplore.onClick.AddListener(() => PlaneSelectScreen("Explore_Mode"));
        //buttonDistance.onClick.AddListener(() => PlaneSelectScreen("distance_mode"));
        buttonTime.onClick.AddListener(() => PlaneSelectScreen("time_mode"));
        buttonQuit.onClick.AddListener(() => Application.Quit());

        // Controls menu
        //buttonControls.onClick.AddListener(() => SceneManager.LoadScene("ControlsScene"));
    }

    void PlaneSelectScreen(string GameMode)
    {
        globalSettings.SetGameMode(GameMode);
        SceneManager.LoadScene("PlaneSelectScreen");
    }

    public void SoundSettings()
    {
        SceneManager.LoadScene("SoundSettings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
