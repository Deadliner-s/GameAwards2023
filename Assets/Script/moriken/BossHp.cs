using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V�[���̈ړ��������s���@�\
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossHp : MonoBehaviour
{
    private GameObject BossManager;

    // Start is called before the first frame update
    void Start()
    {
        BossManager = GameObject.Find("BossManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        // �����Փ˂�������I�u�W�F�N�g�̃^�O��"Enemy"�Ȃ�Β��̏��������s
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            BossManager.GetComponent<MainBossHp>().Damage(collision);
        }
    }
}
