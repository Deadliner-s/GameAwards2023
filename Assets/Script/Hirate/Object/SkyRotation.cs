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
    public Material sky;

    // ��]
    private float rotation;      // ���݂̉�]
    private float rotationSpeed; // ��]���x
    private PhaseManager.Phase PhaseFlg; // �t�F�[�Y�t���O
    private Material skyInstance; // ���������X�J�C�{�b�N�X������p

    // Start is called before the first frame update
    void Start()
    {
        skyInstance = new Material(sky); // ���������X�J�C�{�b�N�X������p
        RenderSettings.skybox = skyInstance;

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

        // �w�i�̉�]
        rotation = Mathf.Repeat(skyInstance.GetFloat("_Rotation") + rotationSpeed, 360f);
        // ������̉�]����
        skyInstance.SetFloat("_Rotation", rotation);
// �f�o�b�O�p
#if _DEBUG
        Debug.Log(rotation);
#endif // _DEBUG
    }

    // �I�u�W�F�N�g���j�����ꂽ�����s
    private void OnDestroy()
    {
        if(skyInstance != null)
        {
            Destroy(skyInstance);
            skyInstance = null;
        }
    }
}
