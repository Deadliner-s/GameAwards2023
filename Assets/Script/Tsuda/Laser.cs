using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // 毎秒増加させる値
    public float increasePerSecond = 1.0f;

    // 毎フレーム呼ばれる関数
    private void Update()
    {
        // オブジェクトのTransformコンポーネントを取得
        Transform transform = GetComponent<Transform>();

        // Scaleプロパティのy値を増加させる
        Vector3 scale = transform.localScale;
        scale.y += increasePerSecond * Time.deltaTime;
        transform.localScale = scale;
    }
}

