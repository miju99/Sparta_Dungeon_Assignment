using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable, //장착 가능
    Consumable, //소비 가능
    Resource //단순 자원 ex)돌
}

public enum ConsumeableType
{
    Health,
    Hunger
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumeableType type;
    public float value; //얼만큼 회복시켜줄지
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")] //scriptableObject를 만들 때 빠르게 만들 수 있도록 메뉴챵에 추가
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName; //아이템 이름
    public string description; //아이템 설명

    public ItemType type; //아이템 타입 ex. 당근 : 체력 회복, 검 : 몬스터 공격용, 도끼 : 자원 채집
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack; //여러 개 가지고 있을 수 있는 아이템인지 판별
    public int maxStackAmount; //아이템 소지 개수 | 얼마나 많이 가지고 있을 수 있는 지

    [Header("Consumable")]
    public ItemDataConsumable[] consumables; //당근을 먹었을 때 체력, 배고픔 둘 다 회복도 가능
}
