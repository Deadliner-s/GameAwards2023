using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 津田ミサイル(仮)

public class TrackingBullet : MonoBehaviour
{
    // 追跡する対象
    Transform target;

    // ミサイルの速度
    public float speed = 8f;

    // ミサイルの旋回速度
    public float turnSpeed = 5f;

    // ミサイルの最大飛行時間
    public float maxFlightTime = 100f;

    // ミサイルのRigidbodyコンポーネント
    private Rigidbody rb;

    // ミサイルの現在の飛行時間
    private float flightTime;

    // 旋回時のスピード
    public float SideSpped = 1f;

    // 旋回限界
    public float MaxSide = 2f;

    // 
    private int State;

    // スクリプトの開始時に呼び出される
    private void Start()
    {
        target = GameObject.FindWithTag("Target").transform;
        // ミサイルのRigidbodyコンポーネントを取得する
        rb = GetComponent<Rigidbody>();

        flightTime = 0;
        State = 0;
    }

    // 一定時間ごとに呼び出される
    private void FixedUpdate()
    {
        // 追跡対象が存在しないか、ミサイルの最大飛行時間を超えたかどうかをチェックする
        if (target == null || flightTime > maxFlightTime)
        {
            // ミサイルを破壊してメソッドを終了する
            DestroyMissile();
            return;
        }

        switch(State)
        {
            case (0):
                Vector3 pos = transform.position;
                pos.x += SideSpped;
                transform.position = pos;
                if (pos.x > MaxSide)
                {
                    State = 1; 
                }
                break;

            case (1):
                // ミサイルから追跡対象への方向を計算する
                Vector3 targetDirection = (target.position - transform.position).normalized;

                // ミサイルが追跡対象を向くために必要な回転を計算する
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                // Slerp補間を使用してミサイルを追跡対象に向けて回転する
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);

                // ミサイルを追跡対象に向かって移動させる
                rb.velocity = transform.forward * speed;

                // 飛行時間を増やす
                flightTime += Time.fixedDeltaTime;
                break;
        }
    }

    // ミサイルがオブジェクトに衝突したときに呼び出される
    private void OnCollisionEnter(Collision collision)
    {
        DestroyMissile();
    }

    // ミサイルの消去
    private void DestroyMissile()
    {
        Destroy(gameObject);
    }
}