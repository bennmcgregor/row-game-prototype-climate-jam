using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public Sprite sprite;

    public Item(int id, string name, Sprite sprite)
    {
        this.id = id;
        this.name = name;
        this.sprite = sprite;
    }
}
