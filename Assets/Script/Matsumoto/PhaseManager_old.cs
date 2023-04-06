using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager_old : MonoBehaviour
{
    // �t�F�C�Y�؂�ւ��p
    [Header("�t�F�C�Y�m�F�p�I�u�W�F�N�g")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;                   // �t�F�C�Y�擾�p
    private float time = 0.0f;                  // �b���J�E���g�p
    private bool boolValue = false;             // �t�F�C�Y�؂�ւ��p

    // �t�F�C�Y���ς��b��
    [Header("�t�F�C�Y���ς��b��")]
    public float PhaseChangeTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // �ŏ��̃t�F�C�Y�擾
        AtkPhaseFlg = PhaseObj.activeSelf;
        // �^�C�}�[������
        time = 0.0f;

        // �ŏ��̃t�F�C�Y�ɂ���ď�������ς���
        if (AtkPhaseFlg == false)
        {
            boolValue = false;
        }
        else
        {
            boolValue = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= PhaseChangeTime)
        {
            boolValue = !boolValue;
            PhaseObj.SetActive(boolValue);
            time = 0.0f;
        }
        
    }
}
