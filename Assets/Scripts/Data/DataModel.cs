using System.Collections.Generic;

public class J_Soldier
{
    public int Id { get; set; }
    public int CharId { get; set; }
    public long Level { get; set; }
    public long Upgrade { get; set; }
    public SoldierController Clone()
    {
        Manager.Data.SoldierData.TryGetValue(Id, out SoldierData data);
        if (data == null)
            return null;
        var sc = Manager.Resource.Instantiate<SoldierController>(data.Prefab);
        sc.Init(Level, Upgrade, data);
        return sc;
    }
}
public class J_SoldierWrapper
{
    public List<J_Soldier> Soldiers { get; set; } = new List<J_Soldier>();
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
    public J_SoldierWrapper Soldiers { get; set; } = new J_SoldierWrapper();
    public J_QuestWrapper Quests { get; set; } = new J_QuestWrapper();
}