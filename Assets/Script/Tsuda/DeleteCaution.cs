using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCaution : MonoBehaviour
{    
    public float lifetime = 5f;  // �I�u�W�F�N�g�̎����i�b�j

    private float timer;  // �^�C�}�[

    void Start()
    {        
        timer = lifetime;  // �^�C�}�[��ݒ肷��
    }

    void Update()
    {        
        timer -= Time.deltaTime;  // �^�C�}�[�����Z����        

        if (timer <= 0)
        {
            Destroy(gameObject);  // �I�u�W�F�N�g���폜����
        }
    }
}
