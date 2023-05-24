using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    private PhaseManager.Phase currentPhase;    // PhaseManager��currentPhase
    private PhaseManager.Phase nextPhase;       // PhaseManager��nextPhase

    [Tooltip("�u���[")]
    [SerializeField]
    private Volume volume;                      // ��ʌ��ʂ��Q�Ƃ���
    private RadialBlurVolume Blur;              // RadialBlur�̒��g���g�p����
    private GaussianBlurVolume gBlur;
    public float BlurStrength;                  // RadialBlur�̋���
    public float gBlurStrength;                 // GaussianBlur�̋���
    private float BlurCount;                    // RadialBlur�����񂩂�����
    private float gBlurCount;                   // GaussianBlur�����񂩂�����
    private float CountPoint = 0.001f;
    private int flame;
    private int gFlame;

    private GameObject playerManager;           // Player�̊O���Q��
    private bool ManeverEnd;                    // �N�C�b�N�}�j���[�o���I���������

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");

        currentPhase = PhaseManager.instance.GetPhase();
        nextPhase = currentPhase;

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

        flame = 0;
        gFlame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentPhase = PhaseManager.instance.GetPhase();

        // �t�F�C�Y���ς�������̏��� 
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;

            Debug.Log("������");
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

        // RadialBlur�̏���
        if (flame < BlurCount && volume != null)
        {
            // �}�j���[�o��̃u���[�������Ă�������
            if (ManeverEnd == true)
            {
                Blur.strength.value -= CountPoint * 5;
                flame += 5;
            }
        }

        // GaussianBlur�̏���
        if (gFlame < gBlurCount && volume != null)
        {
            if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                Debug.Log("�u���[�������Ă��܂�");
                // �u���[�����񂾂�Ƃ����Ă�������
                gBlur.SamplingDistance.value += CountPoint;
                gFlame++;

                // SamplingDistance���ݒ肵�����l�����傫���Ȃ�Ȃ��悤��
                if (gBlur.SamplingDistance.value >= gBlurStrength)
                    gBlur.SamplingDistance.value = gBlurStrength;
            }

            if (currentPhase == PhaseManager.Phase.Attack_Phase
             || currentPhase == PhaseManager.Phase.Normal_Phase)
            {
                // �u���[�����񂾂��0�ɂ��Ă�������
                gBlur.SamplingDistance.value -= CountPoint * 2;
                gFlame += 2;

                // SamplingDistance��0�����������Ȃ�Ȃ��悤��
                if (gBlur.SamplingDistance.value <= 0.0f)
                    gBlur.SamplingDistance.value = 0.0f;
            }
        }

        // �}�j���[�o���s��ꂽ�Ƃ�
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
