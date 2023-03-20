using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //---- �C���X�y�N�^�[�Ɍ�����ϐ� ----
    public Vector3 initPos;   // �����ʒu
    public Vector3 angle;     // ��]
    public Vector3 moveAdd;   // �ړ���
    public Vector3 angleAdd;  // ��]��
    public Vector2 TopBottom; // ��ʂ̏������
    public float angleCorrection; // ��]�ʂ̕␳
    public float horizon;     // �����ɖ߂鎞�̗�
    public float speed = 0.03f; // �ړ��X�s�[�h

    //---- �ϐ��錾 ----
    private Vector3 pos;   // ���W
    private bool bInput;
    private�@Myproject InputActions; // ����
    private Vector2 inputMove;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();        
    }

    // Start is called before the first frame update
    void Start()
    {
        // ������
        initPos = transform.position; // ���W
        bInput = false; // ���͂��ꂽ���̔���
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̒l����
        // ���W
        pos = transform.position;
        // ��]
        angle = transform.eulerAngles;
        // ���̂܂܂��ƌv�Z���ɂ������߁A��������0�Ƃ������l�ɂ���
        if (angle.x >= 315) { angle.x = angle.x - 360; }
        // ����
        bInput = false;

        // �L�[����
        //KeyInput();

        // �L�[����
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();
        // �ړ�
        pos.y = pos.y + inputMove.y * speed;
        // ��]
        if (inputMove.y < 0.0f)
        {
            angle += angleAdd;
        }
        if (inputMove.y > 0.0f)
        {
            angle -= angleAdd * angleCorrection;
        }


        // �ʒu�̏C��
        // ��̏C��
        if (pos.y >= TopBottom.x) { pos.y = TopBottom.x; }
        // ���̏C��
        if (pos.y <= TopBottom.y) { pos.y = TopBottom.y; }
        // ���W�̑��
        transform.position = pos;

        // �����ɂ���
        //Horizon();

        // ��]�̏C��
        // �������̏C��
        if (angle.x >=  45) { angle.x = 45; }
        // ������̏C��
        if (angle.x <= -45) {
            angle.x = -45;
            angle.x = 360 + angle.x;
        }

        // ��]�̑��
        transform.eulerAngles = angle;

        // ���݂̊p�x = (�I�����̊p�x) * ������ + ���݂̊p�x
        //Late = (Rotate - Late) * 0.05f + Late;
        //transform.Rotate(new Vector3(0, rotate, 0));
    }

    //void FixedUpdate()
    //{

    //}


    //�`�`�`�` �L�[���͂̊֐� �`�`�`�`
    private void KeyInput()
    {

        if (Input.GetKey(KeyCode.W)) {
            // �ړ�
            pos += moveAdd;
            // ��]
            angle -= angleAdd * angleCorrection;
            //angle -= angleAdd;
            bInput = true;
        }
        if (Input.GetKey(KeyCode.S)) {
            // �ړ�
            pos -= moveAdd;
            // ��]
            angle += angleAdd;
            bInput = true;
        }
    }

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
