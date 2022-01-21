using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject camera;
    private Vector3 velocity = Vector3.zero;
    // Smaller the number, the closer the follow.
    public float proximity = 0.05f;

    void Start()
    {
        // Create Camera
        GameObject cameraResource = Resources.Load<GameObject>("FollowCamera");
        camera = Instantiate(cameraResource, this.transform.position, this.transform.rotation);
    }

    void FixedUpdate()
    {
        // Follow
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, this.transform.position, ref velocity, proximity);
        
        // Look at
        camera.transform.LookAt(this.transform, this.transform.up);
    }
}
