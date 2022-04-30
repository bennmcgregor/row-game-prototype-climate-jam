using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// TODO: Rename this and item to make more sense,
public class Inv : MonoBehaviour
{
    private bool isOpen = false;
    public GameObject InventoryCanvas;
    public void OnEnable()
    {
        rebuildDisplay();
        InventoryCanvas.SetActive(isOpen);
    }

    private List<Item> itemList;
    private const int MAX_ITEMS = 10;
    public GameObject itemBase;

    private void Awake()
    {
        // retain state across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    public Inv()
    {
        itemList = new List<Item>();
    }

    public void addItem(Item i)
    {
        if (itemList.Count < MAX_ITEMS)
        {
            itemList.Add(i);
        }
    }

    public void OnInventory()
    {
        isOpen = !isOpen;
        InventoryCanvas.SetActive(isOpen);
        if (isOpen)
        {
            rebuildDisplay();
        }
    }

    public void dropItem(int i)
    {
        itemList.RemoveAt(i);
        rebuildDisplay();
    }

    public void rebuildDisplay()
    {
        foreach (Transform child in GameObject.Find("Content").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Item i in itemList)
        {
            Debug.Log(i.name);
            GameObject cloned = Instantiate(itemBase, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
            cloned.transform.localPosition = Vector3.zero;
            cloned.transform.Find("Image").GetComponent<Image>().sprite = i.sprite;
        }
    }
}
