using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable, //���� ����
    Consumable, //�Һ� ����
    Resource //�ܼ� �ڿ� ex)��
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
    public float value; //��ŭ ȸ����������
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")] //scriptableObject�� ���� �� ������ ���� �� �ֵ��� �޴�ò�� �߰�
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName; //������ �̸�
    public string description; //������ ����

    public ItemType type; //������ Ÿ�� ex. ��� : ü�� ȸ��, �� : ���� ���ݿ�, ���� : �ڿ� ä��
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack; //���� �� ������ ���� �� �ִ� ���������� �Ǻ�
    public int maxStackAmount; //������ ���� ���� | �󸶳� ���� ������ ���� �� �ִ� ��

    [Header("Consumable")]
    public ItemDataConsumable[] consumables; //����� �Ծ��� �� ü��, ����� �� �� ȸ���� ����
}
