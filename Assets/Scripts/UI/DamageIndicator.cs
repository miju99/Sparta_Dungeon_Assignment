using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine; //�ڷ�ƾ�� �����Ű�� ����

    private void Start()
    {
        CharacterManager.Instance.Player.condition.onTakedamage += Flash; //TakePhysicaldamage�� ȣ���� ������ onTakeDamage ��������Ʈ(Action)�� ȣ���ϰ� �ǰ�, DamageIndicator.cs�� Flash()�� �����´�.
    }
    
    public void Flash() //damageIndicator�� �̹����� Alpha���� ������ ������ ȿ���� ������.
    {

        if (coroutine != null)//�������� �ѹ��� �޴� ���� �ƴϱ⶧���� �ڷ�ƾ�� �̹� �������� �� �ֱ⶧���� ������ �ִ� �ڷ�ƾ�� ����� ��.
        {
            StopCoroutine(coroutine);
        }
        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway()); //�ڷ�ƾ�� StartCoroutine�� ���� �����ϰ� �ڷ�ƾ ��ȯ
    }
    
    private IEnumerator FadeAway() //�ڷ�ƾ�� �������ؼ��� IEnumerator Ű���� ����ؾ� ��.
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime; //�ѹ� ������ �������� ���̰� ������ ��������
            image.color = new Color(1f, 100f/ 255f, 100f/255f, a);
            yield return null; //��ȯ���� ������� �ڷ�ƾ�� ���������� ����� �� ����.
        }

        image.enabled = false;
    }//���������� ���������� �����س��� ���� �����ٰ� ��������
}
