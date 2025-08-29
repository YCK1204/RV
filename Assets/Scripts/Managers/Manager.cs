using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IManager
{
    public void Init();
}
public class Manager : MonoBehaviour
{
    static Manager _instance;
    static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Manager>();
                if (_instance == null)
                    Init();
            }
            return _instance;
        }
    }
    private void Start()
    {
        Data.Init();
        Spawn.Init();
        UI.Init();
    }

    UIManager _ui;
    public static UIManager UI { get { return Instance._ui; } set { Instance._ui = value; } }
    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } set { Instance._game = value; } }
    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }
    SpawnManager _spawn;
    public static SpawnManager Spawn { get { return Instance._spawn; } set { Instance._spawn = value; } }
    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }
    static void Init()
    {
        GameObject go = new GameObject("Manager");
        _instance = go.AddComponent<Manager>();
        DontDestroyOnLoad(_instance);
    }
}
