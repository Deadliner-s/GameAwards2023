using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PhaseManager : MonoBehaviour
{  
    // フェイズ列挙体
    public enum Phase
    {
        Normal_Phase,
        Speed_Phase,
        Attack_Phase,
        MAX_Phase
    }

    [Header("フェイズの時間")]
    public float NormalTime = 10.0f;
    public float SpeedTime = 10.0f;
    public float AttackTime = 10.0f;

    [Header("現在のフェイズ(初期フェイズ)")]
    public Phase currentPhase = Phase.Normal_Phase;  // 現在のフェイズ
    private Phase nextPhase;                         // 次のフェイズ

    private float time = 0.0f;                       // 秒数カウント用

    [Header("フェイズ毎に管理するオブジェクト")]
    [Tooltip("照準")]
    public GameObject Reticle;

    [Tooltip("集中線")]
    public GameObject Line;
    private Animator LineAnime;

    [Tooltip("ブラー")]
    [SerializeField]
    private Volume volume;
    private RadialBlurVolume Blur;
    public float BlurStrength;
    private float BlurCount;
    private float CountPoint = 0.001f;
    private int i;

    [Tooltip("マニューバ中ブラーに関するオブジェクト")]
    [SerializeField]
    private GameObject player;
    private PlayerMove playerMove;
    private bool ManeverEnd;

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
        if (Line)
        {
            LineAnime = Line.GetComponent<Animator>();
            Line.SetActive(false);
            LineAnime.SetBool("isMove", false);
        }

        // ブラーの初期化処理
        volume.profile.TryGet(out Blur);
        BlurCount = BlurStrength * 1000;
        Blur.strength.value = 0.0f;

        playerMove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズが固定されてない場合
        if (Debug_FixPhaseFlg != true)
        {
            // 時間更新
            time += Time.deltaTime;

            // 通常フェイズ
            if (currentPhase == Phase.Normal_Phase)
            {
                if (time >= NormalTime)
                {
                    currentPhase = Phase.Speed_Phase;
                    time = 0.0f;
                }
            }
            // ハイスピードフェイズ
            else if (currentPhase == Phase.Speed_Phase)
            {
                if (time >= SpeedTime)
                {
                    currentPhase = Phase.Attack_Phase;
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


        // フェイズが変わった場合
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;

            if (currentPhase == Phase.Normal_Phase)
            {
                Reticle.SetActive(true);
                if (Line)
                {
                    Line.SetActive(false);
                    LineAnime.SetBool("isMove", false);
                }
                Blur.strength.value = 0.0f;
            }

            if (currentPhase == Phase.Speed_Phase)
            {
                SoundManager.instance.PlaySE("HighSpeed");
                Reticle.SetActive(false);
                if (Line)
                {
                    Line.SetActive(true);
                    LineAnime.SetBool("isMove", true);
                }
                vibrationManager.GetComponent<VibrationManager>().StartCoroutine("PlayVibration", "HighSpeed");

                i = 0;
            }

            if (currentPhase == Phase.Attack_Phase)
            {
                Reticle.SetActive(true);
                if (Line)
                {
                    Line.SetActive(false);
                    LineAnime.SetBool("isMove", false);
                }

                i = 0;
            }
        }

        // フェーズ変更後の処理
        if (i < BlurCount)
        {
            if (currentPhase == Phase.Speed_Phase)
            {
                if (ManeverEnd == true)
                {
                    Blur.strength.value -= CountPoint;
                    i+= 2;
                }
                else
                {
                    // ブラーをだんだんとかけていく処理
                    Blur.strength.value += CountPoint;
                    i++;
                }

                if(Blur.strength.value == BlurStrength)
                {
                    Debug.Log("ブラー完了");
                }
            }
            if (currentPhase == Phase.Attack_Phase)
            {
                // ブラーをだんだんと0にしていく処理
                Blur.strength.value -= CountPoint;
                i += 2;

                if (ManeverEnd == true)
                {
                    Blur.strength.value -= CountPoint;
                }
            }
        }
        else if (currentPhase == Phase.Speed_Phase || currentPhase == Phase.Attack_Phase)
        {
            if (currentPhase == Phase.Attack_Phase)
            {
                Blur.strength.value = BlurStrength;
                ManeverEnd = false;
            }
            if (currentPhase == Phase.Attack_Phase)
            {
                Blur.strength.value = 0.0f;
                ManeverEnd = false;
            }
        }

        // マニューバが行われたとき
        if (playerMove.maneverFlg == true)
        {
            if (currentPhase == Phase.Speed_Phase)
            {
                i = 0;
                Blur.strength.value = BlurStrength + 0.1f;
                ManeverEnd = true;
            }
            if (currentPhase == Phase.Attack_Phase)
            {
                i = 0;
                Blur.strength.value = BlurStrength;
                ManeverEnd = true;
            }
        }
    }

    public Phase GetPhase()
    {
        return currentPhase;
    }
}