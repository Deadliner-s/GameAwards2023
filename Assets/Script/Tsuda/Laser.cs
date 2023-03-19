using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // ���b����������l
    public float increasePerSecond = 1.0f;

    // ���t���[���Ă΂��֐�
    private void Update()
    {
        // �I�u�W�F�N�g��Transform�R���|�[�l���g���擾
        Transform transform = GetComponent<Transform>();

        // Scale�v���p�e�B��y�l�𑝉�������
        Vector3 scale = transform.localScale;
        scale.y += increasePerSecond * Time.deltaTime;
        transform.localScale = scale;
    }
}

