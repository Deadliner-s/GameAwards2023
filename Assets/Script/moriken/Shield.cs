using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    GameObject Player;
    MeshCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        collider = gameObject.GetComponent<MeshCollider>();
        collider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[��HP��0�ȉ��ɂȂ����瓖���蔻�������
        if (Player.GetComponent<PlayerHp>().PlayerHP <= 0)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // �����Փ˂�������I�u�W�F�N�g�̃^�O��"Enemy"�Ȃ�Β��̏��������s
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Player.GetComponent<PlayerHp>().Damage(collision);
        }
    }
}
