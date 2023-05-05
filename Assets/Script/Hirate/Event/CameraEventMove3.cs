using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraEventMove3 : MonoBehaviour
{
    // オブジェクトの設定
    [Header("オブジェクト設定")]
    [Tooltip("プレイヤーのオブジェクト")]
    public GameObject player;

    // 移動距離の設定
    [Header("移動距離の設定")]
    [Tooltip("移動距離")]
    [SerializeField] float move = 2.0f;
    // 移動時間の設定
    [Header("移動時間の設定")]
    // プレイヤーの設定
    [Tooltip("プレイヤーの移動時間")]
    [SerializeField] float moveTimePlayer = 0.5f;
    // カメラの設定
    [Tooltip("カメラの移動時間")]
    [SerializeField] float moveTimeCamera = 1.0f;

    // シーン遷移用
    private enum Scene
    {
        Scene1 = 1,
        Scene2,
        Scene3,

        SceneMax = 99,
    }

    private PlayerMove playerMove;           // プレイヤーの移動を切る用
    private PlayerMoveAngle playerMoveAngle; // プレイヤーの回転を切る用
    private GManager GManager; // シーン切り替え用

    private float elapsedTime = 0.0f; // 経過時間
    private bool bInput = false; // 入力判定用
    private bool bMoveCamera = false; // カメラの移動が始まったかの判定用

    private AsyncOperation async;

    //public bool bInput { get; private set; } = false;
    //public bool bSceneMove { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMove>();           // プレイヤーの移動スクリプトを入れる用
        playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // プレイヤーの回転スクリプトを入れる用

        async = SceneManager.LoadSceneAsync("merge_2");
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && !bInput)
        {
            // プレイヤーの移動のスクリプトを停止
            if (playerMove != null)
            {
                playerMove.enabled = false;
            }
            // プレイヤーの回転のスクリプトを停止
            if (playerMoveAngle != null)
            {
                playerMoveAngle.enabled = false;
            }

            //---- しーむれすな移動 ----
            // 連続で動作させるための前処理
            var DOTMove = DOTween.Sequence();
            // プレイヤーの移動の追加
            float pos = player.transform.position.z;
            DOTMove.Append(player.transform.DOMoveZ(pos + move, moveTimePlayer));
            // カメラの移動の追加
            pos = gameObject.transform.position.z;
            DOTMove.Append(gameObject.transform.DOMoveZ(pos + move, moveTimeCamera));

            // 移動実行
            DOTMove.Play().OnComplete(SceneMove);

            // もう一度押しても実行されないようにする
            bInput = true;
        }

        //if (elapsedTime >= moveTimePlayer && !bMoveCamera && bInput)
        //{
        //    // カメラの移動
        //    //gameObject.transform.DOMoveZ(move, moveTimeCamera);
        //    // もう一度実行されないようにする
        //    bMoveCamera = true;
        //}

        //// シーン遷移処理
        //if (elapsedTime >= (moveTimePlayer + moveTimeCamera))
        //{
        //    SceneManager.LoadScene("Stage1");
        //    //GManager.instance.SetClearFlg((int)Scene.Scene1);
        //}

        //if (bInput)
        //{
        //    // 時間経過
        //    elapsedTime += Time.deltaTime;
        //    Debug.Log(gameObject.transform.position);
        //}
    }

    // シーン遷移処理
    private void SceneMove()
    {
        async.allowSceneActivation = true;
    }

    //void LateUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        elapsedTime = 0;
    //        start = this.transform.position;
    //        bInput = true;
    //    }
    //    // マニューバ使用時
    //    if (bInput)
    //    {
    //        // プレイヤーの移動のスクリプトを停止
    //        playerMove.enabled = false;
    //        // プレイヤーの回転のスクリプトを停止
    //        playerMoveAngle.enabled = false;

    //        // マニューバ終了時
    //        if (bMove && elapsedTime >= moveTime)
    //        {
    //            // プレイヤーの移動のスクリプトを停止
    //            playerMove.enabled = true;
    //            // プレイヤーの回転のスクリプトを停止
    //            playerMoveAngle.enabled = true;
    //            bInput = false;
    //            bMove = false;
    //            rate = 0;
    //            // シーン遷移フラグを建てる
    //            bSceneMove = true;
    //            return;
    //        }
    //        // カメラ移動開始
    //        if (bMove)
    //        {
    //            Vector3 pos = player.transform.position;
    //            pos.x += 0.02f;
    //            pos.z += 0.001f;
    //            player.transform.position = pos;
    //            // スタート地点を現在の座標にする
    //            transform.position = Vector3.Lerp(start, end, rate);
    //            this.transform.LookAt(player.transform.position);
    //            rate = elapsedTime / moveTime;
    //            //Debug.Log(rate);
    //        }
    //        // マニューバ開始時のカメラ移動停止
    //        if (!bMove && elapsedTime <= stopTime)
    //        {
    //            this.transform.LookAt(player.transform.position);
    //            //Debug.Log("stop");
    //        }
    //        // カメラ移動開始の前処理
    //        if (!bMove && elapsedTime >= stopTime)
    //        {
    //            bMove = true;
    //            elapsedTime = 0;
    //            end = childObj.transform.position;
    //        }
    //        // 時間経過
    //        elapsedTime += Time.deltaTime;
    //        //Debug.Log(elapsedTime);

    //        //// カメラをプレイヤーに追従させる
    //        //// 一度プレイヤーの座標と同じにさせる
    //        //Vector3 pos = player.transform.position;
    //        //pos += centerPoint;
    //        //// カメラが向いている方向とは逆向きにプレイヤーから離す
    //        //pos -= transform.forward * playerDistance;
    //        //// 新しいカメラの位置
    //        //transform.position = pos;
    //    }
    //}
}
