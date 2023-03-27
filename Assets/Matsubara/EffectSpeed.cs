using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpeed : MonoBehaviour
{
    [Tooltip("通常")]
    [SerializeField] private float SpeedNormal = 80.0f;
    [Tooltip("パーティクル数")]
    [SerializeField] private float EmissionNormal = 80.0f;

    [Tooltip("アタック")]
    [SerializeField] private float SpeedAttack = 80.0f;
    [Tooltip("パーティクル数")]
    [SerializeField] private float EmissionAttack = 80.0f;

    [Tooltip("ハイスピード")]
    [SerializeField] private float SpeedHighSpeed = 300.0f;
    [Tooltip("パーティクル数")]
    [SerializeField] private float EmissionHighSpeed = 250.0f;


    [Tooltip("生成するパーティクルの種類")]
    [SerializeField] ParticleSystem particle;
    private PhaseManager.Phase PhaseFlg; // フェーズフラグ

    // Start is called before the first frame update
    void Start()
    {
        // フェーズ取得用
        PhaseFlg = PhaseManager.instance.GetPhase();

        // 通常フェーズを代入
        PhaseFlg = PhaseManager.Phase.Normal_Phase;
    }

    // Update is called once per frame
    void Update()
    {   
        // フェーズ取得用
        PhaseFlg = PhaseManager.instance.GetPhase();
        var main = particle.main;
        var emission = particle.emission;
        var renderer = particle.GetComponent<Renderer>();

        // フェーズによって切り替え
        // 通常
        if (PhaseFlg == PhaseManager.Phase.Normal_Phase)
        {
            main.startSpeed = SpeedNormal;
            emission.rateOverTime = EmissionNormal ;
        }
        // アタック
        if (PhaseFlg == PhaseManager.Phase.Attack_Phase)
        {
            main.startSpeed = SpeedAttack;
            emission.rateOverTime = EmissionAttack;
        }
        // ハイスピード
        if (PhaseFlg == PhaseManager.Phase.Speed_Phase)
        {
            main.startSpeed = SpeedHighSpeed;
            emission.rateOverTime = EmissionHighSpeed;
        }
    }
}
