using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandooo : MonoBehaviour
{
    [Header("�f�o�b�O�p\n(�Q�[�����s�O�ɐݒ肵�Ă�������)")]
    [Header("�f�o�b�O���[�h")]
    [SerializeField] bool debugMode = false;
    [Header("�V�[�����X�L�b�v���܂�")]
    [SerializeField] bool debugScene = false;
    [Header("�����X�C�b�`���������܂����H")]
    [SerializeField] bool debugExplosion = false;
    [Header("�_���[�W��0�ɂ��܂�")]
    [SerializeField] bool debugDamage = false;
    [Header("�~�T�C���̃f�o�b�O�̗L��")]
    [SerializeField] bool debugMissile = false;

    public static DebugCommandooo instance;

    // �V�[���ݒ�̃Z�b�g�p
    public bool debugSceneSet { get; private set; }
    // �����X�C�b�`�ݒ�̃Z�b�g�p
    public bool debugExplosionSet { get; private set; }
    // �_���[�W�ݒ�̃Z�b�g�p
    public bool debugDamageSet { get; private set; }
    // �~�T�C���ݒ�̃Z�b�g�p
    public bool debugMissileSet { get; private set; }

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
            // �V�[���ݒ�̃Z�b�g
            debugSceneSet = false;
            // �����X�C�b�`�ݒ�̃Z�b�g
            debugExplosionSet = false;
            // �_���[�W�ݒ�̃Z�b�g
            debugDamageSet = false;
            // �~�T�C���ݒ�̃Z�b�g
            debugMissileSet = false;

            return;
        }

        // �V�[���ݒ�̃Z�b�g
        debugSceneSet = debugScene;
        // �V�[���ݒ�̃Z�b�g
        debugExplosionSet = debugExplosion;
        // �_���[�W�ݒ�̃Z�b�g
        debugDamageSet = debugDamage;
        // �~�T�C���ݒ�̃Z�b�g
        debugMissileSet = debugMissile;
    }
}
