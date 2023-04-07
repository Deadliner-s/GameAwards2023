using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumDelete : MonoBehaviour
{
    GameObject Boss;

    int HitCount;
    int MaxHitCount;

    // Start is called before the first frame update
    void Start()
    {
        HitCount = 0;
        Boss = GameObject.Find("BOSS_base");
        MaxHitCount = Boss.GetComponent<MainBossHp>().MAXHitCount;
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
                Destroy(gameObject);
            }
        }
    }
}
