using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Action OnGoldChanged = null;
    long _gold = 0;
    public long Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            OnGoldChanged?.Invoke();
        }
    }
    long _lv = 1;
    public long lv
    {
        get { return _lv; }
        set
        {
            _lv = value;
        }
    }
}
