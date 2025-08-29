using System.Collections.Generic;

public class J_Ally
{
    public int Id { get; set; }
    public int CharId { get; set; }
    public long Level { get; set; }
    public long Upgrade { get; set; }
    public AllyController Clone()
    {
        Manager.Data.AllyData.TryGetValue(CharId, out AllyData data);
        if (data == null)
            return null;
        var sc = Manager.Resource.Instantiate<AllyController>(data.Prefab);
        sc.Init(Level, Upgrade, data);
        return sc;
    }
}
public class J_AllyWrapper
{
    public List<J_Ally> Allys { get; set; } = new List<J_Ally>();
}
public class J_Quest
{
    public int Id { get; set; }
    public long Level { get; set; }
}
public class J_QuestWrapper
{
    public List<J_Quest> Quests { get; set; } = new List<J_Quest>();
}
public class J_PlayerData
{
    public int Stage { get; set; }
    public long Gold { get; set; }
    public J_AllyWrapper Allys { get; set; } = new J_AllyWrapper();
    public J_QuestWrapper Quests { get; set; } = new J_QuestWrapper();
    public J_Inventory Inventory { get; set; } = new J_Inventory();
}
public class J_Inventory
{
    public List<J_Item> Items { get; set; } = new List<J_Item>();
}
public class J_Item
{
    public int Id { get; set; }
    // 0 병사
    // 1 배속 아이템
    // 2 능력치 아이템
    // 3 유물 아이템
    public ItemType ItemType { get; set; }
}

public class J_ItemWrapper
{
    public List<J_Item> Items { get; set; } = new List<J_Item>();
}
public class J_GameShop
{
    public J_ItemWrapper Items { get; set; } = new J_ItemWrapper();
}