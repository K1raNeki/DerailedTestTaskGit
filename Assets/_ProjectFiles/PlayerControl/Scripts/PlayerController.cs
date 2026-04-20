using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Links")]
    public static PlayerController Instance;
    public PlayerMovementSettings Settings;
    [SerializeField] private CharacterController _chController;
    public PlayerState State = PlayerState.Default;

    [Header("UpItem")]
    [HideInInspector] public BaseItem CurretItem;
    public GameObject PlayerSlot;
    public bool HaveItem;

    [Header("ControlsSettings")]
    private Camera _camera;
    private PlayerInputActions _actions;
    private float _gravityVelosity;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private float _pitch;


    void Awake()
    {
        Instance = this;

        _camera = Camera.main;
        _actions = new PlayerInputActions();
    }

    void Update()
    {
        _moveInput = _actions.Player.Move.ReadValue<Vector2>();
        _lookInput = _actions.Player.Look.ReadValue<Vector2>();

        switch (State)
        {
            case PlayerState.Default:
                HandleMovement();
                HandleRotation();
                break;

            case PlayerState.Inspection:
                break;

            case PlayerState.Stan:
                break;
        }
    }

    private void HandleMovement()
    {
        Vector3 direction = transform.right * _moveInput.x + transform.forward * _moveInput.y;

        if (!_chController.isGrounded)
        {
            _gravityVelosity += Settings.Gravity * Time.deltaTime;
        }
        else _gravityVelosity = -2f;

        direction *= Settings.MoveSpeed;
        direction.y = _gravityVelosity;

        _chController.Move(direction * Time.deltaTime);
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up * _lookInput.x * Settings.Sensitivity);
        _pitch -= _lookInput.y * Settings.Sensitivity;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        _camera.transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    public bool IsInteractPressed() { return _actions.Player.Interact.triggered; }
    public bool IsInteractHold() { return _actions.Player.Interact.IsPressed(); }
    public bool IsClickHold() { return _actions.Player.Click.IsPressed(); }
    public bool IsClickPressed() { return _actions.Player.Click.triggered; }
    public Vector2 GetLookInput() { return _lookInput; }
    public void PickUpItem(BaseItem item, bool physItem = false)
    {
        if (!HaveItem)
        {
            CurretItem = item;
            CurretItem.MoveItemToSlot(physItem);
            HaveItem = true;
        }
    }


    private void OnEnable() => _actions.Player.Enable();
    private void OnDisable() => _actions.Player.Disable();
}

public enum PlayerState
{
    Default,
    Inspection,
    Stan
}