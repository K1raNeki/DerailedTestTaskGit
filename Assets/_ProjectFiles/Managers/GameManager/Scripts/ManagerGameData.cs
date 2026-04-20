using UnityEngine;

[CreateAssetMenu(fileName = "ManagerGameData", menuName = "Scriptable Objects/ManagerGameData")]
public class ManagerGameData : ScriptableObject
{
    [Header("EndingDescription")]
    [TextArea] public string GoodEndingText;
    [TextArea] public string BadEndingText;
}
