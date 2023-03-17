using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProj : MonoBehaviour
{
    public float Speed;         //ミサイルの速度
    float t = 0;                //time ・ 時間

    Transform FromTransform;    //発射元
    Transform ToTransform;      //発射先

    Vector3 FromPos;
    Vector3 ToPos;

    public float Height;        //ミサイルの高さ
    float ControlPositionX;
    float ControlPositionY;
    float ControlPositionZ;

    // Start is called before the first frame update
    void Start()
    {
        //EnemyBossという名前のものが存在する
        FromPos = GameObject.Find("EnemyBoss").transform.position;
        //Playerという名前のものが存在する
        ToPos = GameObject.Find("Player").transform.position;
        //ミサイルの初期位置を設定
        transform.position = FromPos;
    }

    // Update is called once per frame
    void Update()
    {
        t += Speed;
        //プレイヤー現在の位置を更新・追跡---
       //ToPos = GameObject.Find("Player").transform.position;  //(bug : なぜか未来予測ができる)

        ControlPositionX = FromPos.x + (ToPos.x - FromPos.x) * 0.1f;
        ControlPositionY = FromPos.y + Height;
        ControlPositionZ = FromPos.z + (ToPos.z - FromPos.z) * 0.1f;

        //ミサイルの過去の位置を記録
        Vector3 oldPos = transform.position;

        //ミサイルの次の位置を計算
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
        //ミサイルの回転を計算
        Vector3 Pos = new Vector3(nextPosX, nextPosY, nextPosZ);
        Vector3 Normal = Pos - oldPos;
        Quaternion rot = Quaternion.FromToRotation(new Vector3(0.0f,1.0f,0.0f),Normal.normalized);

        //ミサイルの位置と回転を更新
        transform.rotation = rot;
        transform.position = Pos;
    }
}