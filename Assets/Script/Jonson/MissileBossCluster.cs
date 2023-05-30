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

    float off;
    bool Cluster;

    GameObject newObj;
    public GameObject otherObject;        // ��������v���n�u�I�u�W�F�N�g
    private Camera mainCamera;            // ���C���J����
    private Vector3 targetWorldPosition;  // �ڕW���[���h���W

    bool BossFlg;
    GameObject Player;
    Vector3 ToPos;              //���ː�
    Vector3 Move;               //�ړ�����
    Vector3 LateMove;           //���炩���������邽�߂�move�ϐ�

    // Start is called before the first frame update
    void Start()
    {
        BossFlg = GameObject.Find("BossManager").GetComponent<MainBossHp>();
        if(BossFlg)
        {
            if (Player = GameObject.Find("Player"))
            {
                ToPos = Player.transform.position; //Player
                mainCamera = Camera.main;                             // ���C���J�������擾����

                Cluster = false;
                off = 1.0f;
            }
            else
            {
                Destroy(this, 0.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player && BossFlg)
        {
            ToPos = Player.transform.position;   //�v���C���[�̈ʒu
            Speed += Accel;                                         //�����x
            if (Speed >= MaxSpeed)                                  //���x����
                Speed = MaxSpeed;
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(ToPos.x, 0, ToPos.z));
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
                for (int i = 0; i < ClusterNumber; i++)
                {
                    newObj = Instantiate(otherObject, transform.position, Quaternion.Euler(0.0f,0.0f,270.0f));
                    // MissileObj���^�O����
                    GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
                    // �~�T�C���I�u�W�F�N�g�̎q�ɂ���
                    newObj.transform.parent = missileObj.transform;
                }
                Destroy(gameObject, 0);
            }

            //world���W��camera���W�ɕϊ�
            targetWorldPosition = transform.position;
            targetWorldPosition = mainCamera.WorldToScreenPoint(targetWorldPosition);

            Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f, 1.0f, 0.0f), LateMove);
            transform.rotation = rot;
            if (targetWorldPosition.x <= -100.0f)
            {
                transform.position = new Vector3(transform.position.x, ToPos.y, transform.position.z);
            }
            transform.position += LateMove * Speed * Time.timeScale;
        }
        else
        {
            Destroy(this, 0.0f);
        }
    }
}   