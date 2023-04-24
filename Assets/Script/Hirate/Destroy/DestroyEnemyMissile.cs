using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyMissile : MonoBehaviour
{
    // エフェクト
    [SerializeField] GameObject obj;
    // 衝突位置
    private Vector3 hitPos;

    // オブジェクトが当たった時に行われる関数
    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たった時
        if (collision.gameObject.tag == "Player")
        {
            // 衝突位置を取得
            foreach(ContactPoint contact in collision.contacts)
            {
                hitPos = contact.point;
            }

            // エフェクトを生成
            Instantiate(
                obj, // エフェクトが入ったオブジェクト
                hitPos, // 座標
                Quaternion.identity); // 回転

            // スクリプトをコンポーネントしたオブジェクトを削除
            Destroy(gameObject);
        }
    }
}
