using UnityEngine;

[CreateAssetMenu(menuName = "Folded Game/DayNightCycle Controller")]
public class SO_DayNightCycle : ScriptableObject
{
    [Range(-10,10)] public float timeSpeed = 0.1f;
    public Gradient lightIntensity;
    public Gradient lightSkyfog;
    public Gradient lightAmbience;
}
