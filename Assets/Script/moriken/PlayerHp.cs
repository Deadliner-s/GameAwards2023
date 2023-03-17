using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーンの移動処理を行う機能
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public GameObject DisplayObj;
    public GameObject GaugeObj;
    Slider HpGauge;

    public float PlayerHP;
    float damage;

    public float MaxInvflame;
    float Invflame;

    bool UseFlag;

    // Start is called before the first frame update
    void Start()
    {
        //初期状態ではメニューを非表示
        DisplayObj.SetActive(false);
        HpGauge = GaugeObj.GetComponent<Slider>();
        HpGauge.maxValue = PlayerHP;
        HpGauge.value = PlayerHP;
        //フラグを非表示判定
        UseFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 敵にぶつかった時にシールドを表示する
        if (UseFlag == true)
        {
            DisplayObj.SetActive(true);
            
            Invflame++;
        }

        // 無敵時間に関する処理
        if (MaxInvflame < Invflame)
        {
            DisplayObj.SetActive(false);
            UseFlag = false;
            Invflame = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Invflame == 0)
        {
            // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // "Enemy"タグがついているオブジェクトにある"PlayerDamage"変数を受けとる
                damage = collision.gameObject.GetComponent<Damage>().PlayerDamage;
                PlayerHP -= damage;
                HpGauge.value -= damage;
                
                if (PlayerHP >= 0)
                {
                    UseFlag = true;
                }
                else
                {
                    Destroy(this.gameObject);
                    //シーン移動
                    SceneManager.LoadScene("SceneGameOver");
                }
            }
        }
    }
}
