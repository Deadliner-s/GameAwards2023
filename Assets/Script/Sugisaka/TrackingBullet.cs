using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Óc�~�T�C��(��)

public class TrackingBullet : MonoBehaviour
{
    // �ǐՂ���Ώ�
    Transform target;

    // �~�T�C���̑��x
    public float speed = 8f;

    // �~�T�C���̐��񑬓x
    public float turnSpeed = 5f;

    // �~�T�C���̍ő��s����
    public float maxFlightTime = 100f;

    // �~�T�C����Rigidbody�R���|�[�l���g
    private Rigidbody rb;

    // �~�T�C���̌��݂̔�s����
    private float flightTime;

    // ���񎞂̃X�s�[�h
    public float SideSpped = 1f;

    // ������E
    public float MaxSide = 2f;

    // 
    private int State;

    // �X�N���v�g�̊J�n���ɌĂяo�����
    private void Start()
    {
        target = GameObject.FindWithTag("Target").transform;
        // �~�T�C����Rigidbody�R���|�[�l���g���擾����
        rb = GetComponent<Rigidbody>();

        flightTime = 0;
        State = 0;
    }

    // ��莞�Ԃ��ƂɌĂяo�����
    private void FixedUpdate()
    {
        // �ǐՑΏۂ����݂��Ȃ����A�~�T�C���̍ő��s���Ԃ𒴂������ǂ������`�F�b�N����
        if (target == null || flightTime > maxFlightTime)
        {
            // �~�T�C����j�󂵂ă��\�b�h���I������
            DestroyMissile();
            return;
        }

        switch(State)
        {
            case (0):
                Vector3 pos = transform.position;
                pos.x += SideSpped;
                transform.position = pos;
                if (pos.x > MaxSide)
                {
                    State = 1; 
                }
                break;

            case (1):
                // �~�T�C������ǐՑΏۂւ̕������v�Z����
                Vector3 targetDirection = (target.position - transform.position).normalized;

                // �~�T�C�����ǐՑΏۂ��������߂ɕK�v�ȉ�]���v�Z����
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                // Slerp��Ԃ��g�p���ă~�T�C����ǐՑΏۂɌ����ĉ�]����
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);

                // �~�T�C����ǐՑΏۂɌ������Ĉړ�������
                rb.velocity = transform.forward * speed;

                // ��s���Ԃ𑝂₷
                flightTime += Time.fixedDeltaTime;
                break;
        }
    }

    // �~�T�C�����I�u�W�F�N�g�ɏՓ˂����Ƃ��ɌĂяo�����
    private void OnCollisionEnter(Collision collision)
    {
        DestroyMissile();
    }

    // �~�T�C���̏���
    private void DestroyMissile()
    {
        Destroy(gameObject);
    }
}