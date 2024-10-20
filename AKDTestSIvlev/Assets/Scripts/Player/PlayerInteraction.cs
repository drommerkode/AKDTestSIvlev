using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private LayerMask _maskLayers;
    [SerializeField] private float _rayLength;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _itemPickupedPosition;
    private Item _itemInHand;
    private Item _itemLoked;
    private bool _lookOnItem;
    private Rigidbody _itemRB;

    [Header("Drop")]
    [SerializeField] private float _dropForwardForce = 250;
    [SerializeField] private float _dropUpForce = 100f;

    private void FixedUpdate() {
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit hit, _rayLength, _maskLayers)) {
            if (hit.transform.TryGetComponent<Item>(out _itemLoked)) {
                if (!_lookOnItem && _itemInHand == null) {
                    EventBus.OnPickupItemText.Invoke(true);
                }
                _lookOnItem = true;
            } else {
                ResetItemLook();
            }
        } else {
            ResetItemLook();
        }
    }

    private void ResetItemLook() {
        if (_lookOnItem) {
            EventBus.OnPickupItemText.Invoke(false); 
        }
        _lookOnItem = false;
    }

    public void PickupItem() {
        if (_itemLoked == null) { return; }
        if (_itemInHand != null) { return; }

        _itemInHand = _itemLoked;

        _itemRB = _itemInHand.GetComponent<Rigidbody>();
        _itemRB.isKinematic = true;
        _itemRB.useGravity = false;

        Transform itemTransform = _itemInHand.transform;
        itemTransform.SetParent(_itemPickupedPosition);
        itemTransform.localRotation = Quaternion.identity;
        itemTransform.localPosition  = Vector3.zero;

        EventBus.OnDropItemText.Invoke(true);
        EventBus.OnPickupItemText.Invoke(false);
    }

    public void DropItem() {
        if (_itemInHand == null) { return; }

        _itemRB.isKinematic = false;
        _itemRB.useGravity = true;
        _itemInHand.transform.SetParent(null);
        _itemRB.AddForce(_camera.forward * _dropForwardForce + _camera.up * _dropUpForce);
        EventBus.OnDropItemText.Invoke(false);
        _itemInHand = null;
    }
}
