using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //�÷��̾�� ���õ� ��ɵ��� ���� | �÷��̾��� ������ �ʿ��� ��� �� ��ũ��Ʈ�� ���� ������ �� �ֵ��� ��.
{
    public PlayerController controller;

    public PlayerCondition condition;
    private void Awake()
    {
        CharacterManager.Instance.Player = this; // Charactermanager �ȿ� �ִ� Player�� �� �ڽ� �ֱ�
        controller = GetComponent<PlayerController>(); //PlayerController�� ã�Ƽ� controller �ȿ� �־���.
        condition = GetComponent<PlayerCondition>();
    }
}
