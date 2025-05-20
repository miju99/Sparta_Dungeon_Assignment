using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; //카메라를 기준으로 Ray를 쏠 때 얼마나 자주 Update해서 검출을 할 지
    private float lastCheckTime;
    public float maxCheckDistance; //얼마나 멀리

    public LayerMask layerMask; //어떤 레이어가 달린 게임오브젝트를 추출할 지

    public GameObject curInteractGameObject; //Interact에 성공했다면, Interact된 게임 오브젝트의 정보를 가지고 있음.
    private IInteractable curInteractable; //캐싱하는 자료가 담겨져 있음.

    public TextMeshProUGUI promptText;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {

            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); //카메라 기준으로 스크린의 중앙에서 쏘는 Ray
            RaycastHit hit; //부딪힌 정보를 담음.

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    //프롬포트에 출력
                    SetPromptText();
                }
            }
            else
            {
                //빈 공간에 ray를 계속 쏘는 경우
                curInteractable = null;
                curInteractGameObject = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null) //context.phase가 눌리고, 캐싱하고 있는 정보가 있을 때
        {
            curInteractable.Oninteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
