using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("MoveMent")]

    public float moveSpeed; //���ǵ�
    public float jumpPower;

    public Vector2 curMovementInput; //Input Action���� �޾ƿ� ������ �޾��� ��

    public LayerMask groundLayerMask;

    [Header("Look")]

    public Transform cameraContainer;

    public float minXLook; //x�� �ּ� ȸ�� ����
    public float maxXLook; //x�� �ִ� ȸ�� ����

    private float camCurXRot; //InputAction���� mouse�� delta���� camCurXRot�� ����

    public float lookSensitivity; //ȸ�� �ΰ���

    private Vector2 mouseDelta; //���콺 ��ȭ��

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ���� ������ �ʰ�
    }

    private void FixedUpdate() //���������� �ϴ� ��� FixedUpdate�� ȣ���ϴ� ���� ����.
    {
        Move(); //Ű ���� �������� curMovementInput�� Vector0�� �Ǳ� ������ ��� 0���� ����, direction���� 0�� ������ Move�� ȣ��Ǿ �÷��̾�� �������� ����.
    }

    private void LateUpdate() //��� ������ ���� �� ī�޶� ����
    {
        CameraLook();
    }

    private void Move() //������ �����̴� �κ�
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //forawrd : ��,�ڷ� �̵� = W,S��(curMoveMentInput������ y�� �ش�) | right : ��,��� �̵� = A,S��
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; //������ ���� ���� ��,�Ʒ��� �������� �ϹǷ�

        _rigidbody.velocity = dir; //����
    }

    private void CameraLook() //ȸ������ ���� ī�޶� ȸ��
    {
        camCurXRot += mouseDelta.y * lookSensitivity; // ������ �� = ���콺 y�� * �ΰ��� | ���콺�� ��,��� �����̸� mousedelta.x���� �ٲ�µ�, ĳ���Ͱ� ��,��� �����̷��� y���� ������� �ϱ� ������ ������ �޴� ������ x���� y��, y���� x�� �־���� ��.
        //x���� y�� �־��� ��.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);//�ּҰ��� �ִ밪�� �Ѿ�� �ʵ��� ���� | �ּҰ����� �۾����� �ּҰ� ��ȯ, �ִ밪���� Ŀ���� �ִ밪 ��ȯ
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); //ī�޶� �����̳��� ��ǥ�� local��ǥ�� �����ش�. | mouseDelta���� y���� ���콺�� �Ʒ��� �巡���� �� -�� �ȴ�. Rotation���� �������� �� �� ����! ���콺�� ������ �����ϴ� �Ͱ� �������� ���� �ݴ��̱� ������ ��ȣ�� �ٲ���.

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); //��, �Ʒ��� ĳ���� ������ ȸ�� | ���콺�� ��,��� �����̸� mousedelta.x���� �ٲ�µ�, ĳ���Ͱ� ��,��� �����̷��� y���� ������� �ϱ� ������ ������ �޴� ������ x���� y��, y���� x�� �־���� ��.
        //y���� x�� �־��� ��.
    }

    public void OnMove(InputAction.CallbackContext context) //���� ���¸� �޾ƿ�.
    {
        if (context.phase == InputActionPhase.Performed) //start : ������ �� | Performed : ������ ���� ���� ������ ������ ���� �� | Cancled : ��ҵ��� ��
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled) //Ű�� �������� ��
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //���콺�� ����ؼ� ���� �����ǹǷ� (�����ų� ��� �̷� ��Ȳ�� ���) ���� �о���� ��
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); //���������� Ȯ �ö󰡱⶧���� Mode -> Impulse Mode ���
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
