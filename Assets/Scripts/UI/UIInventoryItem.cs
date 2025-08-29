using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    Image Icon;
    [SerializeField]
    TextMeshProUGUI ItemNameText;
    [SerializeField]
    TextMeshProUGUI ItemDescriptionText;

    int _id;
    public int Id { get { return _id; } }
    public ItemType _itemType;
    public ItemType ItemType { get { return _itemType; } }
    public void Use()
    {
        Manager.UI.Inventory.Used(this);
    }

    public void Init(ItemData itemData)
    {
        ItemNameText.text = itemData.ItemName;
        ItemDescriptionText.text = itemData.Description;
        _id = itemData.Id;
        _itemType = itemData.ItemType;
    }
}
