using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveManever : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private float playerDistance = 0.0f; // プレイヤーまでの距離
    private Vector3 centerPoint;
    private GameObject parentObj;
    private CameraMove cameraMove;

    // Start is called before the first frame update
    void Start()
    {
        //カメラとプレイヤーの距離を調べる
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // 矢印の長さ

        parentObj = transform.parent.gameObject;
        cameraMove = parentObj.GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMove.bInput)
        {
            // 親(プレイヤー)に追従させる
            // 一度プレイヤーの座標と同じにさせる
            Vector3 pos = player.transform.position;
            pos += centerPoint;
            // 本来のカメラが向いている方向とは逆向きにプレイヤーから離す
            pos -= transform.forward * playerDistance;
            // 新しいダミーオブジェクトの位置
            transform.position = pos;
            this.transform.LookAt(enemy.transform.position);
        }
    }
}
