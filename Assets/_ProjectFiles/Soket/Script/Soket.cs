using UnityEngine;

public class Soket : MonoBehaviour, IInteractable
{
    [Header("Links")]
    [HideInInspector] public BaseItem CurretItem;
    [SerializeField] private Transform _attachPoint;


    public string GetInteractText()
    {
        if (PlayerController.Instance.HaveItem)
        {
            if (CurretItem == null) return "[E] Положить предмет";
            if (CurretItem != null) return "Гнездо занято";
        }
        return "У вас нечего класть";
    }

    public void Interact()
    {
        if (PlayerController.Instance.HaveItem && CurretItem == null)
        {
            CurretItem = PlayerController.Instance.CurretItem;

            CurretItem.transform.SetParent(_attachPoint);
            CurretItem.transform.position = _attachPoint.position;
            CurretItem.transform.rotation = Quaternion.identity;
            CurretItem.transform.localScale = Vector3.one;

            CurretItem.MySoket = this;

            PlayerController.Instance.HaveItem = false;
            PlayerController.Instance.CurretItem = null;
        }
    }

}
