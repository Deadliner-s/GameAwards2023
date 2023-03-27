using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    // �w�i�̉�]���x
    [Header("�w�i�̉�]���x")]
    [Tooltip("�ʏ�")]
    [SerializeField] private float rotationSpeedNormal = -0.1f;
    [Tooltip("�A�^�b�N")]
    [SerializeField] private float rotationSpeedAttack = -0.1f;
    [Tooltip("�n�C�X�s�[�h")]
    [SerializeField] private float rotationSpeedHighSpeed = -0.1f;
    // �w�i�̃}�e���A��
    [Header("�w�i�}�e���A���ݒ�")]
    [Tooltip("�w�i�̃}�e���A��")]
    [SerializeField] Material sky;

    // ��]
    private float rotation;      // ���݂̉�]
    private float rotationSpeed; // ��]���x
    private float initRotation;  // ��]�̏����ʒu
    private PhaseManager.Phase PhaseFlg; // �t�F�[�Y�t���O

    // Start is called before the first frame update
    void Start()
    {
        // �I�����Ɍ��݂̉�]���㏑������邽�߁A�ŏ��Ɏ擾
        // ��]�̏����ʒu
        initRotation = sky.GetFloat("_Rotation");

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

        // �w�i�̉�]
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
        // ������̉�]����
        sky.SetFloat("_Rotation", rotation);
// �f�o�b�O�p
#if _DEBUG
        Debug.Log(rotation);
#endif // _DEBUG
    }
    // �A�v���P�[�V�����I�����̏���
    private void OnApplicationQuit()
    {
        // ���݂̉�]�������ʒu�ɏ㏑��
        sky.SetFloat("_Rotation", initRotation);
    }
}
