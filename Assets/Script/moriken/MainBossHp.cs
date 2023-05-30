using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainBossHp : MonoBehaviour
{
    [SerializeField] private GameObject GaugeObj;
    [SerializeField] private GameObject damageGaugeObj;
    [SerializeField] private GameObject allBossHpGauge;
    private Image HpGauge;
    private Image damageGauge;
    private Transform gaugeTrans;

    public float BossHP;
    private float BossMaxHP;
    private float damage;
    [System.NonSerialized]
    public bool DifferenceFlag;

    private int flame;
    [SerializeField] private float Decreaseflame;

    // �G�t�F�N�g
    [Tooltip("��_���[�W��")]
    [SerializeField] private GameObject explosionEffect;
    [Tooltip("�ʏ�_���[�W��")]
    [SerializeField] private GameObject NormalHiteffect;

    // ��_���[�W��
    public float HardDamage;

    //���񓖂�������
    public int MAXHitCount;
    private bool hit;

    // 
    [SerializeField]
    private float speed;
    [SerializeField]
    private float vibration;
    [SerializeField]
    private float vibrationFlame;

    Vector3 gaugePos;
    private Vector3 gaugePosoffset;

    public bool BreakFlag { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        BossMaxHP = BossHP;
        HpGauge = GaugeObj.GetComponent<Image>();
        HpGauge.fillAmount = 1;
        gaugeTrans = allBossHpGauge.GetComponent<Transform>();

        // �_���[�W���������Ƃ��̃Q�[�W
        damageGauge = damageGaugeObj.GetComponent<Image>();
        damageGauge.fillAmount = HpGauge.fillAmount;

        gaugePos = gaugeTrans.position;
        gaugePosoffset = gaugePos;          // �{�X��HP�Q�[�W�̏����ʒu��ۑ�

        flame = 0;
        hit = false;
        DifferenceFlag = false;

        BreakFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            flame++;

            //if (flame < vibrationFlame)
            //{
            //    gaugeTrans.position = PerlinNoise();
            //}
            //else
            //     gaugeTrans.position = gaugePosoffset;
        }

        // �̗͌�������
        if (DifferenceFlag)
        {
            if (Decreaseflame < flame)
            {
                if (HpGauge.fillAmount <= damageGauge.fillAmount)
                    damageGauge.fillAmount -= 0.0005f * Time.timeScale;
                else
                {
                    DifferenceFlag = false;
                    damageGauge.fillAmount = HpGauge.fillAmount;
                    flame = 0;
                    hit = false;
                }
            }
        }
    }

    public void Damage(Collision collision)
    {
        // "Enemy"�^�O�����Ă���I�u�W�F�N�g�ɂ���"PlayerDamage"�ϐ����󂯂Ƃ�
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage * Time.timeScale;
        HpGauge.fillAmount -= damage / BossMaxHP;

        // �����������Ƀt���O��True�ɂ���
        hit = true;
        DifferenceFlag = true;

        // �G�t�F�N�g����
        GameObject InstantiateEffect
            = GameObject.Instantiate(NormalHiteffect, collision.transform.position, Quaternion.identity);

        if (BossHP <= 0)
        {
            BreakFlag = true;
            //�V�[���ړ�
            //SceneManager.LoadScene("GameClear");
            // SceneMoveManager���^�O����
            //GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            //// �V�[���̊J�n
            //obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE);
            //obj.GetComponent<SceneMoveManager>().SceneStartUnload();
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
        BossHP -= HardDamage * Time.timeScale;
        HpGauge.fillAmount -= HardDamage / BossMaxHP;

        SoundManager.instance.PlaySE("WeakPoint");
        VibrationManager.instance.StartCoroutine("PlayVibration", "WeakPoint");
    }

    private Vector3 PerlinNoise()
    {
        gaugePos = gaugeTrans.position;

        gaugePos.x += GetNoise();   // �u�����Q�[�W�̈ʒu�ɉ��Z���Ă���
        gaugePos.y += GetNoise();

        return gaugePos;
    }

    private float GetNoise()
    {
        var perlinNoise = 2 * (Mathf.PerlinNoise(Time.time, 0) - 0.5f);

        return perlinNoise * vibration;
    }
}
