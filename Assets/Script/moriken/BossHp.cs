using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V�[���̈ړ��������s���@�\
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossHp : MonoBehaviour
{
    GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("BOSS_base");
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
            Boss.GetComponent<MainBossHp>().Damage(collision);
        }
    }
}
