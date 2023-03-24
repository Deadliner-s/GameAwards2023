using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBossCluster : MonoBehaviour
{
    public float Speed = 0.005f;        //�~�T�C���̑��x
    public float MaxSpeed = 2.0f;      //���x����
    public float Accel = 0.001f;       //�����x
    public float ClusterRange = 3.5f;     //�v���C���[�ɊO���̋���
    public int ClusterNumber = 6;
    //private GameObject canvas;         // �L�����o�X
    float off;
    bool Cluster;
    GameObject newObj;
    public GameObject otherObject;        // ��������v���n�u�I�u�W�F�N�g
    //private Camera mainCamera;            // ���C���J����
    //private Vector3 targetScreenPosition; // �ڕW�X�N���[�����W
    //private Vector3 targetWorldPosition;  // �ڕW���[���h���W

    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        ToPos = GameObject.Find("Player").transform.position; //Player
        //mainCamera = Camera.main;                             // ���C���J�������擾����
        //canvas = GameObject.Find("Canvas");                 �@// �L�����o�X���w��

        ////UI�����ʒu
        //if (transform.position.x < ToPos.x)
        //{
        //    targetScreenPosition.x = 1820 / -2;
        //}
        //else
        //{
        //    targetScreenPosition.x = 0;
        //}
        //if (transform.position.y < ToPos.y)
        //{
        //    targetScreenPosition.y = 980 / -2;
        //}
        //else if (transform.position.y > ToPos.y)
        //{
        //    targetScreenPosition.y = 980 / 2;
        //}
        //else
        //{
        //    targetScreenPosition.y = 0;
        //}
        //targetScreenPosition.z = 2.0f;

        ////UI����
        //newObj = Instantiate(otherObject, targetScreenPosition, transform.rotation) as GameObject;  // �x��UI�̐���
        //Destroy(newObj, 3.0f);                                                                      // UI������

        //newObj.transform.SetParent(canvas.transform, false);                                        // Canvas�̎q�I�u�W�F�N�g�Ƃ��Đ���

        Cluster = false;
        off = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        ToPos = GameObject.Find("Player").transform.position;   //�v���C���[�̈ʒu
        Speed += Accel;                                         //�����x
        if (Speed >= MaxSpeed)                                  //���x����
            Speed = MaxSpeed;
        float distance = Vector3.Distance(new Vector3(transform.position.x,0, transform.position.z),new Vector3(ToPos.x,0, ToPos.z));
        if (distance >= ClusterRange && !Cluster)               //�܂����􂵂ĂȂ�
        {
            Move = ToPos - transform.position;
            Move = Move.normalized;
            LateMove.x = (Move.x - LateMove.x) * off + (LateMove.x);
            LateMove.z = (Move.z - LateMove.z) * off + (LateMove.z);
        }
        else
        {
            Cluster = true;
            for(int i = 0; i < ClusterNumber;i++)
            {
                newObj = Instantiate(otherObject, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 0);
        }

        //world���W��camera���W�ɕϊ�
        //targetWorldPosition = transform.position;
        //targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);
        //Vector3 NewPosFix = targetWorldPosition;

        ////UI����ʊO�ɂ����Ȃ��悤��
        //if (NewPosFix.y >= 1030)
        //{
        //    NewPosFix.y = 1030;
        //}
        //if (NewPosFix.y <= 50)
        //{
        //    NewPosFix.y = 50;
        //}
        //if (NewPosFix.x >= 1870)
        //{
        //    NewPosFix.x = 1870;
        //}
        //if (NewPosFix.x <= 50)
        //{
        //    NewPosFix.x = 50;
        //}

        //if (newObj)
        //{
        //    newObj.transform.position = NewPosFix;  //UI�̈ʒu���X�V
        //}
        //if (Miss)
        //{
        //    Destroy(newObj);        //�~�T�C�����v���C���[�ɊO�ꂽ��UI������
        //}
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
        transform.rotation = rot;
        transform.position = new Vector3(transform.position.x,ToPos.y,transform.position.z);
        transform.position += LateMove * Speed;
    }
}   