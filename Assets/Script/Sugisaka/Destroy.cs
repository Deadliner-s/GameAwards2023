using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�̒e�������������A
        // �X�N���v�g���R���|�[�l���g�����I�u�W�F�N�g��j�󂷂�
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
