using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossContenaSmall : MonoBehaviour
{
    public float Speed = 0.01f;        //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;      //���x����
    public float Accel = 0.005f;       //�����x
    public float MissRange = 5.0f;     //�v���C���[�ɊO���̋���
    float off;
    bool Miss;

    System.Random rand = new System.Random();
    float randXZ;
    float randY;
    Vector3 PlusRandomMove;

    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�
    // Start is called before the first frame update
    void Start()
    {
        ToPos = GameObject.Find("Player").transform.position; //Player
        PlusRandomMove = ToPos - transform.position;
        PlusRandomMove = PlusRandomMove.normalized;

        randXZ = rand.Next(32);
        randY = rand.Next(32);

        randXZ -= 16;
        randY -= 16;

        randXZ *= 0.1f;
        randY *= 0.1f;

        randXZ += PlusRandomMove.x;
        randY += PlusRandomMove.y;

        Miss = false;
        off = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;   //�v���C���[�̈ʒu
        Speed += Accel;                                         //�����x
        if (Speed >= MaxSpeed)                                  //���x����
            Speed = MaxSpeed;
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(ToPos.x, 0, ToPos.z));
        if (distance >= MissRange && !Miss)
        {
            Move = ToPos - transform.position;
            Move.x += randXZ;
            Move.y += randY;
            Move = Move.normalized;
            LateMove = (Move - LateMove) * off + (LateMove);
        }
        else
        {
            Miss = true;
            Destroy(gameObject, 3.0f);
        }
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position += LateMove * Speed;
    }
}
