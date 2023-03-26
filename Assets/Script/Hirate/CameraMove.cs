using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private float playerDistance = 0.0f; // プレイヤーまでの距離
    private Vector3 centerPoint;

    // 動く時間
    public float moveTime = 5.0f;
    public float stopTime = 5.0f;
    // 初期位置と終着位置
    private Vector3 start;
    private Vector3 end;
    // カメラアニメーション用
    //private bool bInput = false;
    public bool bInput = false;
    private bool bMove = false;
    private float elapsedTime; // 経過時間
    private float rate; // 割合
    private GameObject childObj;

    // Start is called before the first frame update
    void Start()
    {
        //カメラとプレイヤーの距離を調べる
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // 矢印の長さ

        // 基準の位置を設定 (x,y,z)座標
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);

        childObj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            elapsedTime = 0;
            start = this.transform.position;
            bInput = true;
        }
        // マニューバ使用時
        if (bInput)
        {
            // マニューバ終了時
            if (bMove && elapsedTime >= moveTime)
            {
                bInput = false;
                bMove = false;
                rate = 0;
                Debug.Log("end");
                return;
            }
            // カメラ移動開始
            if (bMove)
            {
                // スタート地点を現在の座標にする
                transform.position = Vector3.Lerp(start, end, rate);
                this.transform.LookAt(player.transform.position);
                rate = elapsedTime / moveTime;
                //Debug.Log(rate);
            }
            // マニューバ開始時のカメラ移動停止
            if (!bMove && elapsedTime <= stopTime)
            {
                this.transform.LookAt(player.transform.position);
                //Debug.Log("stop");
            }
            // カメラ移動開始の前処理
            if (!bMove && elapsedTime >= stopTime)
            {
                bMove = true;
                elapsedTime = 0;
                end = childObj.transform.position;
            }
            // 時間経過
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
        }
        // 通常時
        if (!bInput)
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
            //Debug.Log("no");
        }
    }
}
