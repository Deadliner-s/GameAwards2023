using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrackingMissile_2 : MonoBehaviour
{
    [Tooltip("追跡対象")]
    [SerializeField]
    Transform target;

    [Tooltip("何秒で対象に当てるか")]
    [SerializeField, Min(0)]
    float time = 1;

    [Tooltip("生存時間(秒)")]
    [SerializeField]
    float lifeTime = 2;

    [Tooltip("速度上限付けるか")]
    [SerializeField]
    bool limitAcceleration = false;

    [Tooltip("速度上限")]
    [SerializeField, Min(0)]
    float maxAcceleration = 100;

    [Tooltip("発射方向")]
    [SerializeField]
    Vector3 maxInitVelocity;

    // 追跡のために必要な変数
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

    public void SetTarget(GameObject targetObj)
    {
        // ターゲット取得
        target = targetObj.transform;
    }

    void Start()
    {
        // 初期位置設定
        thisTransform = transform;
        position = thisTransform.position;

        // 発射方向
        velocity = new Vector3(
            maxInitVelocity.x,
            maxInitVelocity.y,
            maxInitVelocity.z
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

        // 命中時刻更新
        time -= Time.deltaTime;

        if (time < 0f)
        {
            return;
        }


        // 速度と座標の算出
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;

        // 座標,向き更新
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

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {
        // タグ名と違ったら
        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}