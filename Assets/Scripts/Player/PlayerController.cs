using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("MoveMent")]

    public float moveSpeed; //스피드
    public float jumpPower;

    public Vector2 curMovementInput; //Input Action에서 받아올 값들을 받아줄 곳

    public LayerMask groundLayerMask;

    [Header("Look")]

    public Transform cameraContainer;

    public float minXLook; //x의 최소 회전 범위
    public float maxXLook; //x의 최대 회전 범위

    private float camCurXRot; //InputAction에서 mouse의 delta값을 camCurXRot에 받음

    public float lookSensitivity; //회전 민감도

    private Vector2 mouseDelta; //마우스 변화값

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //마우스 커서가 보이지 않게
    }

    private void FixedUpdate() //물리연산을 하는 경우 FixedUpdate로 호출하는 것이 좋음.
    {
        Move(); //키 값이 떼어지면 curMovementInput이 Vector0가 되기 때문에 모두 0값이 들어가며, direction값도 0이 됨으로 Move가 호출되어도 플레이어는 움직이지 않음.
    }

    private void LateUpdate() //모든 연산이 끝난 후 카메라 동작
    {
        CameraLook();
    }

    private void Move() //실제로 움직이는 부분
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //forawrd : 앞,뒤로 이동 = W,S값(curMoveMentInput값에서 y에 해당) | right : 좌,우로 이동 = A,S값
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; //점프를 했을 때만 위,아래로 움직여야 하므로

        _rigidbody.velocity = dir; //방향
    }

    private void CameraLook() //회전값에 따른 카메라 회전
    {
        camCurXRot += mouseDelta.y * lookSensitivity; // 돌려줄 값 = 마우스 y값 * 민감도 | 마우스를 좌,우로 움직이면 mousedelta.x값이 바뀌는데, 캐릭터가 좌,우로 움직이려면 y축을 돌려줘야 하기 때문에 실제로 받는 값에서 x값을 y에, y값을 x에 넣어줘야 함.
        //x값에 y를 넣어준 것.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);//최소값과 최대값이 넘어가지 않도록 설정 | 최소값보다 작아지면 최소값 반환, 최대값보다 커지면 최대값 반환
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); //카메라 컨테이너의 좌표를 local좌표로 돌려준다. | mouseDelta값의 y값이 마우스를 아래로 드래그할 시 -가 된다. Rotation값을 돌려보면 알 수 있음! 마우스를 실제로 동작하는 것과 보여지는 값이 반대이기 때문에 부호를 바꿔줌.

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); //위, 아래는 캐릭터 각도를 회전 | 마우스를 좌,우로 움직이면 mousedelta.x값이 바뀌는데, 캐릭터가 좌,우로 움직이려면 y축을 돌려줘야 하기 때문에 실제로 받는 값에서 x값을 y에, y값을 x에 넣어줘야 함.
        //y값에 x를 넣어준 것.
    }

    public void OnMove(InputAction.CallbackContext context) //현재 상태를 받아옴.
    {
        if (context.phase == InputActionPhase.Performed) //start : 눌렸을 때 | Performed : 눌리고 나서 내부 로직이 실행이 됐을 때 | Cancled : 취소됐을 때
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled) //키가 떨어졌을 때
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //마우스는 계속해서 값이 유지되므로 (눌리거나 취소 이런 상황이 없어서) 값만 읽어오면 됨
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); //순간적으로 확 올라가기때문에 Mode -> Impulse Mode 사용
        }
    }

    bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up* 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up* 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up* 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up* 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
