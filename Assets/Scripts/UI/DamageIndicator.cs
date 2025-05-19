using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine; //코루틴을 실행시키기 위함

    private void Start()
    {
        CharacterManager.Instance.Player.condition.onTakedamage += Flash; //TakePhysicaldamage를 호출할 때마다 onTakeDamage 델리게이트(Action)도 호출하게 되고, DamageIndicator.cs의 Flash()를 가져온다.
    }
    
    public void Flash() //damageIndicator의 이미지의 Alpha값이 서서히 빠지는 효과를 보여줌.
    {

        if (coroutine != null)//데미지가 한번만 받는 것이 아니기때문에 코루틴이 이미 실행중일 수 있기때문에 기존에 있는 코루틴을 꺼줘야 함.
        {
            StopCoroutine(coroutine);
        }
        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway()); //코루틴에 StartCoroutine을 통해 실행하고 코루틴 반환
    }
    
    private IEnumerator FadeAway() //코루틴을 쓰기위해서는 IEnumerator 키워드 사용해야 함.
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime; //한번 켜져서 빨강색이 보이고 서서히 옅어지게
            image.color = new Color(1f, 100f/ 255f, 100f/255f, a);
            yield return null; //반환값을 정해줘야 코루틴을 정상적으로 사용할 수 있음.
        }

        image.enabled = false;
    }//켜질때마다 연속적으로 설정해놓은 값이 보였다가 없어지게
}
