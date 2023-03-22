using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : MonoBehaviour
{   
    public float Speed;                 // ミサイルの速度
    public float MaxSpeed;              // ミサイルの最高速度
    public float Accel;                 // 加速度
    private GameObject target;     // 追跡する対象  
    public float maxFlightTime = 100f;  // ミサイルの最大飛行時間
    private float flightTime;           // ミサイルの現在の飛行時間
    public int State = 0;              // 状態(0:横移動  1:追跡)


    Vector3 Move;               //移動方向
    Vector3 LateMove;           //滑らか動きをするためのmove変数
    private float off = 0.2f;
    private Quaternion rot;

    private void Start()
    {
        // ミサイルのRigidbodyコンポーネントを取得する
        //rb = GetComponent<Rigidbody>();

        // 存在時間初期化
        flightTime = 0;

        // 状態設定
        State = 0;


    }
    
    //private void FixedUpdate()
    private void Update()
    {
        // 追跡対象が存在しないか、ミサイルの最大飛行時間を超えたかどうかをチェックする
        //if (target == null || flightTime > maxFlightTime)
        if (flightTime > maxFlightTime)
        {
            // ミサイルを破壊してメソッドを終了する
            DestroyMissile();
            return;
        }
        if (target == null)
        {
            State = 2;
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

                // スピードに加速度を加算
                Speed += Accel;
                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
                }

                //// ミサイルから追跡対象への方向を計算する
                //Vector3 targetDirection = (target.position - transform.position).normalized;
                //// ミサイルが追跡対象を向くために必要な回転を計算する
                //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                //// Slerp補間を使用してミサイルを追跡対象に向けて回転する
                //rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);
                //// ミサイルを追跡対象に向かって移動させる
                //rb.velocity = transform.forward * Speed;

                Move = target.transform.position - transform.position;
                Move = Move.normalized;
                LateMove = (Move - LateMove) * off + (LateMove);

                // 飛行時間を増やす
                flightTime++;
                break;

            case (2):
                // 直線移動

                Move = new Vector3(1.0f, 0.0f, 0.0f);
                LateMove = Move;

                flightTime += 100;
                break;
        }

        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;

    }

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {       
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