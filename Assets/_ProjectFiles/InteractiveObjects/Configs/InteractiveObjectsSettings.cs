using UnityEngine;

[CreateAssetMenu(fileName = "InteractiveObjectsSettings", menuName = "Scriptable Objects/InteractiveObjectsSettings")]
public class InteractiveObjectsSettings : ScriptableObject
{
    [Header("ValveSettings")]
    public float ValveMaxRorateAngle = 100f;
    public float ValveSpeedRorate = 30f;
    public float MaxDoorHight = 6f;

}
