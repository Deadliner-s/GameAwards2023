using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public static CreateLaser instance;

    [Tooltip("�~�T�C���v���n�u")]
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g    

    // �i�s���ԗp
    public float timer;

    // ���Z�b�g�t���O
    private bool Reset_flg = false;

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;

    // ���ݑ_���Ă���Split
    public int TargetSplit;

    [Header("���[�U�[1���")]
    [Tooltip("���b�ڊJ�n")]
    public float StartTime_1 = 100.0f;
    [Tooltip("���ԂɌ���")]
    public int Split_1;
    // �m�F�p�t���O
    private bool Use_flg_1 = false;

    [Header("���[�U�[2���")]
    [Tooltip("���b�ڊJ�n")]
    public float StartTime_2 = 100.0f;
    [Tooltip("���Ԃ�")]
    public int Split_2;
    // �m�F�p�t���O
    private bool Use_flg_2 = false;

    [Header("���[�U�[3���")]
    [Tooltip("���b�ڊJ�n")]
    public float StartTime_3 = 100.0f;
    [Tooltip("���Ԃ�")]
    public int Split_3;
    // �m�F�p�t���O
    private bool Use_flg_3 = false;

    [Header("���[�U�[4���")]
    [Tooltip("���b�ڊJ�n")]
    public float StartTime_4 = 100.0f;
    [Tooltip("���Ԃ�")]
    public int Split_4;
    // �m�F�p�t���O
    private bool Use_flg_4 = false;

    [Header("���[�U�[5���")]
    [Tooltip("���b�ڊJ�n")]
    public float StartTime_5 = 100.0f;
    [Tooltip("���Ԃ�")]
    public int Split_5;
    // �m�F�p�t���O
    private bool Use_flg_5 = false;

    void Start()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        // ���Ԃ̏�����
        timer = 0.0f;
    }

    void Update()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        // �f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.C)) // C�L�[�������ꂽ��
        {
            Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
        }


        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {// �n�C�X�s�[�h�t�F�C�Y
            // �t���O�Ȃǂ����Z�b�g
            if (Reset_flg == true)
            {
                timer = 0.0f;

                Use_flg_1 = false;
                Use_flg_2 = false;
                Use_flg_3 = false;
                Use_flg_4 = false;
                Use_flg_5 = false;

                Reset_flg = false;
            }

            // ���ԍX�V
            timer += Time.deltaTime;

            // 1���
            if (Use_flg_1 == false && timer >= StartTime_1)
            {
                TargetSplit = 1;
                // ���[�U�[�쐬
                GameObject obj = Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
                obj.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // �t���O�X�V
                Use_flg_1 = true;
            }
            // 2���
            if (Use_flg_2 == false && timer >= StartTime_2)
            {
                TargetSplit = 2;
                // ���[�U�[�쐬
                GameObject obj1 = Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
                obj1.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // �t���O�X�V
                Use_flg_2 = true;
            }
            // 3���
            if (Use_flg_3 == false && timer >= StartTime_3)
            {
                TargetSplit = 3;
                // ���[�U�[�쐬
                GameObject obj2 = Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
                obj2.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // �t���O�X�V
                Use_flg_3 = true;
            }
            // 4���
            if (Use_flg_4 == false && timer >= StartTime_4)
            {
                TargetSplit = 4;
                // ���[�U�[�쐬
                GameObject obj3 = Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
                obj3.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // �t���O�X�V
                Use_flg_4 = true;
            }
            // 5���
            if (Use_flg_5 == false && timer >= StartTime_5)
            {
                TargetSplit = 5;
                // ���[�U�[�쐬
                GameObject obj4 = Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
                obj4.GetComponent<LaserHead>().SetSplit(TargetSplit);
                // �t���O�X�V
                Use_flg_5 = true;
            }
        }
        else
        {// �A�^�b�N�t�F�C�Y
            Reset_flg = true;
        }
    }
}

