using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumDelete : MonoBehaviour
{
    private GameObject BossManager;

    int HitCount;
    int MaxHitCount;

    // Start is called before the first frame update
    void Start()
    {
        BossManager = GameObject.Find("BossManager");

        HitCount = 0;
       // Boss = GameObject.Find("boss_model");
        MaxHitCount = BossManager.GetComponent<MainBossHp>().MAXHitCount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�̒e�������������A
        // �X�N���v�g���R���|�[�l���g�����I�u�W�F�N�g��j�󂷂�
        if (collision.gameObject.tag == "PlayerBullet")
        {
            HitCount++;
            if(HitCount == MaxHitCount)
            {
                // �G�t�F�N�g�\��
                BossManager.GetComponent<MainBossHp>().ExplosionSet(collision);

                Destroy(gameObject);
            }
        }
    }
}
