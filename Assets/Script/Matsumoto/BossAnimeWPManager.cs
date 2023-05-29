using UnityEngine;

public class BossAnimeWPManager : MonoBehaviour
{
    private GameObject boss;                                        // ボスのオブジェクト
    private GameObject playerManager;                               // プレイヤーマネージャー
    private GameObject bossManager;                                 // ボスマネージャー

    private GameObject[] BossWing = new GameObject[4];              // ボスの羽の弱点
    private Animator[] BossWing_Anime = new Animator[4];            // ボスの羽のアニメーション
    private GameObject[] WP_Bottom = new GameObject[4];             // ボスの下の弱点
    private Animator[] WP_Bottom_Anime = new Animator[4];           // ボスの下のアニメーション

    public GameObject WP_Top_Prefab;                                // ボスの上の弱点のコライダー付きプレハブ
    public GameObject WP_Bottom_Prefab;                             // ボスの下の弱点のコライダー付きプレハブ
    private GameObject[] WP_Top_Collision = new GameObject[4];      // ボスの上の弱点のコライダーオブジェクト
    private GameObject[] WP_Bottom_Collision = new GameObject[4];   // ボスの下の弱点のコライダーオブジェクト

    private GameObject[] WingWP_PosObj = new GameObject[4];         // ボスの羽の弱点の位置オブジェクト

    private PhaseManager.Phase currntPhase;                         // 現在のフェイズ
    private PhaseManager.Phase nextPhase;                           // 次のフェイズ
    private PhaseManager.Phase NormaltoNext;                        // 次のフェイズ


    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        playerManager = GameObject.Find("PlayerManager");
        bossManager = GameObject.Find("BossManager");

        // ボスの羽のオブジェクトの取得
        BossWing[0] = boss.transform.Find("wing1").gameObject;
        BossWing[1] = boss.transform.Find("wing2").gameObject;
        BossWing[2] = boss.transform.Find("wing3").gameObject;
        BossWing[3] = boss.transform.Find("wing4").gameObject;

        // ボスの羽の弱点の位置オブジェクトの取得
        WingWP_PosObj[0] = GameObject.Find("Wing_wp1").gameObject;
        WingWP_PosObj[1] = GameObject.Find("Wing_wp2").gameObject;
        WingWP_PosObj[2] = GameObject.Find("Wing_wp3").gameObject;
        WingWP_PosObj[3] = GameObject.Find("Wing_wp4").gameObject;

        // ボスの下の弱点のオブジェクトの取得
        WP_Bottom[0] = boss.transform.Find("weak1").gameObject;
        WP_Bottom[1] = boss.transform.Find("weak2").gameObject;
        WP_Bottom[2] = boss.transform.Find("weak3").gameObject;
        WP_Bottom[3] = boss.transform.Find("weak4").gameObject;

        // アニメーションの取得
        for (int i = 0; i < 4; i++)
        {
            BossWing_Anime[i] = BossWing[i].GetComponent<Animator>();
        }
        for (int i = 0; i < 4; i++)
        {
            WP_Bottom_Anime[i] = WP_Bottom[i].GetComponent<Animator>();
        }

        // フェイズの初期化
        currntPhase = PhaseManager.instance.GetPhase();
        NormaltoNext = PhaseManager.instance.GetNormaltoNext();

        //nextPhase = currntPhase;
        nextPhase = NormaltoNext;
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズの取得
        currntPhase = PhaseManager.instance.GetPhase();
        NormaltoNext = PhaseManager.instance.GetNormaltoNext();

