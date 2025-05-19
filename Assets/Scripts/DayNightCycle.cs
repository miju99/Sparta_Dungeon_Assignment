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
    public float startTime = 0.4f; //0.5일 때 정오 | 0-100%를 0시부터 24시에 대입
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
    public AnimationCurve reflectionIntensityMultiplier; //반사

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

    void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve) //조명과 other settings을 업데이트
    {
        float intensity = intensityCurve.Evaluate(time); //intensityCurve에 보관되는 값 가져옴.

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4f; //각도를 변화시킴. |[sun일 때] time이 0.5이면 정오이고, 각도가 90이 되어야 함. 그런데 360에 0.5면 180도가 나옴. 그래서 0.25를 빼줌. 그러면 전체적으로 0.25라는 값이 나오고 거기에 90을 곱해도 90이 안나오기때문에 *4까지 해줌 [moon일 때] sun이랑 180도 반대에 잉스므로 0.5를 더해 0.75를 빼줌.
        lightSource.color = gradient.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject; //light를 두 개 다 켜놓을 필요는 없음.
        if(lightSource.intensity == 0 && go.activeInHierarchy) //0이 되면 밝기가 완전히 없어졌다는 것. Hierarchy창에는 켜져있는 상태
        {
            go.SetActive(false);
        }
        else if(lightSource.intensity > 0 && ! go.activeInHierarchy) //꺼져있는 상태에서 0.01 올라갔을 때 Hierarchy에 켜져있지 않으면,
        {
            go.SetActive(true);
        }
    }
}
