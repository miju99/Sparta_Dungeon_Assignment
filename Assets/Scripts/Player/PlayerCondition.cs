using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public interface IDamageIbe //�پ��� �������� ���� �� �ִ�(NPC, ����, ��) �͵鿡 �� �������̽��� ���߻���Ͽ� ���
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageIbe
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } } //PlayerCondition�ȿ����� health�� ���� �����͸� Update
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakedamage;

    void Update()
    {
        hunger.Subtract(hunger.passivevalue * Time.deltaTime);
        stamina.Add(stamina.passivevalue * Time.deltaTime);

        if(hunger.curValue < 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if(health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("�׾����ϴ�");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakedamage?.Invoke();
    }
}
