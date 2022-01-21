using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FogPlayer : MonoBehaviour
{
   
    private GameObject glider;
    private Vector3 direction = Vector3.forward;
    private Vector3 planeDir;


    public GameObject fog;
    public GameObject fogClone;

    public Image brightLight;
    Color temp = Color.white;
    public float imagealpha = 0.1f;


    private GameObject fogGameObject;
    public GameObject fogParent;

    public float timeDeleteFog = 5;

    float fogDistance = 70;

 

    Vector3 heading;
    private bool canSpawn;


    private void Start()
    {
        canSpawn = true;
        fogGameObject = (GameObject)Instantiate(fogParent, transform.position, Quaternion.identity);
        GameObject imageObject = GameObject.FindGameObjectWithTag("FogLight");
        temp = brightLight.color;
        temp.a = 0f;
       



    }

    void Update()
    {
        GameObject imageObject = GameObject.FindGameObjectWithTag("FogLight");



        glider = GameObject.FindGameObjectWithTag("Player");
        if(imageObject != null)
        {
            brightLight = imageObject.GetComponent<Image>();
        }
       

        
        RaycastHit hit;
        float Distance;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, forward, Color.green);


        if(Physics.Raycast(transform.position,(forward), out hit))
        {
            Distance = hit.distance;
            //print(Distance + " " + hit.collider.gameObject.name);
        }
        if(hit.collider.gameObject.name == "Boundary_Wall" && hit.distance <= 500)
        {

            //StartCoroutine(FogDelay());
            FogSpawn();
            Destroy(fogClone, timeDeleteFog);
            temp.a += 0.125f * Time.deltaTime;
            brightLight.color = temp;



        }
        else
        {
            canSpawn = true;
            temp.a -= 0.125f * Time.deltaTime;
            brightLight.color = temp;
        }


        if(temp.a <= 0f)
        {
            temp.a = 0;
        }
        else if (temp.a > 0.7f)
        {
            temp.a = 0.7f;
        }

    }


    

    void FogSpawn()
    {
        Vector3 spawnPos = glider.transform.position + glider.transform.forward * fogDistance;
        fogClone = (GameObject)Instantiate(fog, spawnPos, Quaternion.identity);
        fogClone.transform.SetParent(fogGameObject.transform);

    }

  
}
