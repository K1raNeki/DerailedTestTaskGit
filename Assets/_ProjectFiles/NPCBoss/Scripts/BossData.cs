using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Scriptable Objects/BossData")]
public class BossData : ScriptableObject
{
    [Header("DialogLines")]
    [TextArea] public string StartSpeech;
    [TextArea] public string[] FalseReaction;
    [TextArea] public string TrueReaction;
    [TextArea] public string GratitudeReaction;
    [TextArea] public string DisappoinntmentSpeech;

    [Header("InteractiveText")]
    public string StartIIText;
    public string[] ReationOnItem;
    public string ReationOnChoise;
    public string EndIIText;

    [Header("QuestDescription")]
    public string MainQuestDescription;

}
