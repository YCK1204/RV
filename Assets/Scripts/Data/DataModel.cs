using System.Collections.Generic;

public class J_Soldier
{
    public int Id { get; set; }
    public long Level { get; set; }
    public long Upgrade { get; set; }
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
}