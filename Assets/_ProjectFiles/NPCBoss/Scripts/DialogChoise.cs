using UnityEngine;

public class DialogChoise : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _currentChoise;
    [SerializeField] private BossNPC _bossNPC;
    public string GetInteractText()
    {
        return "[E] Принять решение";
    }

    public void Interact()
    {
        switch (_currentChoise)
        {
            case true:
                Debug.Log("yes");
                break;

            case false:
                Debug.Log("no");
                _bossNPC.BossDeceived();
                break;
        }
    }
}
