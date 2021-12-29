using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Action OnDropItem;

    public Transform ItemHoldingPosition;
    public Button DropItemButton;
    public float ThrowPower = 200f; // power is scaled proportional to the mass of the object being thrown

    private Rigidbody _itemBeingHeld = null;

    private void Awake()
    {
        DropItemButton.onClick.AddListener(() => DropItem());
    }

    public void PickupItem(Rigidbody Item)
    {
        if (_itemBeingHeld == null)
        {
            Item.transform.SetParent(ItemHoldingPosition, false);
            _itemBeingHeld = Item;
            _itemBeingHeld.transform.localPosition = Vector3.zero;
            _itemBeingHeld.transform.localRotation = Quaternion.identity;
            _itemBeingHeld.isKinematic = true;
            DropItemButton.gameObject.SetActive(true);
        }
    }

    private void DropItem()
    {
        _itemBeingHeld.transform.parent = null;
        Vector3 direction = Quaternion.Euler(360f - Camera.main.transform.eulerAngles.x, 0, 0) * transform.forward; 
        Vector3 throwForce = direction * _itemBeingHeld.mass * ThrowPower;
        _itemBeingHeld.isKinematic = false;
        _itemBeingHeld.AddForce(throwForce);
        _itemBeingHeld = null;
    
        DropItemButton.gameObject.SetActive(false);
        
        OnDropItem?.Invoke();
    }
}
