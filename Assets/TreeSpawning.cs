using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawning : MonoBehaviour
{
    Ray myRay;
    RaycastHit hit;
    public GameObject tree;
    public int treesToSpawn;
    public bool isAbleToSpawn = true;
    public GameObject treeDestroy;
    public GameObject player;
    

    private void Start()
    {
        treesToSpawn = 5;
        
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(myRay, out hit))
        {
            //Debug.Log(hit.collider.tag);

            if (hit.collider.gameObject.tag == "Ground" && player.GetComponent<Rigidbody>().useGravity == true)
            {
               
                if (Input.GetMouseButtonDown(0) && isAbleToSpawn == true)
                {
                    GameObject clone = Instantiate(tree, hit.point, Quaternion.identity);
                    treesToSpawn -= 1;
                }
                else if (treesToSpawn <= 0)
                {
                    isAbleToSpawn = false;

                }
                else if (treesToSpawn >= 1)
                {
                    isAbleToSpawn = true;
                }

                
            }

            if (Input.GetButtonDown("Fire2"))
            {

                if (hit.collider.gameObject.tag == "Tree")
                {
                    Destroy(hit.collider.gameObject);
                    GameObject treeParticle = Instantiate(treeDestroy, hit.point, Quaternion.identity);
                    Destroy(treeParticle, 1.5f);
                    treesToSpawn += 1;
                }

            }


        }
    }


}
