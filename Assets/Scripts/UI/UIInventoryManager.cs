using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour, IFooter
{
    public UIInventoryItem UIInventoryItemPrefab { get { return Manager.UI.UIInventoryItemPrefab; } }
    public List<UIInventoryItem> InventoryItems = new List<UIInventoryItem>();
    public RectTransform InventoryContent { get { return Manager.UI.InventoryContent; } }

    private void Start()
    {
        Init();
    }
    public void Enter()
    {
        InventoryContent.gameObject.SetActive(true);
    }

    public void Exit()
    {
        InventoryContent.gameObject.SetActive(false);
    }

    public void Init()
    {
        List<J_Item> items = Manager.Data.playerData.Inventory.Items;
        foreach (J_Item item in items)
        {
            UIInventoryItem uiItem = Manager.Resource.Instantiate<UIInventoryItem>(UIInventoryItemPrefab, InventoryContent);
            ItemData itemData = Manager.Data.ItemData.Where(i => i.Id == item.Id && i.ItemType == item.ItemType).FirstOrDefault();
            if (itemData == null)
                continue;
            uiItem.Init(itemData);
            InventoryItems.Add(uiItem);
        }
    }
    public void Add(UIInventoryItem item)
    {
        if (InventoryItems.Contains(item))
            return;
        InventoryItems.Add(item);
        J_Item ji = new J_Item()
        {
            Id = item.Id,
            ItemType = item.ItemType,
        };
        Manager.Data.playerData.Inventory.Items.Add(ji);
        Manager.Data.Save();
    }
    public void Used(UIInventoryItem item)
    {
        if (!InventoryItems.Contains(item))
            return;
        InventoryItems.Remove(item);
        J_Item ji = Manager.Data.playerData.Inventory.Items.Where(i => i.Id == item.Id && i.ItemType == item.ItemType).FirstOrDefault();
        if (ji != null)
        {
            Manager.Data.playerData.Inventory.Items.Remove(ji);
            Manager.Data.Save();
        }
        Destroy(item.gameObject);
    }
}
