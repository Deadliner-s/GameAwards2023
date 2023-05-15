using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float PlayerDamage { get; private set; }
    public float EnemyDamage { get; private set; }

    // �_���[�W��
    [Header("�_���[�W��")]
    [Tooltip("�v���C���[�ւ̃_���[�W")]
    [SerializeField] DamageManager.DAMAGE_NAME playerDamageName = DamageManager.DAMAGE_NAME.E_DAMAGE_MAX;
    [Tooltip("�{�X�ւ̃_���[�W")]
    [SerializeField] DamageManager.DAMAGE_NAME bossDamageName = DamageManager.DAMAGE_NAME.E_DAMAGE_MAX;

    // �_���[�W�}�l�[�W���[�I�u�W�F�N�g
    private GameObject DamageAmount;

    private void Start()
    {
        // �_���[�W�}�l�[�W���[���擾
        DamageAmount = GameObject.Find("DamageManager");
        //---- �C���X�y�N�^�[�Őݒ肵���^����_���[�W���� ----
        // �v���C���[�ւ̃_���[�W
        PlayerDamage = DamageAmount.GetComponent<DamageManager>().GetDamage(playerDamageName);
        // �{�X�ւ̃_���[�W
        EnemyDamage = DamageAmount.GetComponent<DamageManager>().GetDamage(bossDamageName);
    }
}
