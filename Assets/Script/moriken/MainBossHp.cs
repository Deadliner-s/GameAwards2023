using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainBossHp : MonoBehaviour
{
    public GameObject GaugeObj;
    Image HpGauge;

    public float BossHP;
    float BossMaxHP;
    float damage;

    // 大ダメージ数
    public float HardDamage;

    //何回当たったら
    public int MAXHitCount;
    int HitCount;

    // 何フレーム後にダメージ表現
    float flame;
    public float HardDamageFlame;

    // Start is called before the first frame update
    void Start()
    {
        BossMaxHP = BossHP;
        HpGauge = GaugeObj.GetComponent<Image>();
        HpGauge.fillAmount = 1;
        HitCount = 0;
        flame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // ヒット回数がMaxと同じだったら
        if (HitCount == MAXHitCount)
        {
            // HardDamageFlameになったら
            if (flame == HardDamageFlame)
            {
                BossHP -= HardDamage;
                HpGauge.fillAmount -= HardDamage / BossMaxHP;

                HitCount = 0;
            }
            flame++;
        }
    }

    public void Damage(Collision collision)
    {
        // "Enemy"タグがついているオブジェクトにある"PlayerDamage"変数を受けとる
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage;
        HpGauge.fillAmount -= damage/BossMaxHP;
        HitCount++;
        flame = 0;

        if (BossHP <= 0)
        {
            //シーン移動
            SceneManager.LoadScene("GameClear");
        }
    }
}
