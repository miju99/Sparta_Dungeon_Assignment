using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{

    public float curValue;
    public float startValue;
    public float maxValue;
    public float passivevalue; //주기적으로 변하는 값
    public Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        //UI업데이트
        uiBar.fillAmount = GetPercentage();
    }

    float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue); //외부에서 ADD를 호출하면 curValue에 더해짐! | Mathf.Min(A, B); 두 개의 매개변수를 받고, 둘 중 작은 값을 넣어준다.
    }

    public void Subtract(float value)
    {
        curValue = Mathf.Max(curValue - value, 0.0f); //데미지를 받은 경우
    }
}
