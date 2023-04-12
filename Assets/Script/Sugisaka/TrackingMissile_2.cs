using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrackingMissile_2 : MonoBehaviour
{

    [SerializeField]
    Transform target;
    [SerializeField, Min(0)]
    float time = 1;
    [SerializeField]
    float lifeTime = 2;         // ��������(�b)
    [SerializeField]
    bool limitAcceleration = false;
    [SerializeField, Min(0)]
    float maxAcceleration = 100;
    [SerializeField]
    Vector3 minInitVelocity;
    [SerializeField]
    Vector3 maxInitVelocity;

    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

    //public Transform Target
    //{
    //    set
    //    {
    //        target = value;
    //    }
    //    get
    //    {
    //        return target;
    //    }
    //}

    public void SetTarget(GameObject targetObj)
    {
        target = targetObj.transform;
    }

    void Start()
    {

        thisTransform = transform;
        position = thisTransform.position;

        // ���˕���
        //velocity = new Vector3(
        //    Random.Range(minInitVelocity.x, maxInitVelocity.x),
        //    Random.Range(minInitVelocity.y, maxInitVelocity.y),
        //    Random.Range(minInitVelocity.z, maxInitVelocity.z)
        //    );
        velocity = new Vector3(
            0.0f,
            maxInitVelocity.y,
            0.0f
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

        // ���������`�F�b�N
        time -= Time.deltaTime;

        if (time < 0f)
        {
            return;
        }


        // ���x�ƍ��W�̎Z�o
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        //thisTransform.rotation = Quaternion.LookRotation(position);
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

}