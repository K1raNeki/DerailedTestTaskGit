using UnityEngine;

public class InteractiveComputer : MonoBehaviour, IInteractable
{
    [Header("Links")]
    [SerializeField] private ItemData _requiredCource;
    [SerializeField] private ComputerQuest _computerQuest;

    private bool _isEnable;


    public string GetInteractText()
    {
        if (!_isEnable)
        {
            if (PlayerController.Instance.HaveItem
                && PlayerController.Instance.CurretItem.Data.IetmID == _requiredCource.IetmID)
                return "Нажми [E] чтобы вставить";

            else return "Нечего вставлять";
        }

        return "";
    }

    public void Interact()
    {
        if (!_isEnable
            && PlayerController.Instance.HaveItem
            && PlayerController.Instance.CurretItem.Data.IetmID == _requiredCource.IetmID)
            {
                Destroy(PlayerController.Instance.CurretItem.gameObject);
                PlayerController.Instance.HaveItem = false;
                PlayerController.Instance.CurretItem = null;

                _isEnable = true;

                _computerQuest.ComputerQuestStart(true);
        }
    }

}
