using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrackingMissile_3 : MonoBehaviour
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

    [Tooltip("�㏸����")]
    [SerializeField]
    float stopCnt;

    [SerializeField]
    float unum;

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
    float vectol; 

    // �p�[�e�B�N�������p
    private StartParticle Particle;

    // �����蔻��
    private CapsuleCollider Ccollider;

    public void SetTarget(GameObject targetObj, int num)
    {
        // �^�[�Q�b�g�擾
        target = targetObj.transform;

        maxInitVelocity.y *= num;

        // ���˕���
        velocity = new Vector3(
            maxInitVelocity.x,
            maxInitVelocity.y,
            maxInitVelocity.z
            );

        vectol = maxInitVelocity.y / unum;

        if (num < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180.0f, new Vector3(0, 0, 1));
        }
    }

    void Start()
    {
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

        // �����Ԏ擾
        Ccollider = GetComponent<CapsuleCollider>();

        num = 0;
        LTime = 0;
    }

    public void Update()
    {
        // �ǐՑΏۑ��݊m�F
        if (target == null)
        {
            num = 3;
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
                
                Move = new Vector3(0.0f, vectol, 0.0f);
                LateMove = Move;

                // ���W,��]�X�V
                transform.position += LateMove * Time.timeScale;

                // ���Ԋm�F
                if (LTime >= stopCnt)
                {
                    position = transform.position;

                    num = 1;
                }
                break;
            case (1):
                // �Ȃ���

                if (Particle.enabled != true)
                {
                    // �p�[�e�B�N������
                    Particle.enabled = true;
                    // �����蔻��J�n
                    Ccollider.enabled = true;
                }

                // �����x�̎Z�o(�������x�����^��
                acceleration = 2f / (time * time) * (target.position - position - time * velocity);
                // ���������X�V
                time -= Time.deltaTime;
                // ���x�ƍ��W�̎Z�o
                velocity += acceleration * Time.deltaTime;
                position += velocity * Time.deltaTime;
                LateMove = velocity;
                // ���W,�����X�V
                transform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), velocity);
                transform.position = position * Time.timeScale;

                // �����U���J�n
                if (time < flgTime) num = 2;

                break;
            case (2):
                // �U��

                // �ړ��v�Z
                Move = (target.transform.position - transform.position).normalized;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
                MoveSpeed = Mathf.Pow(1.6f, LTime) / 10.0f;
                // ���W,��]�X�V
                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;
                transform.position += LateMove * MoveSpeed * Time.timeScale;
                break;
            case (3):
                // �����ړ�

                Move = transform.up;
                LateMove = Move;

                // ���W,��]�X�V
                transform.position += LateMove * MoveSpeed * Time.timeScale;
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