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

    public float HardDamage;

    public int MAXHitCount;
    int HitCount;


    // Start is called before the first frame update
    void Start()
    {
        BossMaxHP = BossHP;
        HpGauge = GaugeObj.GetComponent<Image>();
        HpGauge.fillAmount = 1;
        HitCount = 0;
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
        HitCount++;


        if(BossHP <= 0)
        {
            //シーン移動
            SceneManager.LoadScene("GameClear");
        }

        if(HitCount == MAXHitCount)
        {
            BossHP -= HardDamage;
            HpGauge.fillAmount -= HardDamage / BossMaxHP;

            HitCount = 0;
        }
    }
}
