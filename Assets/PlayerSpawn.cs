using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Cinemachine;

public class PlayerSpawn : MonoBehaviour
{
    // Public Prefabs
    public SO_GlobalSettings globalSettings;
    public Button ResetButtonGUI;
    private Texture2D[] tweets;
    // Private
    GameObject launchGUI;
    private GameObject Glider;
    private CinemachineVirtualCamera virtualCamera;
    private GameObject[] spawnPoints;
    private int currentSpawn = 0;

    private void Start()
    {
        StartCoroutine(WaitForMapbox());
    }

    IEnumerator WaitForMapbox()
    {
        // fetch Spawn points
        yield return new WaitUntil(() => spawnPoints != null );

        virtualCamera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        // Spawn the player
        Respawn();

        // Create Launch gui
        //launchGUI = Instantiate(Resources.Load<GameObject>("LaunchSpawnGUI"));
        //launchGUI.transform.SetParent(GameObject.FindWithTag("Canvas").transform);

        yield return null;
    }

    public void NextSpawn()
    {
        if (currentSpawn == spawnPoints.Length-1)
            currentSpawn = 0;
        else
            currentSpawn++;
        Respawn();
    }

    public void PreviousSpawn()
    {
        if (currentSpawn == 0)
            currentSpawn = spawnPoints.Length-1;
        else
            currentSpawn--;
        Respawn();
    }

    public void Respawn()
    {
        if (Glider)
            Destroy(Glider);
        Glider = Instantiate(globalSettings.GetPlaneSelection(), spawnPoints[currentSpawn].transform.position, transform.rotation);
        Glider.GetComponent<GliderController>().AttatchSpawnScript(this);
        AttachCamera();
    }

    private void Update()
    {
        // Fetch spawn locations prior to spawning the player.
        if (spawnPoints == null)
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
    }

    private void AttachCamera()
    {
        virtualCamera.m_Follow = Glider.transform;
        virtualCamera.m_LookAt = Glider.transform;
    }

    public GameObject GetPlayer()
    {
        return Glider;
    }

    public bool IsPlayerAlive()
    {
        return Glider.GetComponent<GliderController>().isAlive;
    }

    public int GetSpawnIndex()
    {
        return currentSpawn;
    }
}
