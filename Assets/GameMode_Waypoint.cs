using UnityEngine;

public class GameMode_Waypoint : MonoBehaviour
{
    private NotificationManager notify;
    private int totalWaypoints;
    private int collectedWaypoints;
    private TimeController tc;

    // Draw Path between all hoops in editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i=transform.childCount-1; i>0; i--)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i-1).position);
        }
    }

    void Start()
    {
        totalWaypoints = transform.childCount;
        notify = GameObject.FindWithTag("Notifications").GetComponent<NotificationManager>();
        notify.newNotification($"Collect all {totalWaypoints} waypoints!");
        tc = GameObject.Find("TimeController").GetComponent<TimeController>();
    }

    public void onPlayerCollection(GameObject waypoint)
    {
        Destroy(waypoint);
        collectedWaypoints++;
        if (collectedWaypoints < totalWaypoints) { notify.newNotification($"You have collected {collectedWaypoints}/{totalWaypoints} waypoints!."); }
        else {
            notify.newNotification($"Congratulations, you have collected all {totalWaypoints} waypoints!");
            tc.StartCoroutine(tc.OnComplete());
        }
    }
}