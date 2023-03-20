using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    // �w�i�̉�]���x
    [Header("�w�i�̉�]���x")]
    [Tooltip("��]���x")]
    public float rotationSpeed;
    // �w�i�̃I�u�W�F�N�g
    [Header("�w�i�I�u�W�F�N�g�ݒ�")]
    [Tooltip("�w�i�̃I�u�W�F�N�g")]
    public Material sky;

    // ��]
    private float rotation; // ���݂̉�]
    private float initRotation; // ��]�̏����ʒu

    // Start is called before the first frame update
    void Start()
    {
        // �I�����Ɍ��݂̉�]���㏑������邽�߁A�ŏ��Ɏ擾
        // ��]�̏����ʒu
        initRotation = sky.GetFloat("_Rotation");
    }

    // Update is called once per frame
    void Update()
    {
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
