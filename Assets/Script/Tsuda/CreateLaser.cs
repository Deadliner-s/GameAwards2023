using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaser : MonoBehaviour
{
    public GameObject prefab; // �v���n�u�I�u�W�F�N�g    

    void Start()
    {        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // C�L�[�������ꂽ��
        {
            Instantiate(prefab, transform.position, transform.rotation); // �v���n�u�I�u�W�F�N�g�𐶐�����
        }
    }
}

