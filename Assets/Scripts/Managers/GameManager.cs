using System;

public class GameManager
{
    public Action OnGoldChanged = null;
    public Action OnStageLvChanged = null;
    long _gold = 0;
    public long Gold
    {
        get { return _gold; }
        set
        {
            if (_gold == value)
                return;
            _gold = value;
            OnGoldChanged?.Invoke();
        }
    }
    int _stageLevel = 1;
    public int StageLevel
    {
        get { return _stageLevel; }
        set
        {
            if (_stageLevel == value)
                return;
            _stageLevel = value;
            OnStageLvChanged?.Invoke();
        }
    }
}