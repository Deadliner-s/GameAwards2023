using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // ���b����������l
    public float increasePerSecond = 1.0f;
    private float timer;  // �^�C�}�[

    // ���t���[���Ă΂��֐�
    private void Update()
    {
        timer += Time.deltaTime;  // �^�C�}�[�����Z����

        // �I�u�W�F�N�g��Transform�R���|�[�l���g���擾
        Transform transform = GetComponent<Transform>();

        if (timer >= 1.5f)
        {
            // Scale�v���p�e�B��y�l�𑝉�������
            Vector3 scale = transform.localScale;
            scale.y += increasePerSecond * Time.deltaTime;
            transform.localScale = scale;
        }
    }
}

