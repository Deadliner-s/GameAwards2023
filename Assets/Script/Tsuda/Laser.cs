using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // 毎秒増加させる値
    public float increasePerSecond = 1.0f;
    private float timer;  // タイマー

    // 毎フレーム呼ばれる関数
    private void Update()
    {
        timer += Time.deltaTime;  // タイマーを減算する

        // オブジェクトのTransformコンポーネントを取得
        Transform transform = GetComponent<Transform>();

        if (timer >= 1.5f)
        {
            // Scaleプロパティのy値を増加させる
            Vector3 scale = transform.localScale;
            scale.y += increasePerSecond * Time.deltaTime;
            transform.localScale = scale;
        }
    }
}

