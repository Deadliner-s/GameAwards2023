using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{
    [Header("低速上昇")]
    [Tooltip("上昇速度")]
    public float upSpeed;
    [Tooltip("慣性")]
    public float inertiaSpeed;
    [Tooltip("上昇限界")]
    public float maxHeight;

    [Header("ため")]
    [Tooltip("ための時間(フレーム)")]
    public int stopTime;

    [Header("誘導移動")]
    [Tooltip("誘導速度")]
    public float MoveSpeed;

    [Header("生存時間")]
    [Tooltip("ミサイルの最大飛行時間(フレーム)")]
    public float maxFlightTime = 100f;

    // 状態(0:上昇移動  1:ため  2:追跡  3:ターゲットなくなった場合)
    private int State = 0;

    // 追跡する対象
    private GameObject target;

    // ミサイルの現在の飛行時間
    private float flightTime;

    // 生成場所
    private Vector3 sponePoint;

    // ため時間用カウント
    private int stopCnt;

    // 移動関係
    private Vector3 Move;
    private Vector3 LateMove;
    private float off = 0.2f;
    private Quaternion rot;


    private void Start()
    {
        // 存在時間初期化
        flightTime = 0;

        // 状態設定
        State = 0;

        // ため時間初期化
        stopCnt = 0;

        // 生成場所保存
        sponePoint = transform.position;
    }
    
    //private void FixedUpdate()
    private void Update()
    {
        // ミサイルの最大飛行時間を超えたかどうか
        if (flightTime > maxFlightTime)
        {
            // ミサイルを破壊してメソッドを終了する
            DestroyMissile();
            return;
        }
        // 追跡対象が存在しないか
        if (target == null)
        {
            // 対象がいなかったら直線移動
            State = 3;
        }


        switch (State)
        {
            case (0):
                // 上昇移動

                // 高さ確認
                if (sponePoint.y + maxHeight <= transform.position.y)
                {
                    State = 1;
                    break;
                }

                Move = new Vector3(1.0f * inertiaSpeed, 1.0f * upSpeed, 0.0f);
                LateMove = Move;

                // 座標,回転更新

                transform.position += LateMove;

                break;

            case (1):
                // ため
                if (stopTime <= stopCnt)
                {
                    State = 2;
                }
                stopCnt++;

                Move = (target.transform.position - transform.position);
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;

                break;

            case (2):
                // 誘導移動
                
                Move = (target.transform.position - transform.position).normalized;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                // 座標,回転更新
                rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
                transform.rotation = rot;
                transform.position += LateMove * MoveSpeed;

                // 飛行時間更新
                flightTime++;
                break;

            case (3):
                // 直線移動
                
                Move = transform.up;
                LateMove = Move;

                // 座標,回転更新
                transform.position += LateMove * MoveSpeed;

                // 飛行時間更新
                flightTime += 100;
                break;
        }
    }

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {       
        // タグ名と違ったら
        if (collision.gameObject.tag == "Enemy")
        {
            DestroyMissile();
        }
    }

    // ミサイルの消去
    private void DestroyMissile()
    {
        Destroy(gameObject);
    }

    // ターゲット設定
    public void SetTarget(GameObject targetObj)
    {
        target = targetObj;
    }
}