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
    string soldierPath = "soldier";
    string playerPath = "player.json";

    public Dictionary<int, QuestData> QuestData = new Dictionary<int, QuestData>();
    public Dictionary<int, SoldierData> SoldierData = new Dictionary<int, SoldierData>();
    public J_PlayerData playerData;

    // ���� ����� json������ T1 Wrapper�� ������ȭ �� T1 Dictionary ��ȯ
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
    // ���� ����� json ���� ������ȭ
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
    // Resources ���� path��� ��ο� �ִ� ��� ������ Dictionary<int, T>�� ��ȯ�Ͽ� ��ȯ
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
    public void Load()
    {
        QuestData = MakeScriptableObjectDict<QuestData>($"Data/{questPath}", (data) => { return data.Id; });
        SoldierData = MakeScriptableObjectDict<SoldierData>($"Data/{soldierPath}", (data) => { return data.Id; });
        playerData = MakeData<J_PlayerData>($"{dataPath}/{playerPath}");
    }
    public void Save()
    {
        if (!File.Exists($"{dataPath}/{playerPath}")) File.Create($"{dataPath}/{playerPath}");
        File.WriteAllText($"{dataPath}/{playerPath}", JsonConvert.SerializeObject(playerData));
    }
}
