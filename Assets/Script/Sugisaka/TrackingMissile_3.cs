using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class TrackingMissile_3 : MonoBehaviour
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

    [Tooltip("上昇時間")]
    [SerializeField]
    float stopCnt;

    [SerializeField]
    float unum;

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
    float vectol; 

    // パーティクル生成用
    private StartParticle Particle;

    // 当たり判定
    private CapsuleCollider Ccollider;

    public void SetTarget(GameObject targetObj, int num)
    {
        // ターゲット取得
        target = targetObj.transform;

        maxInitVelocity.y *= num;

        // 発射方向
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

        // 判定状態取得
        Ccollider = GetComponent<CapsuleCollider>();

        num = 0;
        LTime = 0;
    }

    public void Update()
    {
        // 追跡対象存在確認
        if (target == null)
        {
            num = 3;
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
                
                Move = new Vector3(0.0f, vectol, 0.0f);
                LateMove = Move;

                // 座標,回転更新
                transform.position += LateMove * Time.timeScale;

                // 時間確認
                if (LTime >= stopCnt)
                {
                    position = transform.position;

                    num = 1;
                }
                break;
            case (1):
                // 曲げる

                if (Particle.enabled != true)
                {
                    // パーティクル生成
                    Particle.enabled = true;
                    // 当たり判定開始
                    Ccollider.enabled = true;
                }

                // 加速度の算出(等加速度直線運動
                acceleration = 2f / (time * time) * (target.position - position - time * velocity);
                // 命中時刻更新
                time -= Time.deltaTime;
                // 速度と座標の算出
                velocity += acceleration * Time.deltaTime;
                position += velocity * Time.deltaTime;
                LateMove = velocity;
                // 座標,向き更新
                transform.rotation = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), velocity);
                transform.position = position * Time.timeScale;

                // 高速誘導開始
                if (time < flgTime) num = 2;

                break;
            case (2):
                // 誘導

                // 移動計算
                Move = (target.transform.position - transform.position).normalized;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);
                MoveSpeed = Mathf.Pow(1.6f, LTime) / 10.0f;
                // 座標,回転更新
                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;
                transform.position += LateMove * MoveSpeed * Time.timeScale;
                break;
            case (3):
                // 直線移動

                Move = transform.up;
                LateMove = Move;

                // 座標,回転更新
                transform.position += LateMove * MoveSpeed * Time.timeScale;
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