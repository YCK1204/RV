using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerEx
{
    public T Instantiate<T>(T prefab) where T : Object
    {
        return Object.Instantiate(prefab);
    }
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }
    public void Destroy(Object obj)
    {
        Object.Destroy(obj);
    }
}
