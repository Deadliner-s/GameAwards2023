using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーンの移動処理を行う機能
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossHp : MonoBehaviour
{
    private GameObject BossManager;

    // Start is called before the first frame update
    void Start()
    {
        BossManager = GameObject.Find("BossManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            BossManager.GetComponent<MainBossHp>().Damage(collision);
        }
    }
}
