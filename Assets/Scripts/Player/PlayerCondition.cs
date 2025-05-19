using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public interface IDamageIbe //다양한 데미지를 받을 수 있는(NPC, 몬스터, 등) 것들에 이 인터페이스를 다중상속하여 사용
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamageIbe
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } } //PlayerCondition안에서는 health를 통해 데이터를 Update
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
        Debug.Log("죽었슴니당");
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakedamage?.Invoke();
    }
}
