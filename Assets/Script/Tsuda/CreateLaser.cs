using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g

    void Update()
    {
        // ����'C'�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.C))
        {
            // �v���n�u�I�u�W�F�N�g�𐶐�����
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
