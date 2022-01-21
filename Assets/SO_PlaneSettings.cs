using UnityEngine;

[CreateAssetMenu(menuName = "Folded Game/New Plane Settings")]
public class SO_PlaneSettings : ScriptableObject
{
    public bool showDebugText = false;
    [Range(1,10000)] public float accelerationStrength = 1000f;
    [Range(0,2)] public float verticalTurning = 1f;
    [Range(0,2)] public float rollStrength = 1f;
    [Range(0,2)] public float horizontalTurning = 1f;
}
