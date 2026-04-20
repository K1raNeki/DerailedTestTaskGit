using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovenetSettings", menuName = "Scriptable Objects/PlayerMovenetSettings")]
public class PlayerMovementSettings : ScriptableObject
{
    [Header("Movement")]
    public float MoveSpeed = 7f;

    [Header("Look")]
    public float Sensitivity = 0.1f;

    [Header("Gravity")]
    public float Gravity = -10f;
}
