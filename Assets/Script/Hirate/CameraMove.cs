using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private float playerDistance = 0.0f; // プレイヤーまでの距離
    private Vector3 centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        //カメラとプレイヤーの距離を調べる
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // 矢印の長さ

        // 基準の位置を設定 (x,y,z)座標
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // カメラをプレイヤーに追従させる
        // 一度プレイヤーの座標と同じにさせる
        Vector3 pos = player.transform.position;
        pos += centerPoint;
        // カメラが向いている方向とは逆向きにプレイヤーから離す
        pos -= transform.forward * playerDistance;
        // 新しいカメラの位置
        transform.position = pos;

        this.transform.LookAt(enemy.transform.position);
    }
}
