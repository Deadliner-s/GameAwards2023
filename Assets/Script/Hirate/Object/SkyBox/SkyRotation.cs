using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // �w�i�����I�u�W�F�N�g
    private GameObject skyInstanceObj;

    // ��]
    private float rotation;      // ���݂̉�]
    private float rotationSpeed; // ��]���x
    private PhaseManager.Phase PhaseFlg; // �t�F�[�Y�t���O
    private Material sky; // ���������X�J�C�{�b�N�X������p

    // Start is called before the first frame update
    void Start()
    {
        // �X�J�C�{�b�N�X�̎擾
        SkyBoxCatch();

        // �X�J�C�{�b�N�X���擾
        //sky = RenderSettings.skybox;

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

        if (sky != null)
        {
            // �w�i�̉�]
            rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
            // ������̉�]����
            sky.SetFloat("_Rotation", rotation);
        }
        else
        {
            Debug.Log("SkyBox��Null�ł��B");
        }

// �f�o�b�O�p
#if _DEBUG
        Debug.Log(rotation);
#endif // _DEBUG
    }

    // �X�J�C�{�b�N�X���擾����֐�
    private void SkyBoxCatch()
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SkyBoxInstance skyBoxInstance = sceneRootObj.GetComponent<SkyBoxInstance>();
            if (skyBoxInstance != null)
            {
                sky = skyBoxInstance.GetSky();
                RenderSettings.skybox = sky;
                break;
                //yield return new WaitForSeconds(0.0f);
            }
        }
    }
}
