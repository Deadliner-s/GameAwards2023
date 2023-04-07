using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumDelete : MonoBehaviour
{
    int HitCount;
    public int MaxHitCount;

    // Start is called before the first frame update
    void Start()
    {
        HitCount = 0;
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
