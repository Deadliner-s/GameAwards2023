using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyMissile : MonoBehaviour
{
    // エフェクト
    [SerializeField] GameObject obj;
    // 衝突位置
    private Vector3 hitPos;
    // プレイヤーマネージャー
    private GameObject playerManager;

    private void Update()
    {
        // プレイヤーマネージャーが無い時に代入する
        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerManager");
        }

        // プレイヤーが完全に撃墜されたとき破壊する
        if (playerManager.GetComponent<PlayerHp>().BreakFlag)
        {
            Destroy(gameObject);
        }
    }

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
            if (gameObject.GetComponent<MissileStraight>())
            {
                gameObject.GetComponent<MissileStraight>().isDestroyed = true;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
