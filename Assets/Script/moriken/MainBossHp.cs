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
        // "Enemy"�^�O�����Ă���I�u�W�F�N�g�ɂ���"PlayerDamage"�ϐ����󂯂Ƃ�
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage;
        HpGauge.fillAmount -= damage/BossMaxHP;

        if(BossHP <= 0)
        {
            //�V�[���ړ�
            SceneManager.LoadScene("GameClear");
        }
    }
}
