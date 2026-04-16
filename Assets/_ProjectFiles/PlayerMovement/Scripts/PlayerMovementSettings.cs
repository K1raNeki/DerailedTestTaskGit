using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovenetSettings", menuName = "Scriptable Objects/PlayerMovenetSettings")]
public class PlayerMovementSettings : ScriptableObject
{
    [Header("Movement")]
    public float MoveSpeed = 7f;

    [Header("Look")]
    public float Sensitivity = 0.1f;
}
