using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public static UnityEvent<bool> OnPickupItemText = new UnityEvent<bool>();
    public static UnityEvent<bool> OnDropItemText = new UnityEvent<bool>();
}
