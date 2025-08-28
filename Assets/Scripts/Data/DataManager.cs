using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager
{
    string dataPath = Application.dataPath + "/Resources/Data";
    string questPath = "quest";
    string allyPath = "ally";
    string enemyPath = "enemy";
    string playerPath = "player.json";

    public Dictionary<int, QuestData> QuestData = new Dictionary<int, QuestData>();
    public Dictionary<int, AllyData> AllyData = new Dictionary<int, AllyData>();
    public Dictionary<int, EnemyData> EnemyData = new Dictionary<int, EnemyData>();
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
        Manager.Game.OnGoldChanged += () => { playerData.Gold = Manager.Game.Gold; Save(); };
        Manager.Game.OnStageLvChanged += () => { playerData.Stage = Manager.Game.StageLevel; Save(); };
    }
    public void Load()
    {
        QuestData = MakeScriptableObjectDict<QuestData>($"Data/{questPath}", (data) => { return data.Id; });
        AllyData = MakeScriptableObjectDict<AllyData>($"Data/{allyPath}", (data) => { return data.Id; });
        EnemyData = MakeScriptableObjectDict<EnemyData>($"Data/{enemyPath}", (data) => { return data.Id; });
        playerData = MakeData<J_PlayerData>($"{dataPath}/{playerPath}");
    }
    public void Save()
    {
        if (!File.Exists($"{dataPath}/{playerPath}")) File.Create($"{dataPath}/{playerPath}");
        File.WriteAllText($"{dataPath}/{playerPath}", JsonConvert.SerializeObject(playerData));
    }
}
