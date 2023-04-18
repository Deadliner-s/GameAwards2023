using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    [Header("�ᑬ�㏸")]
    [Tooltip("�㏸���x")]
    [SerializeField]
    float upSpeed;
    [Tooltip("����")]
    [SerializeField]
    float inertiaSpeed;
    [Tooltip("�㏸���E")]
    [SerializeField]
    float maxHeight;

    [Header("����")]
    [Tooltip("���߂̎���(�b)")]
    [SerializeField]
    float stopTime;

    [Header("�U���ړ�")]
    [Tooltip("�U�����x")]
    [SerializeField]
    float MoveSpeed;

    [Header("��������")]
    [Tooltip("�~�T�C���̍ő��s����(�t���[��)")]
    [SerializeField]
    float maxFlightTime = 100f;

    // ���(0:�㏸�ړ�  1:����  2:�ǐ�  3:�^�[�Q�b�g�Ȃ��Ȃ����ꍇ)
    private int State = 0;

    // �ǐՂ���Ώ�
    private GameObject target;

    // �~�T�C���̌��݂̔�s����
    private float flightTime;

    // �����ꏊ
    private Vector3 sponePoint;

    // ���ߎ��ԗp�J�E���g
    private float stopCnt;

    // �ړ��֌W
    private Vector3 Move;
    private Vector3 LateMove;
    private float off = 0.2f;
    private Quaternion rot;

    // �����蔻��
    private CapsuleCollider Ccollider;


    private void Start()
    {
        // ���ݎ��ԏ�����
        flightTime = 0;

        // ��Ԑݒ�
        State = 0;

        // ���ߎ��ԏ�����
        stopCnt = 0;

        // �����ꏊ�ۑ�
        sponePoint = transform.position;

        // �����Ԏ擾
        Ccollider = GetComponent<CapsuleCollider>();
    }
    
    //private void FixedUpdate()
    private void Update()
    {
        // �~�T�C���̍ő��s���Ԃ𒴂������ǂ���
        if (flightTime > maxFlightTime)
        {
            // �~�T�C����j�󂵂ă��\�b�h���I������
            DestroyMissile();
            return;
        }
        // �ǐՑΏۂ����݂��Ȃ���
        if (target == null)
        {
            // �Ώۂ����Ȃ������璼���ړ�
            State = 3;
        }


        switch (State)
        {
            case (0):
                // �㏸�ړ�

                // ���Ԋm�F
                if (stopTime <= stopCnt)
                {
                    State = 2;
                }

                // �����m�F
                if (sponePoint.y + maxHeight <= transform.position.y)
                {
                    State = 1;
                    break;
                }

                Move = new Vector3(1.0f * inertiaSpeed, 1.0f * upSpeed, 0.0f);
                LateMove = Move;

                // ���W,��]�X�V

                transform.position += LateMove;

                stopCnt += Time.deltaTime;

                break;

            case (1):
                // ����
                if (stopTime <= stopCnt)
                {
                    State = 2;
                }

                stopCnt += Time.deltaTime;

                Move = (target.transform.position - transform.position);
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;

                break;

            case (2):
                // �U���ړ�

                if (Ccollider.enabled != true)
                {
                    stopCnt++;
                    if (stopTime + 5 <= stopCnt)
                    {
                        Ccollider.enabled = true;
                    }
                }

                Move = (target.transform.position - transform.position).normalized;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                // ���W,��]�X�V
                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;
                transform.position += LateMove * MoveSpeed;

                // ��s���ԍX�V
                flightTime++;
                break;

            case (3):
                // �����ړ�
                
                Move = transform.up;
                LateMove = Move;

                // ���W,��]�X�V
                transform.position += LateMove * MoveSpeed;

                // ��s���ԍX�V
                flightTime += 100;
                break;
        }
    }

    // �~�T�C�����I�u�W�F�N�g�ɏՓ˂����Ƃ��ɌĂяo�����
    private void OnCollisionEnter(Collision collision)
    {       
        // �^�O���ƈ������
        if (collision.gameObject.tag != "Enemy")
        {
            DestroyMissile();
        }
    }

    // �~�T�C���̏���
    private void DestroyMissile()
    {
        Destroy(gameObject);
    }

    // �^�[�Q�b�g�ݒ�
    public void SetTarget(GameObject targetObj)
    {
        target = targetObj;
    }
}