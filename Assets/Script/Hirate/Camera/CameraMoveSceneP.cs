using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMoveSceneP : MonoBehaviour
{
    public GameObject player;

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

    public bool bInput { get; private set; } = false;
    public bool bSceneMove { get; private set; } = false;

    //private AsyncOperation async;
    [SerializeField]
    private GameObject fade; // フェードオブジェクト

    public bool bAniEnd { get; set; } = false; // アニメーションの終了取得用

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

        //async = SceneManager.LoadSceneAsync("Stage1");
        //async.allowSceneActivation = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) ||
            Input.GetKeyDown(KeyCode.H))
        {
            // SceneLoadManagerをタグ検索
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // シーンの開始
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();
        }

        if (bAniEnd)
        {
            // SceneLoadManagerをタグ検索
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // シーンの開始
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();
        }
    }

    private void Ani()
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
                // シーン遷移フラグを建てる
                bSceneMove = true;
                SceneMove();
                return;
            }
            // カメラ移動開始
            if (bMove)
            {
                Vector3 pos = player.transform.position;
                pos.x += 0.02f;
                pos.z += 0.005f;
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

    // シーン遷移処理
    private void SceneMove()
    {
        //async.allowSceneActivation = true;
        //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage1");
        StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut());
    }
}
