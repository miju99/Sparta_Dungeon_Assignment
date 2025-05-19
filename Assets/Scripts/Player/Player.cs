using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //플레이어와 관련된 기능들의 정보 | 플레이어의 정보가 필요한 경우 이 스크립트를 통해 접근할 수 있도록 함.
{
    public PlayerController controller;

    public PlayerCondition condition;
    private void Awake()
    {
        CharacterManager.Instance.Player = this; // Charactermanager 안에 있는 Player에 나 자신 넣기
        controller = GetComponent<PlayerController>(); //PlayerController를 찾아서 controller 안에 넣어줌.
        condition = GetComponent<PlayerCondition>();
    }
}
