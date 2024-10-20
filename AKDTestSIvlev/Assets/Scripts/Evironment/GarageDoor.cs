using DG.Tweening;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    private BoxCollider _bc;

    private void Start() {
        _bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        _bc.enabled = false;
        transform.DORotate(new Vector3(0f, 0f, 90f), 0.8f);
        transform.DOLocalMoveX(0f, 1.6f);
    }
}
