using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Item : MonoBehaviour
{
    private Outline _outline;

    private void Awake() {
        _outline = GetComponent<Outline>();
    }

    public void SetOutlineEnable(bool value) { 
        _outline.enabled = value;
    }
}
