using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProj : MonoBehaviour
{
    public float Speed;         //�~�T�C���̑��x
    float t = 0;                //time �E ����

    Transform FromTransform;    //���ˌ�
    Transform ToTransform;      //���ː�

    Vector3 FromPos;
    Vector3 ToPos;

    public float Height;        //�~�T�C���̍���
    float ControlPositionX;
    float ControlPositionY;
    float ControlPositionZ;

    // Start is called before the first frame update
    void Start()
    {
        //EnemyBoss�Ƃ������O�̂��̂����݂���
        FromPos = GameObject.Find("EnemyBoss").transform.position;
        //Player�Ƃ������O�̂��̂����݂���
        ToPos = GameObject.Find("Player").transform.position;
        //�~�T�C���̏����ʒu��ݒ�
        transform.position = FromPos;
    }

    // Update is called once per frame
    void Update()
    {
        t += Speed;
        //�v���C���[���݂̈ʒu���X�V�E�ǐ�---
       //ToPos = GameObject.Find("Player").transform.position;  //(bug : �Ȃ��������\�����ł���)

        ControlPositionX = FromPos.x + (ToPos.x - FromPos.x) * 0.1f;
        ControlPositionY = FromPos.y + Height;
        ControlPositionZ = FromPos.z + (ToPos.z - FromPos.z) * 0.1f;

        //�~�T�C���̉ߋ��̈ʒu���L�^
        Vector3 oldPos = transform.position;

        //�~�T�C���̎��̈ʒu���v�Z
        float nextPosX = ((1 - t) * (1 - t) * FromPos.x
                                    + 3 * (1 - t) * (1 - t) * t * ControlPositionX
                                    + 3 * (1 - t) * t * t * ControlPositionX
                                    + t * t * t * ToPos.x);

        float nextPosY = ((1 - t) * (1 - t) * FromPos.y
                                    + 3 * (1 - t) * (1 - t) * t * ControlPositionY
                                    + 3 * (1 - t) * t * t * ControlPositionY
                                    + t * t * t * ToPos.y);

        float nextPosZ = ((1 - t) * (1 - t) * FromPos.z
                                    + 3 * (1 - t) * (1 - t) * t * ControlPositionZ
                                    + 3 * (1 - t) * t * t * ControlPositionZ
                                    + t * t * t * ToPos.z);
        //�~�T�C���̉�]���v�Z
        Vector3 Pos = new Vector3(nextPosX, nextPosY, nextPosZ);
        Vector3 Normal = Pos - oldPos;
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f,1.0f,0.0f),Normal.normalized);

        //�~�T�C���̈ʒu�Ɖ�]���X�V
        transform.rotation = rot;
        transform.position = Pos;
    }
}