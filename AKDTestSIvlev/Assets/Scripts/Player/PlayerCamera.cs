using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector2 _lookInput;

    [SerializeField] private float _lookSens = 10f;
    [SerializeField] private float _lookMinClamp = -90;
    [SerializeField] private float _lookMaxClamp = 90;
    [SerializeField] private Transform _lookHolder;
    private float verticalRotation;

    public void SetLookInput(Vector2 value) {
        _lookInput = value;
    }

    private void Update() {
        float deltatime = Time.deltaTime;

        transform.Rotate(Vector3.up, _lookInput.x * _lookSens * deltatime);
        verticalRotation -= _lookInput.y * _lookSens * deltatime;
        verticalRotation = Mathf.Clamp(verticalRotation, _lookMinClamp, _lookMaxClamp);
        _lookHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
