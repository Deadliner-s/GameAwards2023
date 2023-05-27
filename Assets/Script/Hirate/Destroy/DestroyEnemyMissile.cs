using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyMissile : MonoBehaviour
{
    // �G�t�F�N�g
    [SerializeField] GameObject obj;
    // �Փˈʒu
    private Vector3 hitPos;
    // �v���C���[�}�l�[�W���[
    private GameObject playerManager;

    private void Update()
    {
        // �v���C���[�}�l�[�W���[���������ɑ������
        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerManager");
        }

        // �v���C���[�����S�Ɍ��Ă��ꂽ�Ƃ��j�󂷂�
        if (playerManager.GetComponent<PlayerHp>().BreakFlag)
        {
            Destroy(gameObject);
        }
    }

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
            if (gameObject.GetComponent<MissileStraight>())
            {
                gameObject.GetComponent<MissileStraight>().isDestroyed = true;
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
