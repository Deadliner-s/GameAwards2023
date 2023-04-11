using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAngle : MonoBehaviour
{
    //---- �C���X�y�N�^�[�Ɍ�����ϐ� ----
    // ��]
    [Header("��]")]
    [Tooltip("��]�̍ő�l")]
    public float angleMax = 45; // ��]�̍ő�l
    [Tooltip("��]��")]
    public Vector3 angleAdd = new Vector3(5,0,0);  // ��]��
    // ������
    [Header("����")]
    [Tooltip("�����ɖ߂鎞�̕␳��")]
    public float horizon = 1; // �����ɖ߂鎞�̗�
    //[Tooltip("��]�ʂ̕␳\n(������ɂȂ鎞�̕␳�p)")]
    //public float angleCorrection; // ��]�ʂ̕␳

    //---- �ϐ��錾 ----
    private Vector3 angle;        // ��]
    private Myproject InputActions; // ����
    private Vector2 inputMove;
    private GameObject cameraObj;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̒l����
        // ��]
        angle = transform.eulerAngles;
        // ���̂܂܂��ƌv�Z���ɂ������߁A��������0�Ƃ������l�ɂ���
        if (angle.x >= 315) { angle.x = angle.x - 360; }

        // �L�[����
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // ��]
        if (inputMove.y < 0.5f) {
            angle += angleAdd;
        }
        if (inputMove.y > -0.5f) {
            angle -= angleAdd;// * angleCorrection;
        }
        Debug.Log(inputMove);
        // �J�����ɑ΂��Ă����ƉE������������
        Transform cameraTransform = cameraObj.transform;
        Vector3 cameraAngle = cameraTransform.eulerAngles;
        angle.y = cameraAngle.y + 90.0f;

        // �����ɂ���
        Horizon();

        // ��]�̏C��
        // �������̏C��
        if (angle.x >= angleMax) { angle.x = angleMax; }
        // ������̏C��
        if (angle.x <= -angleMax) {
            angle.x = -angleMax;
            angle.x = 360 + angle.x;
        }

        // ��]�̑��
        transform.eulerAngles = angle;
    }

    //void FixedUpdate()
    //{

    //}

    //�`�`�`�` �����ɂ���֐� �`�`�`�`
    private void Horizon()
    {
        // ��
        if (angle.x >= 0) {
            angle.x = angle.x - horizon;
            if (angle.x <= 0) { angle.x = 0; }
        }
        // ��
        if (angle.x < 0) {
            angle.x = angle.x + horizon;
            if (angle.x >= 0) { angle.x = 0; }
        }
    }
}
