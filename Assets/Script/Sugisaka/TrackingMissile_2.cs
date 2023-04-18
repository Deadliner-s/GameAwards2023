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

    [Tooltip("発射方向")]
    [SerializeField]
    Vector3 maxInitVelocity;

    [Tooltip("誘導速度")]
    [SerializeField]
    float MoveSpeed;
    
    [SerializeField]
    float flgTime;


    // 追跡のために必要な変数
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

    // パーティクル生成用
    private StartParticle Particle;

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

        // パーティクル用
        Particle = GetComponent<StartParticle>();

        num = 0;
        LTime = 0;
    }

    public void Update()
    {
        // 追跡対象存在確認
        if (target == null)
        {
            num = 2;
        }
        // 生存時間更新
        LTime += Time.deltaTime;
        if (LTime > lifeTime) { 
            DestroyMissile();
        }

        switch (num)
        {
            case (0):
                // 上昇
                // 加速度の算出(等加速度直線運動
                acceleration = 2f / (time * time) * (target.position - position - time * velocity);

                // 命中時刻更新
                time -= Time.deltaTime/ 2.0f;

                // 速度と座標の算出
                velocity += acceleration * Time.deltaTime/ 2.0f;
                position += velocity * Time.deltaTime/ 2.0f;
                LateMove = velocity;

                // 座標,向き更新
                thisTransform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), velocity);
                thisTransform.position = position;

                // 高速誘導開始
                if (time < flgTime) num = 1;

                break;
            case (1):
                // 誘導移動

                if (Particle.enabled != true)
                {
                    // パーティクル生成
                    Particle.enabled = true;
                }

                // 移動計算
                Move = (target.transform.position - transform.position).normalized;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
                MoveSpeed = Mathf.Pow(1.6f, LTime) / 10.0f;
                // 座標,回転更新
                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;
                transform.position += LateMove * MoveSpeed;
                break;
            case (2):
                // 直線移動

                Move = transform.up;
                LateMove = Move;

                // 座標,回転更新
                transform.position += LateMove * MoveSpeed;
                break;
        }
    }

    IEnumerator Timer()
    {
        // lifeTine(秒間)処理を中断
        yield return new WaitForSeconds(lifeTime);

        // オブジェクトの削除
        DestroyMissile();
    }

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {
        // タグ名と違ったら
        if (collision.gameObject.tag != "Enemy")
        {
            DestroyMissile();
        }
    }

    // ミサイルの消去
    private void DestroyMissile()
    {
        Destroy(gameObject);
    }

}