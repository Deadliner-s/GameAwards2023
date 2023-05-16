using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumDelete : MonoBehaviour
{
    private GameObject BossManager;

    int HitCount;
    int MaxHitCount;

    // Start is called before the first frame update
    void Start()
    {
        BossManager = GameObject.Find("BossManager");

        HitCount = 0;
       // Boss = GameObject.Find("boss_model");
        MaxHitCount = BossManager.GetComponent<MainBossHp>().MAXHitCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーの弾が当たった時、
        // スクリプトをコンポーネントしたオブジェクトを破壊する
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HitCount++;
            if(HitCount == MaxHitCount)
            {
                // エフェクト表示
                BossManager.GetComponent<MainBossHp>().ExplosionSet(collision);

                Destroy(gameObject);
            }
        }
    }
}
