using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


// TODO: Rename this and item to make more sense,
public class Inv : MonoBehaviour
{
    public void OnEnable()
    {
        itemList.Add(new Item(1, "Apple"));
        itemList.Add(new Item(2, "Sword"));
        rebuildDisplay();
    }

    public void Update()
    {
        
    }
    private List<Item> itemList;
    private const int MAX_ITEMS = 10;
    public GameObject itemBase;

    public Inv()
    {
        itemList = new List<Item>();
    }

    public void addItem(Item i)
    {
        if (itemList.Count < MAX_ITEMS)
        {
            itemList.Add(i);
            rebuildDisplay();
        }
    }

    public void OnInventory()
    {
        //setActive(!gameObject.activeSelf);
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
            GameObject cloned = Instantiate(itemBase, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
            cloned.transform.localPosition = Vector3.zero;
            cloned.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = i.name;
        }
    }
}
