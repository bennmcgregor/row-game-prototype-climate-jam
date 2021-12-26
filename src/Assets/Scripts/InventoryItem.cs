using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Rower Player;
    public Button PickupButton;
    public ProximitySensor ProximitySensor;
    public Rigidbody Rigidbody;

    private void Awake()
    {
        PickupButton.onClick.AddListener(OnPickup);
    }

    private void OnPickup()
    {
        Player.Inventory.OnDropItem += OnDrop;
        Player.Inventory.PickupItem(Rigidbody);
    }

    private void OnDrop()
    {
        ProximitySensor.enabled = true;
        Player.Inventory.OnDropItem -= OnDrop;
    }
}
