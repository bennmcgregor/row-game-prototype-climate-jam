using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    public int index = 0;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { drop(); });
    }

    public void drop() {
        GameObject.Find("Inventory").GetComponent<Inv>().dropItem(index);
    }

}
