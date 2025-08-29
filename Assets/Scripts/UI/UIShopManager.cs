using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopManager : MonoBehaviour, IFooter
{
    public UIShopItem UIShopItemPrefab { get { return Manager.UI.UIShopItemPrefab; } }
    public List<UIShopItem> ShopItems = new List<UIShopItem>();
    public RectTransform ShopContent { get { return Manager.UI.ShopContent; } }

    private void Start()
    {
        Init();
    }
    public void Enter()
    {
        ShopContent.gameObject.SetActive(true);
    }

    public void Exit()
    {
        ShopContent.gameObject.SetActive(false);
    }

    public void Init()
    {
        Manager.Game.OnGoldChanged += OnGoldChanged;

        var itemList = Manager.Data.ItemData;
        foreach (var item in itemList)
        {
            var ui = Manager.Resource.Instantiate<UIShopItem>(UIShopItemPrefab, ShopContent);
            ui.Init(item);
            ShopItems.Add(ui);
        }
    }
    void OnGoldChanged()
    {
        foreach (var item in ShopItems)
            item.Openable();
    }
}
