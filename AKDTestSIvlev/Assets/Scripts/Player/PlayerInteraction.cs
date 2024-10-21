using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private LayerMask _maskColisions;
    [SerializeField] private LayerMask _maskItems;
    [SerializeField] private float _rayLength;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _itemPickupedPosition;
    private LayerMask _maskColissionsAndItems;
    private GameObject _itemInHand;
    private GameObject _objectLoked;
    private Item _itemLooked;
    private Rigidbody _itemRB;

    [Header("Drop")]
    [SerializeField] private float _dropForwardForce = 250;
    [SerializeField] private float _dropUpForce = 100f;

    private void Awake() {
        _maskColissionsAndItems = _maskColisions + _maskItems;
    }

    private void FixedUpdate() {
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit lookHit, _rayLength, _maskColissionsAndItems)) {
            if (_objectLoked == lookHit.transform.gameObject) { return; }

            ResetItemLook();

            if ((_maskItems & (1 << lookHit.transform.gameObject.layer)) == 0) { return; }
            _objectLoked = lookHit.transform.gameObject;

            if (_itemInHand == null) {
                _itemLooked = _objectLoked.GetComponent<Item>();
                _itemLooked.SetOutlineEnable(true);
                EventBus.OnPickupItemText.Invoke(true);
            }
            
        } else {
            ResetItemLook();
        }
    }

    private void ResetItemLook() {
        if (_objectLoked == null) { return; }
        EventBus.OnPickupItemText.Invoke(false);
        _itemLooked?.SetOutlineEnable(false);
        _objectLoked = null;
        _itemLooked = null;
    }

    public void PickupItem() {
        if (_objectLoked == null) { return; }
        if (_itemInHand != null) { return; }

        _itemInHand = _objectLoked;

        _itemRB = _itemInHand.GetComponent<Rigidbody>();
        _itemRB.isKinematic = true;
        _itemRB.useGravity = false;

        Transform itemTransform = _itemInHand.transform;
        itemTransform.SetParent(_itemPickupedPosition);
        itemTransform.localRotation = Quaternion.identity;
        itemTransform.localPosition  = Vector3.zero;

        EventBus.OnDropItemText.Invoke(true);
        ResetItemLook();
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
