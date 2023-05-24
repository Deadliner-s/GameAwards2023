using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    private PhaseManager.Phase currentPhase;    // PhaseManagerのcurrentPhase
    private PhaseManager.Phase nextPhase;       // PhaseManagerのnextPhase

    [Tooltip("ブラー")]
    [SerializeField]
    private Volume volume;                      // 画面効果を参照する
    private RadialBlurVolume Blur;              // RadialBlurの中身を使用する
    private GaussianBlurVolume gBlur;
    public float BlurStrength;                  // RadialBlurの強さ
    public float gBlurStrength;                 // GaussianBlurの強さ
    private float BlurCount;                    // RadialBlurを何回かけたか
    private float gBlurCount;                   // GaussianBlurを何回かけたか
    private float CountPoint = 0.001f;
    private int flame;
    private int gFlame;

    private GameObject playerManager;           // Playerの外部参照
    private bool ManeverEnd;                    // クイックマニューバが終わった判定

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");

        currentPhase = PhaseManager.instance.GetPhase();
        nextPhase = currentPhase;

        // ブラーの初期化処理
        if (volume)
        {
            volume.profile.TryGet(out Blur);
            volume.profile.TryGet(out gBlur);
            BlurCount = BlurStrength * 1000;
            gBlurCount = gBlurStrength * 1000;
            Blur.strength.value = 0.0f;
            gBlur.SamplingDistance.value = 0.0f;
        }
        else
        {
            Debug.Log("volumeがありません");
        }

        flame = 0;
        gFlame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentPhase = PhaseManager.instance.GetPhase();

        // フェイズが変わった時の処理 
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;

            Debug.Log("初期化");
            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                Blur.strength.value = 0.0f;
                gBlur.SamplingDistance.value = 0.0f;
                flame = 0;
                gFlame = 0;
                ManeverEnd = false;
            }

            if (currentPhase == PhaseManager.Phase.Attack_Phase
             || currentPhase == PhaseManager.Phase.Normal_Phase)
            {
                Blur.strength.value = 0.0f;
                flame = 0;
                gFlame = 0;
                ManeverEnd = false;
            } 
        }

        // RadialBlurの処理
        if (flame < BlurCount && volume != null)
        {
            // マニューバ後のブラーを消していく処理
            if (ManeverEnd == true)
            {
                Blur.strength.value -= CountPoint * 5;
                flame += 5;
            }
        }

        // GaussianBlurの処理
        if (gFlame < gBlurCount && volume != null)
        {
            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                Debug.Log("ブラーをかけています");
                // ブラーをだんだんとかけていく処理
                gBlur.SamplingDistance.value += CountPoint;
                gFlame++;

                // SamplingDistanceが設定した数値よりも大きくならないように
                if (gBlur.SamplingDistance.value >= gBlurStrength)
                    gBlur.SamplingDistance.value = gBlurStrength;
            }

            if (currentPhase == PhaseManager.Phase.Attack_Phase
             || currentPhase == PhaseManager.Phase.Normal_Phase)
            {
                // ブラーをだんだんと0にしていく処理
                gBlur.SamplingDistance.value -= CountPoint * 2;
                gFlame += 2;

                // SamplingDistanceが0よりも小さくならないように
                if (gBlur.SamplingDistance.value <= 0.0f)
                    gBlur.SamplingDistance.value = 0.0f;
            }
        }

        // マニューバが行われたとき
        if (playerManager != null)
        {
            if (playerManager.GetComponent<PlayerMove>().maneverFlg == true && volume != null)
            {
                Blur.strength.value = BlurStrength;
                flame = 0;
                ManeverEnd = true;
            }
        }
    }
}
