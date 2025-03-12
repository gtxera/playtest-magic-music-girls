using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, ICharacterAnimatorInputProvider
{
    private Input _input;

    private Vector2 _inputDirection;

    private InputActions.IPlayerActions _playerActions;

    [SerializeField]
    private float _speed;

    private Rigidbody2D _rigidbody2D;

    public Vector2 AnimationInput => _inputDirection;
    
    private void SetupInput()
    {
        _input = Input.Instance;
        _playerActions = new PlayerActionsCallbacks.Builder()
            .OnMove(OnMovePerformed, InputActionPhase.Performed)
            .OnMove(OnMoveCanceled, InputActionPhase.Canceled)
            .Build();
        _input.Add(_playerActions);
        _input.SetInputContext(InputContext.Player);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext _)
    {
        _inputDirection = Vector2.zero;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.freezeRotation = true;

        SetupInput();
    }

    private void OnDestroy()
    {
        _input.Remove(_playerActions);
    }

    private void FixedUpdate()
    {
        var position = _rigidbody2D.position + _inputDirection * (_speed * Time.fixedDeltaTime);
        _rigidbody2D.MovePosition(position);
    }
}
