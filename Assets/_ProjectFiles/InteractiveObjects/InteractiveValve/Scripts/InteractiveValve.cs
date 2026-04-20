using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractiveValve : MonoBehaviour, IInteractable
{
    [Header("Links")]
    [SerializeField] private InteractiveObjectsSettings _settings;
    [SerializeField] private GameObject _door;

    private Vector3 _doorStartPos;
    private bool _isInteracting;
    private float _rotateAngle;


    void Awake()
    {
        _doorStartPos = _door.transform.localPosition;
    }

    private void Update()
    {
        if (_isInteracting)
        {
            _rotateAngle = Mathf.MoveTowards(_rotateAngle, _settings.ValveMaxRorateAngle, _settings.ValveSpeedRorate * Time.deltaTime);
        }
        else
        {
            _rotateAngle = Mathf.MoveTowards(_rotateAngle, 0, _settings.ValveSpeedRorate / 2 * Time.deltaTime);
        }

        ApplyDoorPosition();

        _isInteracting = false;
    }

    public string GetInteractText()
    {
        if (!_isInteracting && MangerGame.CourceCompledet) return "Зажми [E] для вращения";
        else if(!MangerGame.CourceCompledet) return "Смена еще не окончена";
        return "";
    }

    public void Interact()
    {
    }

    public void HoldInteract()
    {
        if (MangerGame.CourceCompledet) _isInteracting = true;
    }

    private void ApplyDoorPosition()
    {
        transform.localRotation = Quaternion.Euler(0, 0, _rotateAngle);

        float progress = _rotateAngle / _settings.ValveMaxRorateAngle;

        _door.transform.localPosition = _doorStartPos + new Vector3(0, progress * _settings.MaxDoorHight, 0);
    }

}
