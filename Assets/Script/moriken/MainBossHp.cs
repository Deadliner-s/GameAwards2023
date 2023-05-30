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

    // エフェクト
    [Tooltip("大ダメージ時")]
    [SerializeField] private GameObject explosionEffect;
    [Tooltip("通常ダメージ時")]
    [SerializeField] private GameObject NormalHiteffect;

    // 大ダメージ数
    public float HardDamage;

    //何回当たったら
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

        // ダメージを喰らったときのゲージ
        damageGauge = damageGaugeObj.GetComponent<Image>();
        damageGauge.fillAmount = HpGauge.fillAmount;

        gaugePos = gaugeTrans.position;
        gaugePosoffset = gaugePos;          // ボスのHPゲージの初期位置を保存

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

        // 体力減少処理
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
        // "Enemy"タグがついているオブジェクトにある"PlayerDamage"変数を受けとる
        damage = collision.gameObject.GetComponent<Damage>().EnemyDamage;
        BossHP -= damage * Time.timeScale;
        HpGauge.fillAmount -= damage / BossMaxHP;

        // 当たった時にフラグをTrueにする
        hit = true;
        DifferenceFlag = true;

        // エフェクト生成
        GameObject InstantiateEffect
            = GameObject.Instantiate(NormalHiteffect, collision.transform.position, Quaternion.identity);

        if (BossHP <= 0)
        {
            BreakFlag = true;
            //シーン移動
            //SceneManager.LoadScene("GameClear");
            // SceneMoveManagerをタグ検索
            //GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            //// シーンの開始
            //obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE);
            //obj.GetComponent<SceneMoveManager>().SceneStartUnload();
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
        BossHP -= HardDamage * Time.timeScale;
        HpGauge.fillAmount -= HardDamage / BossMaxHP;

        SoundManager.instance.PlaySE("WeakPoint");
        VibrationManager.instance.StartCoroutine("PlayVibration", "WeakPoint");
    }

    private Vector3 PerlinNoise()
    {
        gaugePos = gaugeTrans.position;

        gaugePos.x += GetNoise();   // ブレをゲージの位置に加算していく
        gaugePos.y += GetNoise();

        return gaugePos;
    }

    private float GetNoise()
    {
        var perlinNoise = 2 * (Mathf.PerlinNoise(Time.time, 0) - 0.5f);

        return perlinNoise * vibration;
    }
}
