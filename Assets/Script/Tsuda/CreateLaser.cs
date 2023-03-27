using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g    

    // ���݃t�F�C�Y
    private PhaseManager.Phase currentPhase;

    void Start()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();
    }

    void Update()
    {
        // �t�F�C�Y�擾
        currentPhase = PhaseManager.instance.GetPhase();

        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            if (Input.GetKeyDown(KeyCode.C)) // C�L�[�������ꂽ��
            {
                Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
            }
        }
    }
}

