using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public enum SaveType
{
    Quest,
    Soldier,
    Player,
    All
}
public class DataManager
{
    string dataPath = Application.dataPath + "/Resources/Data";
    string questPath = "quest.json";
    string soldierPath = "soldier.json";
    string playerPath = "player.json";

    public Dictionary<int, J_Quest> questData = new Dictionary<int, J_Quest>();
    public Dictionary<int, J_Soldier> soldierData = new Dictionary<int, J_Soldier>();
    public J_PlayerData playerData = new J_PlayerData();
    // 퀘스트 진행상황
    // 골드
    // 병사
    // 스테이지
    Dictionary<int, T1> MakeDict<T1, T2>(string path, Func<T2, Dictionary<int, T1>> factory) where T2 : class
    {
        try
        {
            var json = File.ReadAllText(path);
            var wrapper = JsonConvert.DeserializeObject<T2>(json);
            return factory(wrapper);
        }
        catch (Exception e)
        {
            Debug.LogError($"DataManager Load Failed to {path}");
        }
        return new Dictionary<int, T1>();
    }
    T MakeData<T>(string path) where T : class
    {
        try
        {
            var json = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"DataManager Load Failed to {path}");
        }
        return null;
    }
    public void Load()
    {
        questData = MakeDict<J_Quest, J_QuestWrapper>($"{dataPath}/{questPath}", (wrapper) =>
        {
            var dict = new Dictionary<int, J_Quest>();
            foreach (var item in wrapper.Quests)
                dict.Add(item.Id, item);
            return dict;
        });
        soldierData = MakeDict<J_Soldier, J_SoldierWrapper>($"{dataPath}/{soldierPath}", (wrapper) =>
        {
            var dict = new Dictionary<int, J_Soldier>();
            foreach (var item in wrapper.Soldiers)
                dict.Add(item.Id, item);
            return dict;
        });
        playerData = MakeData<J_PlayerData>($"{dataPath}/{playerPath}");
    }
    public void Save(SaveType type)
    {
        switch (type)
        {
            case SaveType.Quest:
                if (!File.Exists($"{dataPath}/{questPath}"))
                    File.Create($"{dataPath}/{questPath}");
                File.WriteAllText($"{dataPath}/{questPath}", JsonConvert.SerializeObject(new J_QuestWrapper() { Quests = new List<J_Quest>(questData.Values) }));
                return;
            case SaveType.Soldier:
                if (!File.Exists($"{dataPath}/{soldierPath}"))
                    File.Create($"{dataPath}/{soldierPath}");
                File.WriteAllText($"{dataPath}/{soldierPath}", JsonConvert.SerializeObject(new J_SoldierWrapper() { Soldiers = new List<J_Soldier>(soldierData.Values) }));
                break;
            case SaveType.Player:
                if (!File.Exists($"{dataPath}/{playerPath}"))
                    File.Create($"{dataPath}/{playerPath}");
                File.WriteAllText($"{dataPath}/{playerPath}", JsonConvert.SerializeObject(playerData));
                break;
            default:
                if (!File.Exists($"{dataPath}/{playerPath}")) File.Create($"{dataPath}/{playerPath}");
                if (!File.Exists($"{dataPath}/{questPath}")) File.Create($"{dataPath}/{questPath}");
                if (!File.Exists($"{dataPath}/{soldierPath}")) File.Create($"{dataPath}/{soldierPath}");
                File.WriteAllText($"{dataPath}/{questPath}", JsonConvert.SerializeObject(new J_QuestWrapper() { Quests = new List<J_Quest>(questData.Values) }));
                File.WriteAllText($"{dataPath}/{soldierPath}", JsonConvert.SerializeObject(new J_SoldierWrapper() { Soldiers = new List<J_Soldier>(soldierData.Values) }));
                File.WriteAllText($"{dataPath}/{playerPath}", JsonConvert.SerializeObject(playerData));
                break;
        }
    }

}
