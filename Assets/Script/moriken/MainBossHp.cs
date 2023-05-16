using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainBossHp : MonoBehaviour
{
    public GameObject GaugeObj;
    private Image HpGauge;

    public float BossHP;
    private float BossMaxHP;
    private float damage;

    // エフェクト
    [SerializeField]
    [Tooltip("大ダメージ時")]
    private GameObject explosionEffect;
    [SerializeField]
    [Tooltip("通常ダメージ時")]
    private GameObject NormalHiteffect;

    // 大ダメージ数
    public float HardDamage;

    //何回当たったら
    public int MAXHitCount;

    // Start is called before the first frame update
    void Start()
    {
        BossMaxHP = BossHP;
        HpGauge = GaugeObj.GetComponent<Image>();
        HpGauge.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Damage(Collision collision)
    {
        // "Enemy"タグがついているオブジェクトにある"PlayerDamage"変数を受けとる
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage;
        HpGauge.fillAmount -= damage/BossMaxHP;

        // エフェクト生成
        GameObject InstantiateEffect
            = GameObject.Instantiate(NormalHiteffect, collision.transform.position, Quaternion.identity);

        if (BossHP <= 0)
        {
            //シーン移動
            SceneManager.LoadScene("GameClear");
        }
    }

    public void ExplosionSet(Collision collision)
    {
        // エフェクト生成
        GameObject InstantiateEffect 
            = GameObject.Instantiate(explosionEffect, collision.transform.position, Quaternion.identity);

        // 表示時間
        //Destroy(InstantiateEffect, 10.0f);

        // hp更新
        BossHP -= HardDamage;
        HpGauge.fillAmount -= HardDamage / BossMaxHP;

        SoundManager.instance.PlaySE("WeakPoint");
    }
}
