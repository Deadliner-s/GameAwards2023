using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxInstance : MonoBehaviour
{
    // �X�J�C�{�b�N�X�̃}�e���A��
    [Header("�w�i�}�e���A���ݒ�")]
    [Tooltip("�w�i�̃}�e���A��")]
    public Material skyInstance;

    // �X�J�C�{�b�N�X�𐶐����ē����
    void Awake()
    {
        RenderSettings.skybox = new Material(skyInstance);
    }
}
