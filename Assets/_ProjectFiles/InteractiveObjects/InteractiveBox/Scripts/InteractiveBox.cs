using UnityEngine;

public class InteractiveBox : MonoBehaviour, IInteractable
{
    [Header("Links")]
    [SerializeField] private ItemData _requiredKey;
    [SerializeField] private Animator _animator;

    private bool _isOpen;


    public string GetInteractText()
    {
        if (!_isOpen)
        {
            if (PlayerController.Instance.HaveItem
            && PlayerController.Instance.CurretItem.Data.IetmID == _requiredKey.IetmID)
                return "Нажми [E] чтобы открыть";

            else return "Нужен ключ";
        }

        return "";
    }

    public void Interact()
    {
        if (!_isOpen
        && PlayerController.Instance.HaveItem
        && PlayerController.Instance.CurretItem.Data.IetmID == _requiredKey.IetmID)
        {
            Destroy(PlayerController.Instance.CurretItem.gameObject);
            PlayerController.Instance.HaveItem = false;
            PlayerController.Instance.CurretItem = null;

            _animator.SetTrigger("Open");
            _isOpen = true;
        }
    }

}
