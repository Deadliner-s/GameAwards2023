using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{   
    public float Speed;                 // �~�T�C���̑��x
    public float MaxSpeed;              // �~�T�C���̍ō����x
    public float Accel;                 // �����x
    private GameObject target;     // �ǐՂ���Ώ�  
    public float maxFlightTime = 100f;  // �~�T�C���̍ő��s����
    private float flightTime;           // �~�T�C���̌��݂̔�s����
    public int State = 0;              // ���(0:���ړ�  1:�ǐ�)


    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�
    private float off = 0.2f;
    private Quaternion rot;

    private void Start()
    {
        // �~�T�C����Rigidbody�R���|�[�l���g���擾����
        //rb = GetComponent<Rigidbody>();

        // ���ݎ��ԏ�����
        flightTime = 0;

        // ��Ԑݒ�
        State = 0;


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
        if (target == null)
        {
            State = 2;
        }


        switch (State)
        {
            case (0):
                // �㏸�ړ�

                // �~�T�C�����^�[�Q�b�g�Ɠ��������ɂȂ�܂ł�
                if (target.transform.position.y <= transform.position.y)
                {
                    State = 1;
                    break;
                }

                Move = new Vector3(0.0f, 1.0f, 0.0f);
                LateMove = Move;

                break;

            case (1):
                // �U���ړ�

                // �X�s�[�h�ɉ����x�����Z
                Speed += Accel;
                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
                }

                //// �~�T�C������ǐՑΏۂւ̕������v�Z����
                //Vector3 targetDirection = (target.position - transform.position).normalized;
                //// �~�T�C�����ǐՑΏۂ��������߂ɕK�v�ȉ�]���v�Z����
                //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                //// Slerp��Ԃ��g�p���ă~�T�C����ǐՑΏۂɌ����ĉ�]����
                //rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);
                //// �~�T�C����ǐՑΏۂɌ������Ĉړ�������
                //rb.velocity = transform.forward * Speed;

                Move = target.transform.position - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                // ��s���Ԃ𑝂₷
                flightTime++;
                break;

            case (2):
                // �����ړ�

                Move = new Vector3(1.0f, 0.0f, 0.0f);
                LateMove = Move;

                flightTime += 100;
                break;
        }

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;

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
    public void SetTarget(GameObject targetObj)
    {
        target = targetObj;
    }
}