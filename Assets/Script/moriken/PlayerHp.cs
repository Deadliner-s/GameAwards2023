using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    private GameObject player;
    private GameObject playerManager;

    public GameObject GaugeObj;     // プレイヤーのHPゲージオブジェクト

    Image HpGauge;
    public GameObject DamageObj;    // プレイヤーのダメージゲージオブジェクト
    Image DamageGauge;

    public float PlayerHP;          // プレイヤーのHP
    private float PlayerMaxHp;              // プレイヤーのHPの最大値

    [Tooltip("回復量")]
    [SerializeField] private float HealAmount;
    private float damage;

    private float flame;

    private float Decreaseflame;

    [Tooltip("シールド壊れてから回復するまでの時間")]
    [SerializeField] private float RepairShieldflame;

    private bool UseFlag;
    private bool HealFlag;
    private bool BreakShieldFlag;
    public bool BreakFlag { get; private set; }
    private bool DifferenceFlag;

    [Tooltip("撃墜シーンに入ったときに消すオブジェクト")]
    [SerializeField] private GameObject Canvas;

    // hex_Shieldコンポーネント
    private HexShield hs;

    // PlayerHPオブジェクト
    [SerializeField]
    private Transform PlayerHPGaugeTrans;

    private HPGauge[] HpGaugecomponents;

    private SphereCollider modelCollider;

    // シーン読込用
    //    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");

        Canvas.SetActive(true);
        PlayerMaxHp = PlayerHP;
        HpGauge = GaugeObj.GetComponent<Image>();
        HpGauge.fillAmount = 1;
        // ダメージを喰らったときのゲージ
        DamageGauge = DamageObj.GetComponent<Image>();
        DamageGauge.fillAmount = HpGauge.fillAmount;

        UseFlag = false;
        HealFlag = false;
        BreakShieldFlag = false;
        BreakFlag = false;
        DifferenceFlag = false;

        Decreaseflame = 30;


        modelCollider = player.GetComponent<SphereCollider>();

        if (modelCollider != null)
        {
            modelCollider.enabled = false;
        }

        // PlayerHpコオブジェクトのHpGaugeコンポーネントを全て取得する
        HpGaugecomponents = PlayerHPGaugeTrans.GetComponentsInChildren<HPGauge>();
        // シールドオブジェクトからHexShieldコンポーネントを取得
        hs = playerManager.GetComponent<HexShield>();

        // 非同期処理でシーンの遷移実行(現在実行しているシーンのバックグラウンドで次のシーンの読み込みを事前に行う)
        //        async = SceneManager.LoadSceneAsync("GameOver");
        // シーンを読み込み終わってもシーン遷移は行わない状態にする
        //        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerManager");
        }
        if (hs == null)
        {
            hs = playerManager.GetComponent<HexShield>();
        }


        if (player == null)
        {
            player = GameObject.Find("Player");
            modelCollider = player.GetComponent<SphereCollider>();
            //sphereCollider = player.GetComponent<SphereCollider>();
            // PlayerHpコオブジェクトのHpGaugeコンポーネントを全て取得する
            HpGaugecomponents = PlayerHPGaugeTrans.GetComponentsInChildren<HPGauge>();
            // シールドオブジェクトからHexShieldコンポーネントを取得
            hs = playerManager.GetComponent<HexShield>();
        } 

        // 回復開始までの時間管理
        if (UseFlag == true)
        {
            flame++;
        }

        // 体力減少処理
        if (DifferenceFlag)
        {
            if (Decreaseflame < flame)
            {
                if (HpGauge.fillAmount <= DamageGauge.fillAmount)
                    DamageGauge.fillAmount -= 0.005f * Time.timeScale;
                else
                {
                    DifferenceFlag = false;
                    DamageGauge.fillAmount = HpGauge.fillAmount;
                }
            }
        }

        // シールドが消滅したあとの回復時間に関する処理
        if (BreakShieldFlag)
        {
            // シールドが復活
            if (RepairShieldflame < flame)
            {
                BreakShieldFlag = false;
                HealFlag = true;
                flame = 0;
            }
        }

        // Hpの回復処理
        if (HealFlag)
        {
            PlayerHP += HealAmount * Time.timeScale;
            HpGauge.fillAmount = PlayerHP / PlayerMaxHp; ;

            //if (hs == null)
            //{
            //    hs = playerManager.GetComponent<HexShield>();
            //}
            hs.ChangeShieldColor(PlayerHP, PlayerMaxHp);
            foreach (HPGauge comp in HpGaugecomponents)
            {
                comp.ChangeHpGaugeColor(PlayerHP, PlayerMaxHp);
            }

            // プレイヤーのHPの最大値よりも大きくなった時回復をやめる
            if (PlayerHP >= PlayerMaxHp)
            {
                HealFlag = false;
            }
        }

        if (DebugCommandooo.instance.debugExplosionSet && Input.GetKeyDown(KeyCode.F1))
        {
            // 完全に撃墜された判定にする
            BreakFlag = true;
            PlayerHP = 0;
            // UI関連を消す
            Canvas.SetActive(false);
        }

        // 当たり判定関連の処理
        if (BreakShieldFlag)
        {
            // シールドが壊れた(PlayerHpが0になった)ら当たり判定を付ける
            //player.GetComponent<Collider>().enabled = true;
            if (modelCollider == null)
            {
                modelCollider = player.GetComponent<SphereCollider>();
            }
            if (modelCollider != null)
            {
                modelCollider.enabled = true;
            }
        }
        else
        {
            //player.GetComponent<Collider>().enabled = false;
            if (modelCollider == null)
            {
                modelCollider = player.GetComponent<SphereCollider>();
            }
            if (modelCollider != null)
            {
                modelCollider.enabled = false;
            }
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    // もし衝突した相手オブジェクトのタグが"Enemy"ならば中の処理を実行
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Damage(collision);
    //    }
    //}

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

            VibrationManager.instance.StartCoroutine("PlayVibration", "PlayerDeath");
        }


        HealFlag = false;
        
        // "Enemy"タグがついているオブジェクトにある"PlayerDamage"変数を受けとる
        damage = collision.gameObject.GetComponent<Damage>().PlayerDamage;
        PlayerHP -= damage * Time.timeScale;
        HpGauge.fillAmount -= damage / PlayerMaxHp;

        DifferenceFlag = true;

        // フラグ管理
        HealFlag = true;
        UseFlag = true;
        flame = 0;

        // 体力が0以下になったら
        if (PlayerHP <= 0)
        {
            SoundManager.instance.PlaySE("PlayerDeath");
            PlayerHP = 0;
            BreakShieldFlag = true;
        }
        // 体力が0よりも多い時
        else if (PlayerHP > 0)
        {
            if (collision.gameObject.name == "Cylinder")
            {
                // レーザーを受けた時のSEを再生(未設定
                VibrationManager.instance.StartCoroutine("PlayVibration", "Laser");
            }
            else
            {
                // ダメージを受けた時のSEを再生(レーザー以外、ミサイルとかだけ
                SoundManager.instance.PlaySE("PlayerDamage");
                VibrationManager.instance.StartCoroutine("PlayVibration", "PlayerDamage");
            }
        }

        //if (hs == null)
        //{
        //    hs = playerManager.GetComponent<HexShield>();
        //}
        hs.ChangeShieldColor(PlayerHP, PlayerMaxHp);

        foreach (HPGauge comp in HpGaugecomponents)
        {
            comp.ChangeHpGaugeColor(PlayerHP, PlayerMaxHp);
        }
    }
}
