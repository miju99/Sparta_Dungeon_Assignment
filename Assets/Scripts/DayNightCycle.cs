using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;

    public float fullDayLength;
    public float startTime = 0.4f; //0.5�� �� ���� | 0-100%�� 0�ú��� 24�ÿ� ����
    private float timeRate;
    public Vector3 noon; //Vector 90 0 0

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier; //�ݻ�

    void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve) //����� other settings�� ������Ʈ
    {
        float intensity = intensityCurve.Evaluate(time); //intensityCurve�� �����Ǵ� �� ������.

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f; //������ ��ȭ��Ŵ. |[sun�� ��] time�� 0.5�̸� �����̰�, ������ 90�� �Ǿ�� ��. �׷��� 360�� 0.5�� 180���� ����. �׷��� 0.25�� ����. �׷��� ��ü������ 0.25��� ���� ������ �ű⿡ 90�� ���ص� 90�� �ȳ����⶧���� *4���� ���� [moon�� ��] sun�̶� 180�� �ݴ뿡 �׽��Ƿ� 0.5�� ���� 0.75�� ����.
        lightSource.color = gradient.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject; //light�� �� �� �� �ѳ��� �ʿ�� ����.
        if(lightSource.intensity == 0 && go.activeInHierarchy) //0�� �Ǹ� ��Ⱑ ������ �������ٴ� ��. Hierarchyâ���� �����ִ� ����
        {
            go.SetActive(false);
        }
        else if(lightSource.intensity > 0 && ! go.activeInHierarchy) //�����ִ� ���¿��� 0.01 �ö��� �� Hierarchy�� �������� ������,
        {
            go.SetActive(true);
        }
    }
}
