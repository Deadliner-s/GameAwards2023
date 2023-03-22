using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{   
    public float Speed;                 // ミサイルの速度
    public float MaxSpeed;              // ミサイルの最高速度
    public float Accel;                 // 加速度
    private GameObject target;          // 追跡する対象  
    public float maxFlightTime = 100f;  // ミサイルの最大飛行時間
    private float flightTime;           // ミサイルの現在の飛行時間
    public int State = 0;               // 状態(0:横移動  1:追跡)
    private Vector3 Move;               //移動方向
    private Vector3 LateMove;           //滑らか動きをするためのmove変数
    private float off = 0.2f;
    private Quaternion rot;

    private void Start()
    {
        // 存在時間初期化
        flightTime = 0;

        // 状態設定
        State = 0;
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
            State = 2;
        }

        // スピード更新
        Speed += Accel;
        if (Speed >= MaxSpeed)
        {
            Speed = MaxSpeed;
        }


        switch (State)
        {
            case (0):
                // 上昇移動

                // ミサイルがターゲットと同じ高さになるまでの
                if (target.transform.position.y <= transform.position.y)
                {
                    State = 1;
                    break;
                }

                Move = new Vector3(0.0f, 1.0f, 0.0f);
                LateMove = Move;

                break;

            case (1):
                // 誘導移動
                
                Move = target.transform.position - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                // 飛行時間更新
                flightTime++;
                break;

            case (2):
                // 直線移動

                Move = new Vector3(1.0f, 0.0f, 0.0f);
                LateMove = Move;

                flightTime += 100;
                break;
        }

        // 座標,回転更新
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;

    }

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {       
        // タグ名と違ったら
        if (collision.gameObject.tag != "PlayerBullet")
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