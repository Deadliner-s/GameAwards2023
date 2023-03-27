using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpeed : MonoBehaviour
{
    [Tooltip("�ʏ�")]
    [SerializeField] private float SpeedNormal = 80.0f;
    [Tooltip("�p�[�e�B�N����")]
    [SerializeField] private float EmissionNormal = 80.0f;

    [Tooltip("�A�^�b�N")]
    [SerializeField] private float SpeedAttack = 80.0f;
    [Tooltip("�p�[�e�B�N����")]
    [SerializeField] private float EmissionAttack = 80.0f;

    [Tooltip("�n�C�X�s�[�h")]
    [SerializeField] private float SpeedHighSpeed = 300.0f;
    [Tooltip("�p�[�e�B�N����")]
    [SerializeField] private float EmissionHighSpeed = 250.0f;


    [Tooltip("��������p�[�e�B�N���̎��")]
    [SerializeField] ParticleSystem particle;
    private PhaseManager.Phase PhaseFlg; // �t�F�[�Y�t���O

    // Start is called before the first frame update
    void Start()
    {
        // �t�F�[�Y�擾�p
        PhaseFlg = PhaseManager.instance.GetPhase();

        // �ʏ�t�F�[�Y����
        PhaseFlg = PhaseManager.Phase.Normal_Phase;
    }

    // Update is called once per frame
    void Update()
    {   
        // �t�F�[�Y�擾�p
        PhaseFlg = PhaseManager.instance.GetPhase();
        var main = particle.main;
        var emission = particle.emission;
        var renderer = particle.GetComponent<Renderer>();

        // �t�F�[�Y�ɂ���Đ؂�ւ�
        // �ʏ�
        if (PhaseFlg == PhaseManager.Phase.Normal_Phase)
        {
            main.startSpeed = SpeedNormal;
            emission.rateOverTime = EmissionNormal ;
        }
        // �A�^�b�N
        if (PhaseFlg == PhaseManager.Phase.Attack_Phase)
        {
            main.startSpeed = SpeedAttack;
            emission.rateOverTime = EmissionAttack;
        }
        // �n�C�X�s�[�h
        if (PhaseFlg == PhaseManager.Phase.Speed_Phase)
        {
            main.startSpeed = SpeedHighSpeed;
            emission.rateOverTime = EmissionHighSpeed;
        }
    }
}
