using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
                {
                    GameObject go = new GameObject("Manager");
                    _instance = go.AddComponent<Manager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    UIManager _ui;
    public static UIManager UI { get { return _instance._ui; } set { _instance._ui = value; } }
    GameManager _game = new GameManager();
    public static GameManager Game { get { return _instance._game; } set { _instance._game = value; } }
    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return _instance._resource; } }
    SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return _instance._scene; } }
}
