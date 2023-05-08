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

    // �G�t�F�N�g
    [SerializeField]
    [Tooltip("��_���[�W��")]
    private GameObject explosionEffect;
    [SerializeField]
    [Tooltip("�ʏ�_���[�W��")]
    private GameObject NormalHiteffect;

    // ��_���[�W��
    public float HardDamage;

    //���񓖂�������
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
        // "Enemy"�^�O�����Ă���I�u�W�F�N�g�ɂ���"PlayerDamage"�ϐ����󂯂Ƃ�
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage;
        HpGauge.fillAmount -= damage/BossMaxHP;

        // �G�t�F�N�g����
        GameObject InstantiateEffect
            = GameObject.Instantiate(NormalHiteffect, collision.transform.position, Quaternion.identity);

        if (BossHP <= 0)
        {
            //�V�[���ړ�
            SceneManager.LoadScene("GameClear");
        }
    }

    public void ExplosionSet(Collision collision)
    {
        // �G�t�F�N�g����
        GameObject InstantiateEffect 
            = GameObject.Instantiate(explosionEffect, collision.transform.position, Quaternion.identity);

        // �\������
        //Destroy(InstantiateEffect, 10.0f);

        // hp�X�V
        BossHP -= HardDamage;
        HpGauge.fillAmount -= HardDamage / BossMaxHP;

        SoundManager.instance.PlaySE("WeakPoint");
    }
}
