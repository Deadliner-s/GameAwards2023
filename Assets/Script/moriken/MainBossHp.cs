using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MainBossHp : MonoBehaviour
{
    public GameObject GaugeObj;
    Slider HpGauge;

    public float BossHP;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        HpGauge = GaugeObj.GetComponent<Slider>();
        HpGauge.maxValue = BossHP;
        HpGauge.value = BossHP;
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
        HpGauge.value -= damage;
    }
}
