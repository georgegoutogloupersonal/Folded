using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneSelect : MonoBehaviour
{
    // Public
    public SO_GlobalSettings globalSettings;
    public GameObject[] stats;
    public Transform spawnPoint;
    public GameObject LoadingScreen;

    // Private
    private int index = 0;
    private GameObject PlanePreview;
    private GameObject PlaneStats;

    void Start()
    {
        PlanePreview = DisplayPlane(globalSettings.PlanePrefabs[index]);
        PlaneStats = GameObject.Instantiate(stats[index], spawnPoint.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    public GameObject DisplayPlane(GameObject plane)
    {
        GameObject display = Instantiate(plane, new Vector3(0f,0f,-8f), Quaternion.Euler(-30f,0f,0f));
        Destroy(display.GetComponent<GliderController>());
        Destroy(display.GetComponent<FogPlayer>());
        Destroy(display.GetComponent<TreeSpawning>());
        Destroy(display.GetComponent<Rigidbody>());
        display.AddComponent<RotateScript>();
        return display;
    }

    public void Previous()
    {
        Destroy(PlanePreview);
        Destroy(PlaneStats);
        index = (index == 0) ? globalSettings.PlanePrefabs.Count-1 : index-1;
        PlanePreview = DisplayPlane(globalSettings.PlanePrefabs[index]);
        PlaneStats = GameObject.Instantiate(stats[index], spawnPoint.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    public void Next()
    {
        Destroy(PlanePreview);
        Destroy(PlaneStats);
        index = (index == globalSettings.PlanePrefabs.Count-1) ? 0 : index+1;
        PlanePreview = DisplayPlane(globalSettings.PlanePrefabs[index]);
        PlaneStats = GameObject.Instantiate(stats[index], spawnPoint.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    public void Select()
    {
        globalSettings.SetPlaneSelection(index);
        StartCoroutine(LoadGameScene());
    }

    IEnumerator LoadGameScene()
    {
        //SceneManager.LoadScene(globalSettings.GetGameMode());
        Instantiate(LoadingScreen, GameObject.FindGameObjectWithTag("Canvas").transform);
        AsyncOperation async = SceneManager.LoadSceneAsync(globalSettings.GetGameMode());
        while (!async.isDone) yield return null;
    }
}
