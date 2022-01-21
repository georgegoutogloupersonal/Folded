using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Node : MonoBehaviour
{
    // PRIVATE
    private bool isSelected = false;
    // PUBLIC
    public List<GameObject> neighbour;
    

    //Highlight connected nodes on select
    void OnDrawGizmosSelected()
    {
        // if (!showGizmos) return;
        isSelected = true;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5);
        for (int i=0; i<neighbour.Count; i++)
        {
            if (neighbour[i])
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, neighbour[i].transform.position, 0.5f));
        }
        isSelected = false;
    }

    // draw lines connecting each node
    void OnDrawGizmos()
    {
        // if (!showGizmos) return;
        if (isSelected) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 5);
        for (int i=0; i<neighbour.Count; i++)
        {
            if (neighbour[i])
                Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, neighbour[i].transform.position, 0.5f));
        }
    }

    public List<GameObject> GetNeighbours()
    {
        List<GameObject> results = new List<GameObject>();
        for (int i=0; i<neighbour.Count; i++)
            results.Add(neighbour[i]);
        return results;
    }
}
