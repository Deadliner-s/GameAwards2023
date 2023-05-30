using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxInstance : MonoBehaviour
{
    // �X�J�C�{�b�N�X�̃}�e���A��
    [Header("�w�i�}�e���A���ݒ�")]
    [Tooltip("�w�i�̃}�e���A��")]
    public static SkyBoxInstance instance;
    public Material skyInstance;
    // ���������}�e���A����������p
    private Material sky;

    // ��]
    private float rotation; // ���݂̉�]
    // ��]���x�̎擾�ƃZ�b�g
    public float rotationSpeed { get; set; } // ��]���x

    // �X�J�C�{�b�N�X�𐶐����ē����
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // �ݒ肵�����X�J�C�{�b�N�X�̃}�e���A�����X�J�C�{�b�N�X�ɑ��
        sky = new Material(skyInstance);
        RenderSettings.skybox = sky;
        // �S�ẴV�[���Ŏg���邽�߁A�V�[���؂�ւ��Ŕj������Ȃ��悤�ɂ���
        DontDestroyOnLoad(sky);
    }

    private void Update()
    {
        // �w�i�̉�]
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
        // ������̉�]����
        sky.SetFloat("_Rotation", rotation);
    }

    // ���������X�J�C�{�b�N�X�̃}�e���A����Ԃ�
    public Material GetSky()
    {
        return sky;
    }
}