        // フェイズが変わったら
        if (nextPhase != currntPhase)
        {
            // フェイズの更新
            nextPhase = currntPhase;


            // ノーマルフェイズ
            if (currntPhase == PhaseManager.Phase.Normal_Phase && NormaltoNext == PhaseManager.Phase.First_Normal)
            {
                // アニメーション上
                for(int i = 0; i < 4; i++)
                {
                    BossWing_Anime[i].SetBool("isWing", true);
                }
                // アニメーション下
                //for(int i = 0; i < 4; i++)
                //{
                //}
            }
            // ノーマルフェイズの次がアタックフェーズ
            else if (currntPhase == PhaseManager.Phase.Normal_Phase && NormaltoNext == PhaseManager.Phase.Attack_Phase)
            {
                // アニメーション上
                for (int i = 0; i < 4; i++)
                {
                    BossWing_Anime[i].SetBool("isBinder", true);
                }
                // アニメーション下
                for (int i = 0; i < 4; i++)
                {
                    WP_Bottom_Anime[i].SetBool("isOpen", true);
                    WP_Bottom_Anime[i].SetBool("isClose", false);
                    WP_Bottom_Anime[i].SetBool("isMove", false);
                }
            }
            //　ノーマルフェイズの次がスピードフェイズ
            else if (currntPhase == PhaseManager.Phase.Normal_Phase && NormaltoNext == PhaseManager.Phase.Speed_Phase)
            {
                // アニメーション上
                for (int i = 0; i < 4; i++)
                {
                    BossWing_Anime[i].SetBool("isBinder", false);
                }
                // アニメーション下
                for (int i = 0; i < 4; i++)
                {
                    WP_Bottom_Anime[i].SetBool("isOpen", false);
                    WP_Bottom_Anime[i].SetBool("isClose", true);
                    WP_Bottom_Anime[i].SetBool("isMove", true);
                }

                // 生成されている場合、ボスの弱点のコライダーを削除
                for (int i = 0; i < 4; i++)
                {
                    if (WP_Top_Collision[i] != null)
                    {
                        Destroy(WP_Top_Collision[i]);
                    }
                    if (WP_Bottom_Collision[i] != null)
                    {
                        Destroy(WP_Bottom_Collision[i]);
                    }
                }
            }
            if (currntPhase == PhaseManager.Phase.Attack_Phase)
            {
                // ボスの弱点のコライダーオブジェクトを生成
                for (int i = 0; i < 4; i++)
                {
                    if (WP_Top_Collision[i] == null)
                    {
                        WP_Top_Collision[i] = Instantiate(WP_Top_Prefab, BossWing[i].transform.position, Quaternion.identity);
                    }
                    if (WP_Bottom_Collision[i] == null)
                    {
                        WP_Bottom_Collision[i] = Instantiate(WP_Bottom_Prefab, WingWP_PosObj[i].transform.position, Quaternion.identity);
                    }
                }
            }
        }

        // アタックフェイズ中
        if (currntPhase == PhaseManager.Phase.Attack_Phase)
        {
            // プレイヤーによって弱点が破壊されたら
            for (int i = 0; i < 4; i++)
            {
                if (WP_Top_Collision[i] == null)
                {
                    BossWing_Anime[i].SetBool("isBinder", false);
                }
                if (WP_Bottom_Collision[i] == null)
                {
                    WP_Bottom_Anime[i].SetBool("isOpen", false);
                    WP_Bottom_Anime[i].SetBool("isClose", true);
                    WP_Bottom_Anime[i].SetBool("isMove", true);
                }
            }
        }

        // プレイヤーのHPが0になった時、ボスの弱点のコライダーを削除
        if (playerManager.GetComponent<PlayerHp>().BreakFlag == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (WP_Top_Collision[i] != null)
                {
                    Destroy(WP_Top_Collision[i]);
                }
                if (WP_Bottom_Collision[i] != null)
                {
                    Destroy(WP_Bottom_Collision[i]);
                }
            }
        }

        // ボスの弱点のコライダーの位置を更新
        for (int i = 0; i < 4; i++)
        {
            if (WP_Top_Collision[i] != null)
            {
                WP_Top_Collision[i].transform.position = WingWP_PosObj[i].transform.position;
            }
            if (WP_Bottom_Collision[i] != null)
            {
                WP_Bottom_Collision[i].transform.position = WP_Bottom[i].transform.position;
            }
        }

        // プレイヤーかボスが撃墜された場合に消す
        if (playerManager.GetComponent<PlayerHp>().BreakFlag ||
            bossManager.GetComponent<MainBossHp>().BreakFlag)
        {
            // 生成されている場合、ボスの弱点のコライダーを削除
            for (int i = 0; i < 4; i++)
            {
                if (WP_Top_Collision[i] != null)
                {
                    Destroy(WP_Top_Collision[i]);
                }
                if (WP_Bottom_Collision[i] != null)
                {
                    Destroy(WP_Bottom_Collision[i]);
                }
            }
        }
    }
}
