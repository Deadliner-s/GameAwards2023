using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrackingMissile_2 : MonoBehaviour
{
    [Tooltip("�ǐՑΏ�")]
    [SerializeField]
    Transform target;

    [Tooltip("���b�őΏۂɓ��Ă邩")]
    [SerializeField, Min(0)]
    float time = 1;

    [Tooltip("��������(�b)")]
    [SerializeField]
    float lifeTime = 2;

    [Tooltip("���˕���")]
    [SerializeField]
    Vector3 maxInitVelocity;

    [Tooltip("�U�����x")]
    [SerializeField]
    float MoveSpeed;
    
    [SerializeField]
    float flgTime;


    // �ǐՂ̂��߂ɕK�v�ȕϐ�
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

    Vector3 Move;
    Vector3 LateMove;
    float off = 0.2f;
    Quaternion rot;

    int num;
    float LTime;

    // �p�[�e�B�N�������p
    private StartParticle Particle;

    public void SetTarget(GameObject targetObj)
    {
        // �^�[�Q�b�g�擾
        target = targetObj.transform;
    }

    void Start()
    {
        // �����ʒu�ݒ�
        thisTransform = transform;
        position = thisTransform.position;

        // ���˕���
        velocity = new Vector3(
            maxInitVelocity.x,
            maxInitVelocity.y,
            maxInitVelocity.z
            );

        // �������ԊǗ�
        StartCoroutine(nameof(Timer));

        // �p�[�e�B�N���p
        Particle = GetComponent<StartParticle>();

        num = 0;
        LTime = 0;
    }

    public void Update()
    {
        // �ǐՑΏۑ��݊m�F
        if (target == null)
        {
            num = 2;
        }
        // �������ԍX�V
        LTime += Time.deltaTime;
        if (LTime > lifeTime) { 
            DestroyMissile();
        }

        switch (num)
        {
            case (0):
                // �㏸
                // �����x�̎Z�o(�������x�����^��
                acceleration = 2f / (time * time) * (target.position - position - time * velocity);

                // ���������X�V
                time -= Time.deltaTime/ 2.0f;

                // ���x�ƍ��W�̎Z�o
                velocity += acceleration * Time.deltaTime/ 2.0f;
                position += velocity * Time.deltaTime/ 2.0f;
                LateMove = velocity;

                // ���W,�����X�V
                thisTransform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), velocity);
                thisTransform.position = position;

                // �����U���J�n
                if (time < flgTime) num = 1;

                break;
            case (1):
                // �U���ړ�

                if (Particle.enabled != true)
                {
                    // �p�[�e�B�N������
                    Particle.enabled = true;
                }

                // �ړ��v�Z
                Move = (target.transform.position - transform.position).normalized;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
                MoveSpeed = Mathf.Pow(1.6f, LTime) / 10.0f;
                // ���W,��]�X�V
                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;
                transform.position += LateMove * MoveSpeed;
                break;
            case (2):
                // �����ړ�

                Move = transform.up;
                LateMove = Move;

                // ���W,��]�X�V
                transform.position += LateMove * MoveSpeed;
                break;
        }
    }

    IEnumerator Timer()
    {
        // lifeTine(�b��)�����𒆒f
        yield return new WaitForSeconds(lifeTime);

        // �I�u�W�F�N�g�̍폜
        DestroyMissile();
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

}