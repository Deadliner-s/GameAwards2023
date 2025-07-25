using UnityEngine;

public class PhaseManager : MonoBehaviour
{  
    // フェイズ列挙体
    public enum Phase
    {
        Normal_Phase,
        Speed_Phase, 
        Attack_Phase,
        First_Normal,
        MAX_Phase
    }

    [Header("フェイズの時間")]
    public float NormalTime = 10.0f;
    public float SpeedTime = 10.0f;
    public float AttackTime = 10.0f;
    public float First_NormalTime = 4.0f;

    private bool lastNormalFlg = false;

    [Header("現在のフェイズ(初期フェイズ)")]
    public Phase currentPhase = Phase.Normal_Phase;  // 現在のフェイズ
    [System.NonSerialized]
    public Phase nextPhase;                          // 次のフェイズ
    [Header("通常フェイズから次のフェイズ")]
    public Phase NormaltoNext = Phase.First_Normal;  // 通常フェイズから次のフェイズ

    private float time = 0.0f;                       // 秒数カウント用

    [Header("フェイズ毎に管理するオブジェクト")]
    [Tooltip("照準")]
    public GameObject Reticle;

    [Header("デバッグ用 フェイズを固定する")]
    public bool Debug_FixPhaseFlg = false;


    private GameObject vibrationManager;            // バイブレーションマネージャー

    // インスタンス
    public static PhaseManager instance;

    void Awake()
    {
        // インスタンスが存在しない場合
        if(instance == null)
        {
            // インスタンス生成
            instance = this;
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;

        //currentPhase = Phase.Normal_Phase;          // 初期フェイズ
        nextPhase = currentPhase;

        vibrationManager = GameObject.Find("VibrationManagerObj");

        // 初回通常フェイズ
        NormaltoNext = Phase.First_Normal;

        lastNormalFlg = false;
    }
    // Update is called once per frame
    void Update()
    {
        // ステージ1,2
        //if (SceneManager.GetActiveScene().name == "Stage1" || SceneManager.GetActiveScene().name == "Stage2")
        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
            SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2) 
        {
            // フェイズが固定されてない場合
            if (Debug_FixPhaseFlg != true)
            {
                // 時間更新
                time += Time.deltaTime;

                // 通常フェイズ
                if (currentPhase == Phase.Normal_Phase)
                {
                    // 初回通常フェイズの秒数処理
                    float nt = 999.0f;
                    if (NormaltoNext == Phase.First_Normal)
                        nt = First_NormalTime;
                    else if (NormaltoNext != Phase.First_Normal)
                        nt = NormalTime;

                    if (time >= nt)
                    {
                        // ステージ1,2はアタックフェイズから
                        if (NormaltoNext == Phase.First_Normal)
                        {
                            NormaltoNext = Phase.Attack_Phase;
                        }
                        if (NormaltoNext == Phase.Attack_Phase)
                        {
                            currentPhase = Phase.Attack_Phase;
                            NormaltoNext = Phase.Speed_Phase;
                        }
                        else if (NormaltoNext == Phase.Speed_Phase)
                        {
                            currentPhase = Phase.Speed_Phase;
                            NormaltoNext = Phase.Attack_Phase;
                        }

                        time = 0.0f;
                    }
                }
                // ハイスピードフェイズ
                else if (currentPhase == Phase.Speed_Phase)
                {
                    if (time >= SpeedTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
                // アタックフェイズ
                else if (currentPhase == Phase.Attack_Phase)
                {
                    if (time >= AttackTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
            }
            // 残り時間がn秒になったら通常フェイズに固定する
            if (lastNormalFlg == true)
            {
                currentPhase = Phase.Normal_Phase;
                Debug_FixPhaseFlg = true;
            }
        }
        // ステージ3
        else
        {
            // フェイズが固定されてない場合
            if (Debug_FixPhaseFlg != true)
            {
                // 時間更新
                time += Time.deltaTime;

                // 通常フェイズ
                if (currentPhase == Phase.Normal_Phase)
                {
                    // 初回通常フェイズの秒数処理
                    float nt = 999.0f;
                    if (NormaltoNext == Phase.First_Normal)
                        nt = First_NormalTime;
                    else if (NormaltoNext != Phase.First_Normal)
                        nt = NormalTime;

                    if (time >= nt)
                    {
                        // ステージ3はスピードフェイズから
                        if (NormaltoNext == Phase.First_Normal)
                        {
                            NormaltoNext = Phase.Speed_Phase;
                        }
                        if (NormaltoNext == Phase.Speed_Phase)
                        {
                            currentPhase = Phase.Speed_Phase;
                            NormaltoNext = Phase.Attack_Phase;
                        }
                        else if (NormaltoNext == Phase.Attack_Phase)
                        {
                            currentPhase = Phase.Attack_Phase;
                            NormaltoNext = Phase.Speed_Phase;
                        }

                        time = 0.0f;
                    }
                }
                // ハイスピードフェイズ
                else if (currentPhase == Phase.Speed_Phase)
                {
                    if (time >= SpeedTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
                // アタックフェイズ
                else if (currentPhase == Phase.Attack_Phase)
                {
                    if (time >= AttackTime)
                    {
                        currentPhase = Phase.Normal_Phase;
                        time = 0.0f;
                    }
                }
            }
        }

        // フェイズが変わった場合
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;

            if (currentPhase == Phase.Normal_Phase)
            {
                Reticle.SetActive(false);
            }

            if (currentPhase == Phase.Speed_Phase)
            {
                SoundManager.instance.PlaySE("HighSpeed");
                Reticle.SetActive(false);
                
                vibrationManager.GetComponent<VibrationManager>().StartCoroutine("PlayVibration", "HighSpeed");
            }

            if (currentPhase == Phase.Attack_Phase)
            {
                Reticle.SetActive(true);
            }
        }
    }

    public Phase GetPhase()
    {
        return currentPhase;
    }

    public Phase GetNormaltoNext()
    {
        return NormaltoNext;
    }

    //public bool GetLastNormalFlg()
    //{
    //    return lastNormalFlg;
    //}
    public void SetLastNormalFlg(bool flg)
    {
        lastNormalFlg = flg;
    }
}