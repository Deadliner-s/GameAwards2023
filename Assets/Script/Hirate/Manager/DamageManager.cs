using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public enum DAMAGE_NAME
    {
        // �{�X�ւ̃_���[�W
        E_PLAYER_ATTACK_DAMAGE = 0,

        // �~�T�C��
        E_BOSS_MISSLE_CLUSTER,       // �N���X�^�[�~�T�C��
        E_BOSS_MISSLE_CLUSTER_SMALL, // �N���X�^�[�~�T�C���̒��g
        E_BOSS_MISSLE_CONTENA,       // �R���e�i�~�T�C��
        E_BOSS_MISSLE_CONTENA_SMALL, // �R���e�i�~�T�C���̒��g
        E_BOSS_MISSLE_HOMING,        // �U���~�T�C��
        E_BOSS_MISSLE_SPEED,         // �����~�T�C��
        E_BOSS_MISSLE_ZAKO,          // �����~�T�C��

        // ���[�U�[
        E_BOSS_LASER,

        // �_���[�W�̖��O�̍ő�
        E_DAMAGE_MAX
    }

    // �_���[�W�̕ϐ�
    [Header("�{�X�ւ̃_���[�W�ݒ�")]
    [SerializeField] float PlayerAttackDamage = 100.0f;
    [Header("�v���C���[�ւ̃_���[�W�ݒ�")]
    [Tooltip("�N���X�^�[�~�T�C��")]
    [SerializeField] float BossMissleCluster = 5.0f;
    [Tooltip("�N���X�^�[�~�T�C���̒��g")]
    [SerializeField] float BossMissleClusterSmall = 5.0f;
    [Tooltip("�R���e�i�~�T�C��")]
    [SerializeField] float BossMissleContena = 5.0f;
    [Tooltip("�R���e�i�~�T�C���̒��g")]
    [SerializeField] float BossMissleContenaSmall = 5.0f;
    [Tooltip("�U���~�T�C��")]
    [SerializeField] float BossMissleHoming = 5.0f;
    [Tooltip("�����~�T�C��")]
    [SerializeField] float BossMissleSpeed = 5.0f;
    [Tooltip("�G���̃~�T�C��")]
    [SerializeField] float BossMissleZako = 5.0f;
    [Tooltip("���[�U�[")]
    [SerializeField] float BossLaser = 5.0f;

    // �f�o�b�O�p(�_���[�W��0�ɂ��܂�)
    private bool debug = false;

    private void Start()
    {
        // �f�o�b�O�p�̐ݒ�
        debug = DebugCommandooo.instance.debugDamageSet;
    }

    // �_���[�W�ʎ擾�p�̊֐�
    public float GetDamage(DAMAGE_NAME damage_name)
    {
        // �Ԃ�l�p�̕ϐ�
        float work = 0.0f;

        // �Ԃ�l�Ń_���[�W��Ԃ����߂̕���
        switch (damage_name)
        {
            // �N���X�^�[�~�T�C��
            case DAMAGE_NAME.E_BOSS_MISSLE_CLUSTER:
                work = BossMissleCluster;
                break;
            // �N���X�^�[�~�T�C���̒��g
            case DAMAGE_NAME.E_BOSS_MISSLE_CLUSTER_SMALL:
                work = BossMissleClusterSmall;
                break;
            // �R���e�i�~�T�C��
            case DAMAGE_NAME.E_BOSS_MISSLE_CONTENA:
                work = BossMissleContena;
                break;
            // �R���e�i�~�T�C���̒��g
            case DAMAGE_NAME.E_BOSS_MISSLE_CONTENA_SMALL:
                work = BossMissleContenaSmall;
                break;
            // �U���~�T�C��
            case DAMAGE_NAME.E_BOSS_MISSLE_HOMING:
                work = BossMissleHoming;
                break;
            // �����~�T�C��
            case DAMAGE_NAME.E_BOSS_MISSLE_SPEED:
                work = BossMissleSpeed;
                break;
            // �G���̃~�T�C��
            case DAMAGE_NAME.E_BOSS_MISSLE_ZAKO:
                work = BossMissleZako;
                break;
            // ���[�U�[
            case DAMAGE_NAME.E_BOSS_LASER:
                work = BossLaser;
                break;
            // �f�o�b�O�p
            case DAMAGE_NAME.E_DAMAGE_MAX:
                work = 0.0f;
                break;
        }

        // �f�o�b�O�p
        if (debug)
        {
            // �_���[�W�ʂ�0�ɂ���
            work = 0.0f;
        }

        // �v���C���[����̃_���[�W�͂��̂܂܂ɂ���
        if (damage_name == DAMAGE_NAME.E_PLAYER_ATTACK_DAMAGE)
        {
            work = PlayerAttackDamage;
        }

        // �Ԃ�l�Ń_���[�W�ʂ�Ԃ�
        return work;
    }
}
