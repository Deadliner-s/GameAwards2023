using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // プレハブオブジェクト
    public float Interval = 2.0f;
    public float Interval2 = 8.0f;

    private float timer = 0.0f;
    private float timer2 = 0.0f;
    private bool wait = false;

    private GameObject targetObject; // 対象オブジェクト

    void Start()
    {
        // 対象オブジェクトを取得する
        targetObject = GameObject.Find("AttackPhase");
    }

    void Update()
    {
        // 対象オブジェクトが存在しない場合は、処理を実行しない
        if (targetObject == null)
        {
            return;
        }

        if (!wait)
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;

            if (timer >= Interval)
            {
                Instantiate(prefab, transform.position, transform.rotation); // プレハブオブジェクトを生成する
                timer = 0.0f; // タイマーをリセットする
            }
            if (timer2 >= Interval2)
            {
                wait = true;
                timer2 = 0.0f;
            }
        }
        if (wait)
        {
            timer2 += Time.deltaTime;

            if (timer2 >= Interval2)
            {
                wait = false;
                timer2 = 0.0f;
            }
        }
    }
}
