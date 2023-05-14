using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MissileBossClusterSmall : MonoBehaviour
{
    float Speed = 0.01f;        //�~�T�C���̑��x
    float MinSpeed = 0.004f;    //�ŏ����x
    float MaxSpeed = 0.02f;     //�ő呬�x
    float Accel = 0.0001f;      //��(��)���x

    public float MissRange = 2.0f;     //�v���C���[�ɊO���̋���
    float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randSpeed;
    float randY;
    Vector3 PlusY;
    //OtherScript Particle = GetComponent<StartParticle>();

    GameObject Player;
    Vector3 ToPos;              //���ː�
    Vector3 CheckPos;
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        if (Player = GameObject.Find("Player"))//�v���C���[�͐����Ă���i���݂���j
        {
            ToPos = Player.transform.position; //Player
            Miss = false;
            off = 0.2f;
            randSpeed = rand.Next(20);
            randSpeed *= 0.0001f;
            randY = rand.Next(30);
            randY -= 15;
            randY *= 0.01f;
            randY += PlusY.y;
            GetComponent<StartParticle>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)//�v���C���[�͐����Ă���i���݂���j
        {
            if (!Miss && Speed >= MinSpeed+randSpeed)
            {
                Speed -= Accel;
                ToPos = Player.transform.position;   //�v���C���[�̈ʒu
                Move = new Vector3(1.0f,randY,0.0f);
            }
            else
            {
                if(!GetComponent<StartParticle>().enabled)
                    GetComponent<StartParticle>().enabled= true;
                Miss = true;
                Speed = MaxSpeed;
                Move = new Vector3(1.0f, randY * 0.3f, 0.0f);
                Destroy(gameObject, 3.0f);
            }

            LateMove = (Move - LateMove) * off + (LateMove);
            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            transform.position += LateMove * Speed;
        }
        else
        {
            Destroy(this, 0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}