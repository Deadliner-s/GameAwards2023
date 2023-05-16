using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject Player;
    MeshCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        collider = gameObject.GetComponent<MeshCollider>();
        collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーのHPが0以下になったら当たり判定を消す
        if (Player.GetComponent<PlayerHp>().PlayerHP <= 0)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player.GetComponent<PlayerHp>().Damage(collision);
        }
    }
}
