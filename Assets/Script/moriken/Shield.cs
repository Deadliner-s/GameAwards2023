using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Shield : MonoBehaviour
{
    private GameObject playerManager;
    private GameObject shield;
    private MeshCollider MS_shield;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        //collider = gameObject.GetComponent<MeshCollider>();
        shield = GameObject.Find("hex_shield");
        MS_shield = shield.GetComponent<MeshCollider>();
        MS_shield.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーのHPが0以下になったら当たり判定を消す
        if (playerManager.GetComponent<PlayerHp>().PlayerHP <= 0)
        {
            MS_shield.enabled = false;
        }
        else
        {
            MS_shield.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerManager.GetComponent<PlayerHp>().Damage(collision);
        }
    }
}
