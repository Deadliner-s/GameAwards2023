using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxInstance : MonoBehaviour
{
    // �X�J�C�{�b�N�X�̃}�e���A��
    [Header("�w�i�}�e���A���ݒ�")]
    [Tooltip("�w�i�̃}�e���A��")]
    //public SkyBoxInstance instance;
    public Material skyInstance;

    // �X�J�C�{�b�N�X�𐶐����ē����
    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}

        // �ݒ肵�����X�J�C�{�b�N�X�̃}�e���A�����X�J�C�{�b�N�X�ɑ��
        RenderSettings.skybox = new Material(skyInstance);
        // �S�ẴV�[���Ŏg���邽�߁A�V�[���؂�ւ��Ŕj������Ȃ��悤�ɂ���
        DontDestroyOnLoad(skyInstance);
    }
    // ���������X�J�C�{�b�N�X�̃}�e���A����Ԃ�
    public Material GetSky()
    {
        return skyInstance;
    }
}
