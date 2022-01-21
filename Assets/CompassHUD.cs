using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassHUD : MonoBehaviour
{
    public Transform playerTransform;
    private GameObject[] poi;
    private GameObject[] text;

    void Start()
    {
        // Fetch Assets
        GameObject iconObj = transform.GetChild(1).gameObject;
        GameObject textObj = transform.GetChild(2).gameObject;

        // Fetch points of interest
        poi = GameObject.FindGameObjectsWithTag("StreetLights");
        
        // Create icon Elements <>

        // Create text Elements
        text = new GameObject[4];
        for (int i=0; i<4; i++)
        {
            text[i] = Instantiate(textObj, Vector3.zero, Quaternion.identity);
            text[i].transform.SetParent(this.transform);
            text[i].GetComponent<RectTransform>().localPosition = new Vector3(0f,-100f,0f);
            text[i].GetComponent<Text>().text = "NESW"[i].ToString();
            text[i].name = $"Text {"NESW"[i]}";
        }
    }

    private void Update()
    {
        // Draw Compass Icons (Update)
        Vector3 behind = playerTransform.TransformDirection(Vector3.back);
        Vector3 side = playerTransform.TransformDirection(Vector3.right);
        for (int i=0; i<poi.Length; i++)
        {
            Vector3 other = Vector3.Normalize(poi[i].transform.position - playerTransform.position);
            if (Vector3.Dot(behind,other) > 0)
            {
                // hide offscreen
            }
            else
            {
                // float xpos = Vector3.Dot(side,other);
                // xpos abs > 0=100% opacicty, 1=0% alpha
                // alpha = 1-abs(xpos)
            }
            //Debug.Log($"{poi[i].name}: {Vector3.Dot(side,other)}"); // use this to draw ui
        }

        // Draw Compass text (Update)
        float xpos = Vector3.Dot(Vector3.forward, playerTransform.right);
        float ypos = Vector3.Dot(Vector3.forward, playerTransform.forward);
        text[0].SetActive(true);
        text[1].SetActive(true);
        text[2].SetActive(true);
        text[3].SetActive(true);


        if (xpos > 0)
        {
            text[3].transform.localPosition = new Vector3(-ypos*200f, 0f, 0f);
            text[1].SetActive(false);
        }
        else
        {
            text[1].transform.localPosition = new Vector3(ypos*200f, 0f, 0f);
            text[3].SetActive(false);
        }

        if (ypos > 0)
        {
            text[0].transform.localPosition = new Vector3(xpos*200f, 0f, 0f);
            text[2].SetActive(false);
        }
        else
        {
            text[2].transform.localPosition = new Vector3(-xpos*200f, 0f, 0f);
            text[0].SetActive(false);
        }
    }
}
