using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private InteractionSettings _settings;
    [SerializeField] private InspectionUI _inspectionUI;
    [SerializeField] private Image _point;
    [SerializeField] private TextMeshProUGUI _hintText;
    [SerializeField] private Transform _inspectionPlace;

    private BaseItem _inspectedItem;
    private RaycastHit _hit;
    private bool _objCatched;


    void Awake()
    {
        _point.color = _settings.UnSelectColor;

        _hintText.text = "";
    }

    void Update()
    {
        switch (PlayerController.Instance.State)
        {
            case PlayerState.Default:
                PlaceItem();
                break;

            case PlayerState.Inspection:
                UpdateInspection();
                break;

        }
    }

    private void PlaceItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _settings.RayDistance))
        {
            IInteractable interactable = _hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                _point.color = _settings.SelectColor;
                _hintText.text = interactable.GetInteractText();

                if (PlayerController.Instance.IsInteractPressed())
                {
                    if (interactable is BaseItem item) StartInspection(item);
                    else interactable.Interact();
                }

                if (PlayerController.Instance.IsInteractHold()) interactable.HoldInteract();
            }
        }
        else
        {
            _point.color = _settings.UnSelectColor;
            _hintText.text = "";
        }
    }

    private void StartInspection(BaseItem item)
    {
        _inspectedItem = item;

        CheakAnimationOpen(_inspectedItem, true);

        _inspectedItem.transform.SetParent(_inspectionPlace);
        _inspectedItem.transform.localPosition = Vector3.zero;
        _inspectedItem.transform.localRotation = Quaternion.identity;

        _inspectionUI.EnableInspectionUI(true);
        _inspectionUI.EnterText(_inspectedItem.Data.Name, _inspectedItem.Data.Description);


        _point.color = _settings.UnSelectColor;
        PlayerController.Instance.State = PlayerState.Inspection;
    }

    private void UpdateInspection()
    {
        _hintText.text = "Нажми [E] чтобы забрать";

        if (PlayerController.Instance.IsClickPressed())
        {
            if (IsCathInspectedItem())
            {
                _objCatched = true;
            }
        }

        if (PlayerController.Instance.IsClickHold() && _objCatched)
        {
            Vector2 mouseDelta = PlayerController.Instance.GetLookInput();
            float sensitivity = PlayerController.Instance.Settings.Sensitivity;

            _inspectedItem.transform.Rotate(Vector3.up, -mouseDelta.x * sensitivity, Space.World);
            _inspectedItem.transform.Rotate(Vector3.right, mouseDelta.y * sensitivity, Space.World);
        }
        else if (!PlayerController.Instance.IsClickHold())
        {
            _objCatched = false;
        }

        if (PlayerController.Instance.IsInteractPressed())
        {
            _inspectedItem.ItemPhysDisable(false);
            _inspectedItem.Interact();
            CheakAnimationOpen(_inspectedItem, false);


            _inspectionUI.EnableInspectionUI(false);
            _inspectedItem = null;
            _objCatched = false;
            PlayerController.Instance.State = PlayerState.Default;
            _hintText.text = "";
        }
    }

    private bool IsCathInspectedItem()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform.IsChildOf(_inspectedItem.transform);
        }

        return false;
    }

    private void CheakAnimationOpen(BaseItem item, bool open)
    {
        if (item.TryGetComponent(out Animator anim))
        {
            anim.SetBool("Open", open);
        }
    }

}


