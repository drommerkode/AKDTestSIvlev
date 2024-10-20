using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _cc;

    private Vector2 _moveInput;
    private bool _jumpInput;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private float _acceleration = 8f;
    private Vector2 _smothMoveInput;
    private Vector3 _moveVelocity;
    private Vector3 _verticalVelocity;

    [Header("Jump")]
    [SerializeField] private float _jumpVelocity = 1f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f; 
    [SerializeField] private float _onGroundGravity = -2f;

    [Header("Check ground")]
    [SerializeField] private LayerMask _groundCheckMask;
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private Transform _groundCheck;
    private bool _isGrounded;

    private void Awake() {
        _cc = GetComponent<CharacterController>();
    }

    #region Input
    public void SetMoveInput(Vector2 value) {
        _moveInput = value;
    }
    public void SetJumpInput(bool value) {
        _jumpInput = value;
    }
    #endregion

    private void Update() {
        float deltaTime = Time.deltaTime;

        float deltaAcceleration = _acceleration * deltaTime;
        _smothMoveInput.x = Mathf.Lerp(_smothMoveInput.x, _moveInput.x, deltaAcceleration);
        _smothMoveInput.y = Mathf.Lerp(_smothMoveInput.y, _moveInput.y, deltaAcceleration);

        _moveVelocity = (transform.right * _smothMoveInput.x + transform.forward * _smothMoveInput.y) * _moveSpeed;
        _cc.Move(_moveVelocity * deltaTime);

        if (_isGrounded) {
            _verticalVelocity.y = _onGroundGravity;
            if (_jumpInput) {
                _verticalVelocity.y = Mathf.Sqrt(_jumpVelocity * -2 * gravity);
            }
        } else {
            _verticalVelocity.y += gravity * deltaTime;
        }

        _cc.Move(_verticalVelocity * deltaTime);
    }

    private void FixedUpdate() {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundCheckMask);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}
