using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーの弾が当たった時、
        // スクリプトをコンポーネントしたオブジェクトを破壊する
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
