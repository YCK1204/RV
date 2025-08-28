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
}