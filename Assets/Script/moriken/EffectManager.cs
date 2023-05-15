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
    private float CountPoint = 0.001f;
    private int i;

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

        // �t�F�[�Y�ύX��̏���
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
                    // �u���[�����񂾂�Ƃ����Ă�������
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
                    Debug.Log("�u���[����");
                }
            }
            if (phaseManager.currentPhase == PhaseManager.Phase.Attack_Phase && volume != null)
            {
                if (ManeverEnd == true)
                {
                    // �}�j���[�o��̃u���[�������Ă�������
                    Blur.strength.value -= CountPoint * 5;
                    i += 5;
                }
                else
                {
                    //// �u���[�����񂾂��0�ɂ��Ă�������
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

        // �}�j���[�o���s��ꂽ�Ƃ�
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
