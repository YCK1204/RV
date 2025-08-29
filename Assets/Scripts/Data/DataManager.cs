using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : IManager
{
    string dataPath = Application.dataPath + "/Resources/Data";
    string questPath = "quest";
    string allyPath = "ally";
    string enemyPath = "enemy";
    string playerPath = "player.json";
    string itemPath = "item";
    string relicItemPath = "relicItem";
    string doubleSpeedItemPath = "doubleSpeedItem";
    string abilityItemPath = "abilityItem";
    string allyItemPath = "allyItem";

    public Dictionary<int, QuestData> QuestData = new Dictionary<int, QuestData>();
    public Dictionary<int, AllyData> AllyData = new Dictionary<int, AllyData>();
    public Dictionary<int, EnemyData> EnemyData = new Dictionary<int, EnemyData>();

    Dictionary<int, RelicItemData> _relicItemData = new Dictionary<int, RelicItemData>();
    Dictionary<int, DoubleSpeedItemData> _doubleSpeedItemData = new Dictionary<int, DoubleSpeedItemData>();
    Dictionary<int, AbilityItemData> _abilityItemData = new Dictionary<int, AbilityItemData>();
    Dictionary<int, AllyItemData> _allyItemData = new Dictionary<int, AllyItemData>();
    public List<ItemData> ItemData = new List<ItemData>();
    public J_PlayerData playerData;

    // 절대 경로의 json파일을 T1 Wrapper로 데이터화 후 T1 Dictionary 변환
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
    // 절대 경로의 json 파일 역직렬화
    T MakeData<T>(string path) where T : class, new()
    {
        T result = null;
        try
        {
            var json = File.ReadAllText(path);
            result = JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception e)
        {
            Debug.LogError($"DataManager Load Failed to {path}");
        }
        if (result == null)
            result = new T();
        return result;
    }
    // Resources 기준 path상대 경로에 있는 모든 파일을 Dictionary<int, T>로 변환하여 반환
    Dictionary<int, T> MakeScriptableObjectDict<T>(string path, Func<T, int> extractIdentifier) where T : ScriptableObject
    {
        Dictionary<int, T> dict = new Dictionary<int, T>();

        var allData = Manager.Resource.LoadAll<T>(path);

        foreach (var data in allData)
        {
            var id = extractIdentifier(data);
            if (!dict.TryAdd(id, data))
                Debug.LogError($"Overlapped Quest Data {data.name}");
        }
        return dict;
    }
    public void Init()
    {
        Load();
        InitItemData();
        Manager.Game.OnGoldChanged += OnGoldChanged;
        Manager.Game.OnStageLvChanged += OnStageLvChanged;
    }
    void InitItemData()
    {
        foreach (var item in _relicItemData)
            ItemData.Add(item.Value);
        foreach (var item in _doubleSpeedItemData)
            ItemData.Add(item.Value);
        foreach (var item in _abilityItemData)
            ItemData.Add(item.Value);
        foreach (var item in _allyItemData)
            ItemData.Add(item.Value);
        ItemData.Sort((a, b) =>
        {
            if (a.ItemType != b.ItemType)
                return a.ItemType.CompareTo(b.ItemType);
            return a.Id.CompareTo(b.Id);
        });
    }
    public void Load()
    {
        QuestData = MakeScriptableObjectDict<QuestData>($"Data/{questPath}", (data) => { return data.Id; });
        AllyData = MakeScriptableObjectDict<AllyData>($"Data/{allyPath}", (data) => { return data.Id; });
        EnemyData = MakeScriptableObjectDict<EnemyData>($"Data/{enemyPath}", (data) => { return data.Id; });
        playerData = MakeData<J_PlayerData>($"{dataPath}/{playerPath}");
        _relicItemData = MakeScriptableObjectDict<RelicItemData>($"Data/{itemPath}/{relicItemPath}", (data) => { return data.Id; });
        _doubleSpeedItemData = MakeScriptableObjectDict<DoubleSpeedItemData>($"Data/{itemPath}/{doubleSpeedItemPath}", (data) => { return data.Id; });
        _abilityItemData = MakeScriptableObjectDict<AbilityItemData>($"Data/{itemPath}/{abilityItemPath}", (data) => { return data.Id; });
        _allyItemData = MakeScriptableObjectDict<AllyItemData>($"Data/{itemPath}/{allyItemPath}", (data) => { return data.Id; });
    }
    public void Save()
    {
        if (!File.Exists($"{dataPath}/{playerPath}")) File.Create($"{dataPath}/{playerPath}");
        File.WriteAllText($"{dataPath}/{playerPath}", JsonConvert.SerializeObject(playerData));
    }
    public void OnGoldChanged()
    {
        playerData.Gold = Manager.Game.Gold;
        Save();
    }
    public void OnStageLvChanged()
    {
        playerData.Stage = Manager.Game.StageLevel;
        Save();
    }
}
