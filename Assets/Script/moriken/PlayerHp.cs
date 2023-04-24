using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public GameObject GaugeObj;
    Image HpGauge;
    public GameObject DamageObj;
    Image DamageGauge;

    public float PlayerHP;
    float PlayerMaxHp;

    float Difference;

    public float HealAmount;
    float damage;
    
    public float MaxInvflame;
    float Invflame;

    float Decreaseflame;

    float i;

    public float RepairShieldflame;

    bool UseFlag;
    bool HealFlag;
    bool BreakShieldFlag;
    public bool BreakFlag { get; private set; }

    public GameObject Canvas;

    // hex_Shieldコンポーネント
    private HexShield hs;

    // PlayerHPオブジェクト
    [SerializeField]
    Transform PlayerHPGaugeTrans;

    HPGauge[] HpGaugecomponents;

    public bool BreakShield;

    SphereCollider collider;

    // シーン読込用
//    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(true);
        PlayerMaxHp = PlayerHP;
        HpGauge = GaugeObj.GetComponent<Image>();
        HpGauge.fillAmount = 1;
        // ダメージを喰らったときのゲージ
        DamageGauge = DamageObj.GetComponent<Image>();
        DamageGauge.fillAmount = 1;

        //フラグを非表示判定
        UseFlag = false;
        HealFlag = false;
        BreakShieldFlag = false;
        BreakFlag = false;

        Decreaseflame = 30;

        BreakShield = false;
        collider = gameObject.GetComponent<SphereCollider>();
        collider.enabled = false;

        // PlayerHpコオブジェクトのHpGaugeコンポーネントを全て取得する
        HpGaugecomponents = PlayerHPGaugeTrans.GetComponentsInChildren<HPGauge>();
        // シールドオブジェクトからHexShieldコンポーネントを取得
        hs = this.gameObject.GetComponent<HexShield>();

        // 非同期処理でシーンの遷移実行(現在実行しているシーンのバックグラウンドで次のシーンの読み込みを事前に行う)
//        async = SceneManager.LoadSceneAsync("GameOver");
        // シーンを読み込み終わってもシーン遷移は行わない状態にする
//        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 回復開始までの時間管理
        if (UseFlag == true)
        {
            Invflame++;
        }

        // 体力減少処理
        if(Decreaseflame < Invflame)
        {
            if(Difference != i)
            {
                DamageGauge.fillAmount -= 0.001f;
                i += 0.001f;
            }
            else
            {
                i = 0.0f;
                Difference = 0.0f;
            }
        }

        // 回復時間に関する処理
        if(BreakShieldFlag)
        {
            // シールドが復活
            if (RepairShieldflame < Invflame)
            {
                BreakShieldFlag = false;
                BreakShield = false;
                HealFlag = true;
                Invflame = 0;
            }
        }
        else if (MaxInvflame < Invflame)
        {
            UseFlag = false;
            HealFlag = true;
            Invflame = 0;
        }

        if (HealFlag)
        {
            PlayerHP += HealAmount;
            HpGauge.fillAmount = PlayerHP / PlayerMaxHp;
            DamageGauge.fillAmount = PlayerHP / PlayerMaxHp;

            hs.ChangeShieldColor(PlayerHP, PlayerMaxHp);
            foreach (HPGauge comp in HpGaugecomponents)
            {
                comp.ChangeHpGaugeColor(PlayerHP, PlayerMaxHp);
            }

            if (PlayerHP >= PlayerMaxHp)
            {
                HealFlag = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            // 完全に撃墜された判定にする
            BreakFlag = true;
            PlayerHP = 0;
            // UI関連を消す
            Canvas.SetActive(false);
        }
        
        // 当たり判定関連の処理
        if (BreakShield == true)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(collision);
        }
    }

    public void Damage(Collision collision)
    {
        // シールドが破壊されているとき
        if (BreakShieldFlag)
        {
            // 完全に撃墜された判定にする
            BreakFlag = true;

            Canvas.SetActive(false);
            //Destroy(this.gameObject);
            //シーン移動
            //SceneManager.LoadScene("GameOver");
            //async.allowSceneActivation = true;
        }

        HealFlag = false;

        // "Enemy"タグがついているオブジェクトにある"PlayerDamage"変数を受けとる
        damage = collision.gameObject.GetComponent<Damage>().PlayerDamage;
        PlayerHP -= damage;
        DamageGauge.fillAmount = HpGauge.fillAmount;
        HpGauge.fillAmount -= damage / PlayerMaxHp;

        Difference = HpGauge.fillAmount - DamageGauge.fillAmount;

        // 体力が0以下になったら
        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            BreakShieldFlag = true;
            BreakShield = true;
            UseFlag = true;
            Invflame = 0;
        }
        // 体力が0よりも多い時
        else if (PlayerHP > 0)
        {
            UseFlag = true;
            Invflame = 0;
        }

        hs.ChangeShieldColor(PlayerHP, PlayerMaxHp);

        foreach (HPGauge comp in HpGaugecomponents)
        {
            comp.ChangeHpGaugeColor(PlayerHP, PlayerMaxHp);
        }
    }
}
