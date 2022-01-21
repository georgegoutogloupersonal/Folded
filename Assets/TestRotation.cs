using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    // public bool correction = false;
    public float pitchStrength = 2f;
    public float turnStrength = 2f;
    public float barrel = 0f;
    private Transform rollBody;

    private void Start()
    {
        rollBody = transform.GetChild(0);
    }

    private void Update()
    {
        // ROLL CORRECTION
        // ROLL TURNING

        Pitch();
        Turn();
        //
        // testrot += 1f;
        // if (testrot >= 360f)
        //     testrot -= 360f;
        // Vector3 rotation = transform.rotation.eulerAngles;
        // transform.rotation = Quaternion.Euler(testrot, rotation.y, rotation.z);
        // if (correction) 
        // {
        //     Quaternion start = transform.rotation;
        //     Quaternion end = transform.rotation;
        //     transform.rotation = Quaternion.Slerp(start, end, timeCount);

        //     if (start == end)
        //         correction = false;
        //     return;
        // }
    }


    // private void Pitch()
    // {
    //     float input = Input.GetAxis("Vertical");

    //     if (Mathf.Abs(input) > inputDeadZone)
    //     {
    //         // Change the Pitch via horizontal input
    //         Vector3 rotation = transform.rotation.eulerAngles;
    //         transform.rotation = Quaternion.Euler(rotation.x + (verticalTurning * input), rotation.y, rotation.z);
    //     }
    // }

    private void Pitch()
    {
        float input = Input.GetAxis("Vertical");

        //if (Mathf.Abs(input) > inputDeadZone)
        transform.Rotate(Vector3.right, pitchStrength * input);
    }


// private void Turn()
//     {
//         float input = Input.GetAxis("Horizontal");
//
//         if (Mathf.Abs(input) > inputDeadZone)
//         {
//             // Turn & Roll via horizontal input
//             Vector3 rotation = transform.rotation.eulerAngles;
//             transform.rotation = Quaternion.Euler(rotation.x, rotation.y + (horizontalTurning * input), rotation.z + (rollStrength * -input));
//         }
//         else if (Mathf.Abs(transform.forward.y) <= 0.3f)
//         {
//             // Roll Smoothing, levels out the plane when no input is recieved
//             Vector3 rotation = transform.rotation.eulerAngles;
//             Quaternion smoothRoll = Quaternion.Euler(rotation.x, rotation.y, 0f);
//             transform.rotation = Quaternion.Slerp(transform.rotation, smoothRoll, 0.02f);
//         }
//     }

    private void Turn()
    {
        float input = Input.GetAxis("Horizontal");

        //if (Mathf.Abs(input) > inputDeadZone)
        {
            // Turn
            transform.Rotate(Vector3.up, turnStrength * input);

            // Roll
            //if (rollBody.rotation.eulerAngles.z > -90f && rollBody.rotation.eulerAngles.z < 90f)
            rollBody.Rotate(Vector3.forward, -turnStrength * input);
        }

        // ROLL CORRECTION
    }

    void BarrelRoll()
    {
        //rollBody.Rotate(Vector3.forward, barrel * Time.deltaTime);
    }
    
}
