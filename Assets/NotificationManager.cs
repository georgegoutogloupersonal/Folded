using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    private GameObject messageObject;
    private Dictionary <GameObject, float> messages = new Dictionary<GameObject, float>();
    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
        messageObject = Resources.Load<GameObject>("NotificationMessage");
    }

    public void newNotification(string message, float duration = 5f)
    {
        GameObject newMessage = Instantiate(messageObject);
        newMessage.GetComponent<RectTransform>().SetParent(GameObject.FindWithTag("Notifications").transform);
        newMessage.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
        newMessage.GetComponent<Text>().text = message;
        messages.Add(newMessage, Time.time+duration);
    }

    private void FixedUpdate()
    {
        // Remove each messsage after duration expires
        foreach (KeyValuePair<GameObject, float> i in messages) {
            if (i.Value < Time.time)
            {
                messages.Remove(i.Key);
                Destroy(i.Key);
                break;
            }
        }
    }
}