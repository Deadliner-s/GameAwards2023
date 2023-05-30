using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using DG.Tweening;

public class CameraEventMove2 : MonoBehaviour
{
    // 移動距離の設定
    //[Header("移動距離の設定")]
    //[Tooltip("移動距離")]
    //[SerializeField] float move = 5.0f;
    // 移動時間の設定
    //[Header("移動時間の設定")]
    // ボスの設定
    //[Tooltip("ボスの移動時間")]
    //[SerializeField] float moveTimeBoss = 0.5f;
    // カメラの設定
    //[Tooltip("カメラの移動時間")]
    //[SerializeField] float moveTimeCamera = 1.0f;

    private GameObject player; // プレイヤーオブジェクト
    private GameObject boss; // ボスオブジェクト
    private PlayerMove playerMove;           // プレイヤーの移動を切る用
    private PlayerMoveAngle playerMoveAngle; // プレイヤーの回転を切る用

    private Vector3 initPos;
    //private bool bInput = false; // 入力判定用

    //private AsyncOperation async;

    private GameObject fade; // フェードオブジェクト

    // 制限時間の設定
    [Header("制限時間")]
    [Tooltip("制限時間")]
    [SerializeField] float TimeLimit = 60; //制限時間
    private float Counttime; //時間を測る

    private bool bSceneStart = false;
    public bool bAniEnd { get; set; } = false; // アニメーションの終了取得用

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの取得
        player = GameObject.FindGameObjectWithTag("Player");
        // ボスの取得
        boss = GameObject.FindGameObjectWithTag("BossParent");
        // フェードの取得
        fade = GameObject.FindGameObjectWithTag("Fade");

        playerMove = player.GetComponent<PlayerMove>();           // プレイヤーの移動スクリプトを入れる用
        playerMoveAngle = player.GetComponent<PlayerMoveAngle>(); // プレイヤーの回転スクリプトを入れる用

        // カメラの初期位置を取得
        initPos = gameObject.transform.position;

        //async = SceneManager.LoadSceneAsync("Stage2");
        //async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
    }

    // シーン遷移処理
    private void SceneMove()
    {
        //async.allowSceneActivation = true;
        //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2");

        // SceneMoveManagerをタグ検索
        GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
        // ボスのローテーションを代入する
        GameObject boss = GameObject.FindGameObjectWithTag("BossParent");
        BossRotationCatch.instance.fRotation = boss.transform.rotation;
        // シーンの開始
        obj.GetComponent<SceneMoveManager>().SceneStartUnload();
    }

    // カウントダウン
    private void CountTime()
    {
        Counttime += Time.deltaTime;//時間を足す

        if (Counttime > TimeLimit)
        {
            // シームレスな遷移
            //Seamless();

            // アニメーション終了したかの判定
            if (bAniEnd)
            {
                // シーン遷移
                SceneMove();
            }
        }

        if (Input.GetKeyDown(KeyCode.H) ||
            Input.GetKeyDown(KeyCode.P))
        {
            if (bSceneStart) { return; }

            // シーン遷移
            SceneMove();

            bSceneStart = true;
        }
    }

    // シームレスな遷移
    private void Seamless()
    {
        //if (bInput) { return; }
        //// プレイヤーの移動のスクリプトを停止
        //if (playerMove != null)
        //{
        //    playerMove.enabled = false;
        //}
        //// プレイヤーの回転のスクリプトを停止
        //if (playerMoveAngle != null)
        //{
        //    playerMoveAngle.enabled = false;
        //}

        ////---- しーむれすな移動 ----
        //// 連続で動作させるための前処理
        //var DOTMove = DOTween.Sequence();
        ////ボスの移動の追加
        //float pos = boss.transform.position.z;
        //DOTMove.Append(boss.transform.DOMoveZ(pos - move, moveTimeBoss));
        //// カメラの移動の追加
        //pos = gameObject.transform.position.z;
        //DOTMove.Join(gameObject.transform.DOMoveZ(
        //    pos - move, moveTimeBoss));
        //// 両方動かした後、カメラの移動を行う
        //// その後、シーン遷移処理
        //DOTMove.Append(gameObject.transform.DOMoveZ(
        //    initPos.z, moveTimeCamera));
        //// 移動実行
        //DOTMove.Play().OnComplete(SceneMove);

        //// もう一度押しても実行されないようにする
        //bInput = true;
    }
}
