using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pickUpItemText;
    [SerializeField] private GameObject _dropItemText;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        EventBus.OnPickupItemText.AddListener(PickupItemText); 
        EventBus.OnDropItemText.AddListener(DropItemText);
        _pickUpItemText.SetActive(false);
        _dropItemText.SetActive(false);
    }

    private void PickupItemText(bool value) {
        _pickUpItemText.SetActive(value);
    }

    private void DropItemText(bool value) {
        _dropItemText.SetActive(value);
    }
}
