using UnityEngine;

[CreateAssetMenu(fileName = "InteractionSettings", menuName = "Scriptable Objects/InteractionSettings")]
public class InteractionSettings : ScriptableObject
{
    [Header("Colors")]
    public Color UnSelectColor;
    public Color SelectColor;

    [Header("Ray")]
    public float RayDistance = 4;


}
