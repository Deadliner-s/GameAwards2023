using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    public GameObject phaseManagerObj;
    private PhaseManager phaseManager;

    [Tooltip("�u���[")]
    [SerializeField]
    private Volume volume;
    private RadialBlurVolume Blur;
    private GaussianBlurVolume gBlur;
    public float BlurStrength;
    public float gBlurStrength;
    private float BlurCount;
    private float gBlurCount;
    private float CountPoint = 0.001f;
    private int flame;
    private int gFlame;
    private bool onece;

    [Tooltip("�}�j���[�o���u���[�Ɋւ���I�u�W�F�N�g")]
    [SerializeField]
    private GameObject player;
    private PlayerMove playerMove;
    private bool ManeverEnd;

    // Start is called before the first frame update
    void Start()
    {
        phaseManager = phaseManagerObj.GetComponent<PhaseManager>();

        // �u���[�̏���������
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
            Debug.Log("volume������܂���");
        }

        playerMove = player.GetComponent<PlayerMove>();
        if (playerMove == null)
        {
            Debug.Log("player������܂���");
        }

        flame = 0;
        gFlame = 0;
        onece = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseManager.nextPhase != phaseManager.currentPhase)
        {
            if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                Blur.strength.value = 0.0f;
                gBlur.SamplingDistance.value = 0.0f;
                flame = 0;
                gFlame = 0;
                ManeverEnd = false;
            }
            if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                Blur.strength.value = 0.0f;
                gBlur.SamplingDistance.value = 0.0f;
                flame = 0;
                gFlame = 0;
                ManeverEnd = false;
            }
        }

        // RadialBlur�̏���
        if (flame < BlurCount && volume != null)
        {
            if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                // �}�j���[�o��̃u���[�������Ă�������
                if (ManeverEnd == true)
                {
                    Blur.strength.value -= CountPoint * 5;
                    flame += 5;
                }
            }
            if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                // �}�j���[�o��̃u���[�������Ă�������
                if (ManeverEnd == true)
                {
                    Blur.strength.value -= CountPoint * 5;
                    flame += 5;
                }
            }
        }

        // GaussianBlur�̏���
        if (gFlame < gBlurCount && volume != null)
        {
            if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                // �u���[�����񂾂�Ƃ����Ă�������
                if (gBlur.SamplingDistance.value <= gBlurStrength)
                {
                    gBlur.SamplingDistance.value += CountPoint;
                    gFlame++;
                }
                else
                {
                    gBlur.SamplingDistance.value = gBlurStrength;
                }
            }

            if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                //// �u���[�����񂾂��0�ɂ��Ă�������
                gBlur.SamplingDistance.value -= CountPoint * 2;
                gFlame += 2;
            }
        }
        else
        if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase
            && gBlur.SamplingDistance.value > 0)
            gBlur.SamplingDistance.value = 0;

        // �}�j���[�o���s��ꂽ�Ƃ�
        if (playerMove != null)
        {
            if (playerMove.maneverFlg == true && volume != null)
            {
                if (phaseManager.currentPhase == PhaseManager.Phase.Speed_Phase
                || phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase)
                {
                    Blur.strength.value = BlurStrength;
                    flame = 0;
                    ManeverEnd = true;
                }
            }
        }
    }
}
