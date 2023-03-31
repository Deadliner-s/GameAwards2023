using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float time = 0.0f;                       // 秒数カウント用

    [Header("フェイズ毎に管理するオブジェクト")]
    [Tooltip("照準")]
    public GameObject Reticle;


    [Header("デバッグ用 フェイズを固定する")]
    public bool Debug_FixPhaseFlg = false;

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

        Reticle.SetActive(true);
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

                Reticle.SetActive(true);
            }
            // ハイスピードフェイズ
            else if (currentPhase == Phase.Speed_Phase)
            {
                if (time >= SpeedTime)
                {
                    currentPhase = Phase.Attack_Phase;
                    time = 0.0f;
                }

                Reticle.SetActive(false);
            }
            // アタックフェイズ
            else if (currentPhase == Phase.Attack_Phase)
            {
                if (time >= AttackTime)
                {
                    currentPhase = Phase.Normal_Phase;
                    time = 0.0f;
                }

                Reticle.SetActive(true);
            }
        }
    }

    public Phase GetPhase()
    {
        return currentPhase;
    }
}
