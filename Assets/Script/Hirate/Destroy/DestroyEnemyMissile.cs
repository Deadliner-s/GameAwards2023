using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyMissile : MonoBehaviour
{
    // �G�t�F�N�g
    [SerializeField] GameObject obj;
    // �Փˈʒu
    private Vector3 hitPos;

    // �I�u�W�F�N�g�������������ɍs����֐�
    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�ɓ���������
        if (collision.gameObject.tag == "Player")
        {
            // �Փˈʒu���擾
            foreach(ContactPoint contact in collision.contacts)
            {
                hitPos = contact.point;
            }

            // �G�t�F�N�g�𐶐�
            Instantiate(
                obj, // �G�t�F�N�g���������I�u�W�F�N�g
                hitPos, // ���W
                Quaternion.identity); // ��]

            // �X�N���v�g���R���|�[�l���g�����I�u�W�F�N�g���폜
            Destroy(gameObject);
        }
    }
}
