using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveSceneA : MonoBehaviour
{
    private GameObject player;
    private GameObject playerManager;

    public GameObject next;

    private float playerDistance = 0.0f; // プレイヤーまでの距離
    private Vector3 centerPoint;

    // 動く時間
    [SerializeField] float moveTime = 5.0f;
    [SerializeField] float stopTime = 5.0f;
    // 初期位置と終着位置
    private Vector3 start;
    private Vector3 end;
    // カメラアニメーション用
    private bool bMove = false;
    private float elapsedTime; // 経過時間
    private float rate; // 割合
    private GameObject childObj;
    //private PlayerMove playerMove;           // プレイヤーの移動を切る用
    //private PlayerMoveAngle playerMoveAngle; // プレイヤーの回転を切る用

    public bool bInput { get; private set; } = false;
    public bool bSceneMove { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");

        //カメラとプレイヤーの距離を調べる
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // 矢印の長さ

        // 基準の位置を設定 (x,y,z)座標
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);

        childObj = transform.GetChild(0).gameObject;
        //playerMove = player.GetComponent<PlayerMove>();           // プレイヤーの移動スクリプトを入れる用
        //playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // プレイヤーの回転スクリプトを入れる用
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            elapsedTime = 0;
            start = this.transform.position;
            bInput = true;
            next.SetActive(true);
        }
        // マニューバ使用時
        if (bInput)
        {
            // プレイヤーの移動のスクリプトを停止
            playerManager.GetComponent<PlayerMove>().enabled = false;
            // プレイヤーの回転のスクリプトを停止
            playerManager.GetComponent<PlayerMoveAngle>().enabled = false;

            // マニューバ終了時
            if (bMove && elapsedTime >= moveTime)
            {
                // プレイヤーの移動のスクリプトを停止
                playerManager.GetComponent<PlayerMove>().enabled = true;
                // プレイヤーの回転のスクリプトを停止
                playerManager.GetComponent<PlayerMoveAngle>().enabled = true;
                bInput = false;
                bMove = false;
                rate = 0;
                // シーン遷移フラグを建てる
                bSceneMove = true;
                return;
            }
            // カメラ移動開始
            if (bMove)
            {
                Vector3 pos = player.transform.position;
                pos.x += 0.02f;
                pos.z += 0.001f;
                player.transform.position = pos;
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
            //Debug.Log(elapsedTime);
        }
    }
}
