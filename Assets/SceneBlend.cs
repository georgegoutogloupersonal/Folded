using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBlend : MonoBehaviour
{

    public Animator anim;
    public GameObject blendObject;

    public bool isAble;
   
    void Start()
    {

        //anim.SetTrigger("is_Blending");
        isAble = true;


    }

    
    void Update()
    {


        StartCoroutine(Blending());


    }
   

    IEnumerator Blending()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        switch (sceneName)
        {
            case "Explore_Mode":
               
                if (isAble)
                {
                    blendObject.SetActive(true);
                    anim.SetTrigger("is_Blending");
                    yield return new WaitForSeconds(1.5f);
                    blendObject.SetActive(false);
                }
                break;
            case "time_mode":
                
                if (isAble)
                {
                    blendObject.SetActive(true);
                    anim.SetTrigger("is_Blending");
                    yield return new WaitForSeconds(1.5f);
                    blendObject.SetActive(false);
                }
                break;
            case "Menu":
               
                if (isAble)
                {
                    blendObject.SetActive(true);
                    anim.SetTrigger("is_Blending");
                    yield return new WaitForSeconds(1.5f);
                    blendObject.SetActive(false);
                }  
                
                break;
        }

        isAble = false;

       
    }

}
