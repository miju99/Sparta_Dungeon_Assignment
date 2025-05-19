using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    List<IDamageIbe> things = new List<IDamageIbe>(); //Trigger가 Enter/Exit될 때마다 리스트에 넣어주고, 빼주면 DealDamage에서는 things에 들어가있는 객체에 OnTakedamage 함수를 실행

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
