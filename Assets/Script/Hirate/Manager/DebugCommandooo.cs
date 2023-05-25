using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandooo : MonoBehaviour
{
    [Header("�f�o�b�O�p\n(�Q�[�����s�O�ɐݒ肵�Ă�������)")]
    [Header("�f�o�b�O���[�h")]
    [SerializeField] bool debugMode = false;
    [Header("�_���[�W��0�ɂ��܂�")]
    [SerializeField] bool debugDamage = false;
    [Header("�V�[�����X�L�b�v���܂�")]
    [SerializeField] bool debugScene = false;

    public static DebugCommandooo instance;

    // �_���[�W�ݒ�̃Z�b�g�p
    public bool debugDamageSet { get; private set; }
    // �V�[���ݒ�̃Z�b�g�p
    public bool debugSceneSet { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        // �f�o�b�O���[�h��OFF�Ȃ�f�o�b�O���[�h�ɂ��Ȃ�
        if (!debugMode)
        {
            // �_���[�W�ݒ�̃Z�b�g
            debugDamageSet = false;
            // �V�[���ݒ�̃Z�b�g
            debugSceneSet = false;

            return;
        }

        // �_���[�W�ݒ�̃Z�b�g
        debugDamageSet = debugDamage;
        // �V�[���ݒ�̃Z�b�g
        debugSceneSet = debugScene;
    }
}
