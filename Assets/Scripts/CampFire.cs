using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    List<IDamageIbe> things = new List<IDamageIbe>(); //Trigger�� Enter/Exit�� ������ ����Ʈ�� �־��ְ�, ���ָ� DealDamage������ things�� ���ִ� ��ü�� OnTakedamage �Լ��� ����

    void Start()
    {
        InvokeRepeating("Dealdamage", 0, damageRate);
    }


    void Dealdamage()
    {
        for(int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageIbe damagalbe))
        {
            things.Add(damagalbe);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageIbe damagalbe))
        {
            things.Remove(damagalbe);
        }
    }
}
