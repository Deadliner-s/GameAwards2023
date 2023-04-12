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
    float lifeTime = 2;         // 生存時間(秒)
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

        // 発射方向
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

        // 生存時間管理
        StartCoroutine(nameof(Timer));
    }

    public void Update()
    {
        // 追跡対象存在確認
        if (target == null)
        {
            return;
        }

        // 加速度の算出(等加速度直線運動
        acceleration = 2f / (time * time) * (target.position - position - time * velocity);

        // 加速度制限がONの場合加速度を制限
        if (limitAcceleration && acceleration.sqrMagnitude > maxAcceleration * maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        // 命中時刻チェック
        time -= Time.deltaTime;

        if (time < 0f)
        {
            return;
        }


        // 速度と座標の算出
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        //thisTransform.rotation = Quaternion.LookRotation(position);
        thisTransform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), velocity);
        thisTransform.position = position;

    }

    IEnumerator Timer()
    {
        // lifeTine(秒間)処理を中断
        yield return new WaitForSeconds(lifeTime);

        // オブジェクトの削除
        Destroy(gameObject);
    }

}