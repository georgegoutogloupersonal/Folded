using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance_Test : MonoBehaviour
{
    void OnDrawGizmos()
    {
        RaycastHit hit;
        float rayLength = 200f; // <- factor in speed?
        Ray colliderRay = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(colliderRay, out hit, rayLength))
        {
            // Math
            Vector3 reflection = Vector3.Reflect(transform.forward, hit.normal);
            float distance = Vector3.Distance(transform.position, hit.point) * 1.1f;
            Vector3 newPosition = hit.point + reflection * distance;

            // flatten y axis
            if (newPosition.y < transform.position.y)
                newPosition = new Vector3(newPosition.x, transform.position.y, newPosition.z); // flatten Y
            
            Vector3 newDirection = (newPosition - transform.position).normalized;

            // Visuals
            Debug.DrawRay(hit.point, hit.normal *distance, Color.yellow); // normal
            Debug.DrawRay(hit.point, reflection *distance, Color.blue); // reflection
            Debug.DrawRay(transform.position, transform.forward *distance, Color.red); // collision
            Debug.DrawRay(transform.position, newDirection * distance, Color.green); // new direction
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(hit.point, 1f); // collision
            Gizmos.DrawWireSphere(newPosition, 1f); // new target
        }
        else
            Debug.DrawRay(transform.position, transform.forward *rayLength, Color.white); // no collision
    }
}