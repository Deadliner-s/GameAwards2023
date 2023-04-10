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

    // ��_���[�W��
    public float HardDamage;

    //���񓖂�������
    public int MAXHitCount;
    int HitCount;

    // ���t���[����Ƀ_���[�W�\��
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
        // �q�b�g�񐔂�Max�Ɠ�����������
        if (HitCount == MAXHitCount)
        {
            // HardDamageFlame�ɂȂ�����
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
        // "Enemy"�^�O�����Ă���I�u�W�F�N�g�ɂ���"PlayerDamage"�ϐ����󂯂Ƃ�
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage;
        HpGauge.fillAmount -= damage/BossMaxHP;
        HitCount++;
        flame = 0;

        if (BossHP <= 0)
        {
            //�V�[���ړ�
            SceneManager.LoadScene("GameClear");
        }
    }
}
