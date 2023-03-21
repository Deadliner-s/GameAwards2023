using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{   
    public float turnSpeed = 5f;        // �~�T�C���̐��񑬓x
    private Rigidbody rb;               // �~�T�C����Rigidbody�R���|�[�l���g
    public int cnt;
    
    public float Speed;                 // �~�T�C���̑��x
    public float MaxSpeed;              // �~�T�C���̍ō����x
    public float Accel;                 // �����x
    private Transform target;     // �ǐՂ���Ώ�  
    public float maxFlightTime = 100f;  // �~�T�C���̍ő��s����
    private float flightTime;           // �~�T�C���̌��݂̔�s����
    public int State = 0;              // ���(0:���ړ�  1:�ǐ�)


    private void Start()
    {
        // �~�T�C����Rigidbody�R���|�[�l���g���擾����
        rb = GetComponent<Rigidbody>();

        // ���ݎ��ԏ�����
        flightTime = 0;

        // ��Ԑݒ�
        State = 0;

        cnt = 0;

        // �������v���C���[�Ɠ����ɂ���
        Transform playertra = GameObject.Find("Player").transform;
        this.transform.LookAt(playertra);
    }
    
    //private void FixedUpdate()
    private void Update()
    {
        // �ǐՑΏۂ����݂��Ȃ����A�~�T�C���̍ő��s���Ԃ𒴂������ǂ������`�F�b�N����
        //if (target == null || flightTime > maxFlightTime)
        if (flightTime > maxFlightTime)
        {
            // �~�T�C����j�󂵂ă��\�b�h���I������
            DestroyMissile();
            return;
        }
        // �X�s�[�h�ɉ����x�����Z
        Speed += Accel;
        if (Speed > MaxSpeed)
        {
            Speed = MaxSpeed;
        }

        switch (State)
        {
            case (0):
                // �㏸�ړ�

                if (cnt > 5)
                {
                    State = 1;
                    break;
                }

                var pos = transform.up;
                transform.position += pos;

                cnt++;
                break;

            case (1):
                // �U���ړ�

                if (target == null)
                {
                    State = 2;
                    break;
                }

                // �~�T�C������ǐՑΏۂւ̕������v�Z����
                Vector3 targetDirection = (target.position - transform.position).normalized;
                // �~�T�C�����ǐՑΏۂ��������߂ɕK�v�ȉ�]���v�Z����
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                // Slerp��Ԃ��g�p���ă~�T�C����ǐՑΏۂɌ����ĉ�]����
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);
                // �~�T�C����ǐՑΏۂɌ������Ĉړ�������
                rb.velocity = transform.forward * Speed;

                // ��s���Ԃ𑝂₷
                //flightTime += Time.fixedDeltaTime;
                flightTime++;
                break;

            case (2):
                // �����ړ�

                var pos1 = transform.forward;
                transform.position += pos1;

                flightTime += 100;
                break;
        }

    }

    // �~�T�C�����I�u�W�F�N�g�ɏՓ˂����Ƃ��ɌĂяo�����
    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.tag != "PlayerBullet")
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
    public void SetTarget(Transform targetObj)
    {
        target = targetObj;
    }
}