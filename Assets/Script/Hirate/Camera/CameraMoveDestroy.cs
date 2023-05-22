using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMoveDestroy : MonoBehaviour
{
    // プレイヤー
    [Header("オブジェクト設定")]
    private GameObject player;          // プレイヤー
    private GameObject playerManager;   // プレイヤーマネージャー
    [Tooltip("見る先のオブジェクト")]
    [SerializeField] GameObject obj;
    // エフェクト
    [Tooltip("エフェクトの演出時間")]
    [SerializeField] float effectTime = 3.0f;

    // 現在のシーンの設定
    [Header("現在のシーン設定")]
    [Tooltip("現在のシーン")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNow;

    // 次のシーンの設定
    [Header("次のシーン設定")]
    [Tooltip("次のシーン")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNext;

    private float playerDistance = 0.0f; // プレイヤーまでの距離
    private Vector3 centerPoint;
    //private PlayerHp playerHp; // バリア破壊後の完全撃墜時のフラグ取得用
    //private ShotDown shotDown; // エフェクト演出用
    private float cnt = 0.0f;  // 経過時間
    private bool breakStart;   // 撃破開始

    [SerializeField]
    private GameObject fade; // フェードオブジェクト
    private bool bFade = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");

        //// プレイヤーのHPスクリプトを入れる用
        //playerHp = player.GetComponent<PlayerHp>();
        //// 撃墜時のスクリプトを入れる用
        //shotDown = player.GetComponent<ShotDown>();

        //カメラとプレイヤーの距離を調べる
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // 矢印の長さ

        // 基準の位置を設定 (x,y,z)座標
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);
        // 撃破開始フラグ
        breakStart = false;
    }

    void LateUpdate()
    {
        // 完全に撃墜された時
        if (player != null && playerManager.GetComponent<PlayerHp>().BreakFlag && !breakStart)
        {
            // カメラをプレイヤーに追従させる
            // 一度プレイヤーの座標と同じにさせる
            Vector3 pos = player.transform.position;
            pos += centerPoint;
            // カメラが向いている方向とは逆向きにプレイヤーから離す
            pos -= transform.forward * playerDistance / 2.3f;
            // 新しいカメラの位置
            transform.position = pos;
            // 撃破開始フラグを建てる
            breakStart = true;
        }
        // 位置変更後はカメラは追うだけにする
        if (player != null && breakStart)
        {
            // カメラでプレイヤーを追う
            gameObject.transform.LookAt(obj.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
        }

        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerManager");
        }

        if (bFade) { return; }
        if (playerManager.GetComponent<ShotDown>().EffectFlag)
        {
            if (cnt > effectTime)
            {
                // シーン移動
                //SceneManager.LoadScene("GameOver");
                fade.GetComponent<Fade>().StartCoroutine(
                    fade.GetComponent<Fade>().Color_FadeOut_GameOver(SceneNow));
                bFade = true;
            }
            // 撃墜からの時間経過
            cnt += Time.deltaTime;
        }
    }
}
