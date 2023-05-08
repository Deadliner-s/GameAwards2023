using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotation : MonoBehaviour
{
    [Header("Boss�̉�]���x")]
    [Tooltip("�ʏ�")]
    [SerializeField] private float rotationSpeedNormal = 0.1f;
    [Tooltip("�A�^�b�N")]
    [SerializeField] private float rotationSpeedAttack = 0.1f;
    [Tooltip("�n�C�X�s�[�h")]
    [SerializeField] private float rotationSpeedHighSpeed = 0.1f;

    private float rotationSpeed; // ��]���x
    private PhaseManager.Phase PhaseFlg; // �t�F�[�Y�t���O

    // Start is called before the first frame update
    void Start()
    {
        // �t�F�[�Y�擾�p
        try
        {
            PhaseFlg = PhaseManager.instance.GetPhase();
        }
        catch
        {

        }

        // �ʏ�t�F�[�Y����
        PhaseFlg = PhaseManager.Phase.Normal_Phase;
    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�[�Y�擾�p
        try
        {
            PhaseFlg = PhaseManager.instance.GetPhase();
        }
        catch
        {

        }

        // �t�F�[�Y�ɂ���Đ؂�ւ�
        // �ʏ�
        if (PhaseFlg == PhaseManager.Phase.Normal_Phase) {
            rotationSpeed = rotationSpeedNormal;
        }
        // �A�^�b�N
        if (PhaseFlg == PhaseManager.Phase.Attack_Phase) {
            rotationSpeed = rotationSpeedAttack;
        }
        // �n�C�X�s�[�h
        if (PhaseFlg == PhaseManager.Phase.Speed_Phase) {
            rotationSpeed = rotationSpeedHighSpeed;
        }
        // �����Ă���悤�Ɍ�����
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }
}
