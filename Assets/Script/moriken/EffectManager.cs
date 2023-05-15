using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    public GameObject phaseManagerObj;
    private PhaseManager phaseManager;

    [Tooltip("ブラー")]
    [SerializeField]
    private Volume volume;
    private RadialBlurVolume Blur;
    private GaussianBlurVolume gBlur;
    public float BlurStrength;
    public float gBlurStrength;
    private float BlurCount;
    private float CountPoint = 0.001f;
    private int i;

    [Tooltip("マニューバ中ブラーに関するオブジェクト")]
    [SerializeField]
    private GameObject player;
    private PlayerMove playerMove;
    private bool ManeverEnd;

    // Start is called before the first frame update
    void Start()
    {
        phaseManager = phaseManagerObj.GetComponent<PhaseManager>();

        // ブラーの初期化処理
        if (volume)
        {
            volume.profile.TryGet(out Blur);
            volume.profile.TryGet(out gBlur);
            BlurCount = BlurStrength * 1000;
            Blur.strength.value = 0.0f;
            gBlur.SamplingDistance.value = 0.0f;
        }
        else
        {
            Debug.Log("volumeがありません");
        }

        playerMove = player.GetComponent<PlayerMove>();
        if (playerMove == null)
        {
            Debug.Log("playerがありません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            i = 0;
        }
        if(phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase)
        {
            i = 0;
        }

        // フェーズ変更後の処理
        if (i < BlurCount && volume != null)
        {
            if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                if (ManeverEnd == true)
                {
                    if (volume)
                    {
                        Blur.strength.value -= CountPoint * 5;
                    }
                    i += 5;
                }
                else
                {
                    // ブラーをだんだんとかけていく処理
                    if (gBlur.SamplingDistance.value != gBlurStrength)
                    {
                        if (volume)
                        {
                            gBlur.SamplingDistance.value += CountPoint;
                        }
                        i++;
                    }
                }


                if (Blur.strength.value == BlurStrength && volume != null)
                {
                    Debug.Log("ブラー完了");
                }
            }
            if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase && volume != null)
            {
                if (ManeverEnd == true)
                {
                    // マニューバ後のブラーを消していく処理
                    Blur.strength.value -= CountPoint * 5;
                    i += 5;
                }
                else
                {
                    //// ブラーをだんだんと0にしていく処理
                    gBlur.SamplingDistance.value -= CountPoint;
                    i += 2;
                }
            }
        }
        else if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase || phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase && volume != null)
        {
            if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                Blur.strength.value = 0.0f;
                ManeverEnd = false;
            }
            if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                Blur.strength.value = 0.0f;
                gBlur.SamplingDistance.value = 0.0f;
                ManeverEnd = false;
            }
        }

        // マニューバが行われたとき
        if (playerMove != null)
        {
            if (playerMove.maneverFlg == true)
            {
                i = 0;
                if (volume)
                {
                    Blur.strength.value = BlurStrength;
                }
                ManeverEnd = true;
            }
        }
    }
}
