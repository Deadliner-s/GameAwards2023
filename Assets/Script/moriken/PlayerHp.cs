using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//シーンの移動処理を行う機能
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    
    public GameObject GaugeObj;
    Slider HpGauge;

    public float PlayerHP;
    float damage;

    public float MaxInvflame;
    float Invflame;

    bool UseFlag;

    [SerializeField] ParticleSystem particle;
    [SerializeField] Color[] color = new Color[3];

    // Start is called before the first frame update
    void Start()
    {        
        HpGauge = GaugeObj.GetComponent<Slider>();
        HpGauge.maxValue = PlayerHP;
        HpGauge.value = PlayerHP;
        //フラグを非表示判定
        UseFlag = false;
     
    }

    // Update is called once per frame
    void Update()
    {
        var main = particle.main;
        // 敵にぶつかった時にシールドを表示する
        if (UseFlag == true)
        {   
            Invflame++;
        }

        // 無敵時間に関する処理
        if (MaxInvflame < Invflame)
        {
            UseFlag = false;
            Invflame = 0;
        }

        if(PlayerHP >= 66)
        { 
            main.startColor = color[0];
        }
        else if(PlayerHP >= 33)
        {
            main.startColor = color[1];
        }
        else
        {
            main.startColor = color[2];
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
