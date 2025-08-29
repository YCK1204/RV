using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour
{
    [SerializeField]
    Image Icon;
    [SerializeField]
    TextMeshProUGUI ItemNameText;
    [SerializeField]
    TextMeshProUGUI ItemDescriptionText;
    [SerializeField]
    TextMeshProUGUI CostText;
    [SerializeField]
    Button PurchaseBtn;

    ItemData _itemData;
    public UIInventoryItem InventoryItemPrefab { get { return Manager.UI.UIInventoryItemPrefab; } }
    public void Init(ItemData data)
    {
        _itemData = data;

        ItemNameText.text = data.ItemName;
        ItemDescriptionText.text = data.Description;
        CostText.text = data.Cost.ToString();
        Openable();
    }
    public virtual void Purchase()
    {
        if (Manager.Game.Gold < _itemData.Cost)
            return; // 일단 리턴 나중에 골드 부족 팝업띄워야함
        Manager.Game.Gold -= _itemData.Cost;
        switch (_itemData.ItemType)
        {
            case ItemType.Ally:
                var allyManager = Manager.UI.Ally;
                var ally = Manager.Resource.Instantiate<UIAlly>(allyManager.AllyUI, allyManager.AllyContent);
                Manager.Data.AllyData.TryGetValue(_itemData.Id, out var v);
                if (v == null)
                    return;
                ally.Init(v);

                Manager.UI.Ally.Add(ally);
                break;
            default:
                var item = Manager.Resource.Instantiate<UIInventoryItem>(InventoryItemPrefab, Manager.UI.Inventory.InventoryContent);
                item.Init(_itemData);
                Manager.UI.Inventory.Add(item);
                break;
        }
    }
    public void Openable()
    {
        PurchaseBtn.interactable = Manager.Game.Gold >= _itemData.Cost;
    }
}
