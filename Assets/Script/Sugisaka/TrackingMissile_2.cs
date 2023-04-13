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

    [Tooltip("���x����t���邩")]
    [SerializeField]
    bool limitAcceleration = false;

    [Tooltip("���x���")]
    [SerializeField, Min(0)]
    float maxAcceleration = 100;

    [Tooltip("���˕���")]
    [SerializeField]
    Vector3 maxInitVelocity;

    // �ǐՂ̂��߂ɕK�v�ȕϐ�
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

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
    }

    public void Update()
    {
        // �ǐՑΏۑ��݊m�F
        if (target == null)
        {
            return;
        }

        // �����x�̎Z�o(�������x�����^��
        acceleration = 2f / (time * time) * (target.position - position - time * velocity);

        // �����x������ON�̏ꍇ�����x�𐧌�
        if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        // ���������X�V
        time -= Time.deltaTime;

        if (time < 0f)
        {
            return;
        }


        // ���x�ƍ��W�̎Z�o
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        // ���W,�����X�V
        thisTransform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), velocity);
        thisTransform.position = position;

    }

    IEnumerator Timer()
    {
        // lifeTine(�b��)�����𒆒f
        yield return new WaitForSeconds(lifeTime);

        // �I�u�W�F�N�g�̍폜
        Destroy(gameObject);
    }

    // �~�T�C�����I�u�W�F�N�g�ɏՓ˂����Ƃ��ɌĂяo�����
    private void OnCollisionEnter(Collision collision)
    {
        // �^�O���ƈ������
        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}