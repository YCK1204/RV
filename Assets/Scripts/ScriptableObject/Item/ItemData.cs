using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    public void Use();
}
public enum ItemType
{    // 0 ����
    // 1 ��� ������
    // 2 �ɷ�ġ ������
    // 3 ���� ������
    Ally = 0,
    DoubleSpeed = 1,
    Ability = 2,
    Relic = 3,
}
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int id;
    public int Id { get { return id; } }
    [SerializeField]
    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } }
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField]
    private int cost;
    public int Cost { get { return cost; } }
    [SerializeField]
    private string description;
    public string Description { get { return description; } }
    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get { return icon; } }
}
