using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public void Destroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }
    public T Instantiate<T>(T prefab, Transform parent = null) where T : Component
    {
        var go = GameObject.Instantiate(prefab.gameObject);
        go.transform.SetParent(parent);
        return go.GetComponent<T>();
    }
}
