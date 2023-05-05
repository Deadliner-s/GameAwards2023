using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraEventMove : MonoBehaviour
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

    //private AsyncOperation async; // シーン遷移用

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMove>();           // プレイヤーの移動スクリプトを入れる用
        playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // プレイヤーの回転スクリプトを入れる用

        // シーン読み込み
        //Scene scene = Scene.SceneMax;
        //switch (scene)
        //{
        //    case Scene.Scene1:
        //        async = SceneManager.LoadSceneAsync("Stage2");

        //        break;

        //}
        //async.allowSceneActivation = false;
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
    }

    // シーン遷移処理
    private void SceneMove()
    {
        SceneManager.LoadScene("Stage1");
        //async.allowSceneActivation = true;
    }
}
