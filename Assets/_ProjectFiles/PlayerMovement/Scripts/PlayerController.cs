using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private PlayerMovementSettings _settings;
    [SerializeField] private CharacterController chController;

    private Camera _camera;
    private PlayerInputActions _actions;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private float _pitch;

    void Awake()
    {
        _camera = Camera.main;
        _actions = new PlayerInputActions();
    }

    void Update()
    {
        _moveInput = _actions.Player.Move.ReadValue<Vector2>();
        _lookInput = _actions.Player.Look.ReadValue<Vector2>();

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 direction = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        chController.Move(direction * _settings.MoveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up * _lookInput.x * _settings.Sensitivity);
        _pitch -= _lookInput.y * _settings.Sensitivity;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        _camera.transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }


    private void OnEnable() => _actions.Player.Enable();
    private void OnDisable() => _actions.Player.Disable();
}
