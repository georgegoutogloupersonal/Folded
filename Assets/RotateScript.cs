using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float RotateSpeed_X = 0f;
    public float RotateSpeed_Y = 50f;
    public float RotateSpeed_Z = 0f;
   
    void Update()
    {
        transform.Rotate(RotateSpeed_X * Time.deltaTime, RotateSpeed_Y * Time.deltaTime, RotateSpeed_Z * Time.deltaTime);
    }
}
