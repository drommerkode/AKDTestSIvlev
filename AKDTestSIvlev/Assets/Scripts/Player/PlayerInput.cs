using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    private InputSystem_Actions _input;

    private PlayerMovement _playerMovement;
    private PlayerCamera _playerCamera;
    private PlayerInteraction _playerInteraction;

    private void Awake() {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCamera = GetComponent<PlayerCamera>();
        _playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void OnEnable() {
        _input = new InputSystem_Actions();
        _input.Enable();

        _input.Player.Move.performed += SetMove;
        _input.Player.Move.canceled += SetMove;

        _input.Player.Look.performed += SetLook;
        _input.Player.Look.canceled += SetLook;

        _input.Player.Jump.started += SetJump;
        _input.Player.Jump.canceled += SetJump;

        _input.Player.PickUpItem.started += SetPickUpItem;

        _input.Player.DropItem.started += SetDropItem;
    }

    private void OnDisable() {
        _input.Player.Move.performed -= SetMove;
        _input.Player.Move.canceled -= SetMove;

        _input.Player.Look.performed -= SetLook;
        _input.Player.Look.canceled -= SetLook;

        _input.Player.Jump.started -= SetJump;
        _input.Player.Jump.canceled -= SetJump;

        _input.Player.PickUpItem.started -= SetPickUpItem;

        _input.Player.DropItem.started -= SetDropItem;

        _input.Disable();
    }

    private void SetMove(InputAction.CallbackContext ctx) {
        _playerMovement.SetMoveInput(ctx.ReadValue<Vector2>());
    }

    private void SetLook(InputAction.CallbackContext ctx) {
        _playerCamera.SetLookInput(ctx.ReadValue<Vector2>());
    }

    private void SetJump(InputAction.CallbackContext ctx) {
        _playerMovement.SetJumpInput(ctx.started);
    }

    private void SetPickUpItem(InputAction.CallbackContext ctx) {
        _playerInteraction.PickUpItem();
    }

    private void SetDropItem(InputAction.CallbackContext ctx) {
        _playerInteraction.DropItem();
    }
}
