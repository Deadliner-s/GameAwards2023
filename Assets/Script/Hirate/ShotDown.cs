using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDown : MonoBehaviour
{
    // 撃墜時の設定
    [Header("撃墜時の設定")]
    [Tooltip("撃墜までの時間")]
    [SerializeField] float destroyTime = 3.0f;
    [Tooltip("撃墜時の移動")]
    [SerializeField] Vector3 movePos;
    // エフェクト
    [Header("エフェクトの設定")]
    [Tooltip("黒煙エフェクトのオブジェクト")]
    [SerializeField] GameObject effect_1;
    [Tooltip("爆発エフェクトのオブジェクト")]
    [SerializeField] GameObject effect_2;

    private GameObject obj;                  // プレイヤー
    private PlayerMove playerMove;           // プレイヤーの移動を切る用
    private PlayerMoveAngle playerMoveAngle; // プレイヤーの回転を切る用
    private PlayerHp playerHp;               // バリア破壊後の完全撃墜時のフラグ取得用
    private float cnt = 2.0f;                // 爆発までの時間
    private Vector3 pos;                     // 座標
    private Quaternion q;                    // 親の回転の代入用
    private bool GameOverStartFlg = true;    // 黒煙開始用

    public bool EffectFlag { get; protected set; } // エフェクトの演出用

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject; // プレイヤーのオブジェクトを代入
        playerMove = gameObject.GetComponent<PlayerMove>();           // プレイヤーの移動スクリプトを入れる用
        playerMoveAngle = gameObject.GetComponent<PlayerMoveAngle>(); // プレイヤーの回転スクリプトを入れる用
        playerHp = gameObject.GetComponent<PlayerHp>();               // プレイヤーのHPスクリプトを入れる用

        // エフェクトの演出用
        EffectFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        // playerHPに入れる用
        //public bool BreakFlag { get; private set; }

        //BreakFlag = false;

        //if (BreakShieldFlag)
        //{
        //    // 完全に撃墜された判定にする
        //    BreakFlag = true;
        //}

        // 完全に撃墜された時
        if (playerHp.BreakFlag)
        {
            // 撃墜の時間を過ぎた時
            if (cnt > destroyTime)
            {
                // エフェクトを生成
                Instantiate(
                    effect_2, // エフェクトが入ったオブジェクト
                    obj.transform.position, // 座標
                    Quaternion.identity);   // 回転

                // エフェクトの演出用フラグを建てる
                EffectFlag = true;

                // プレイヤーの削除
                Destroy(this.gameObject);

                return;
            }

            // プレイヤーの座標を代入
            pos = obj.transform.position;
            // 撃墜時の移動
            pos += movePos;
            movePos.y *= 1.1f;
            // プレイヤーの座標に代入
            obj.transform.position = pos;

            // 黒煙開始時のみ通る
            if (GameOverStartFlg)
            {
                // プレイヤーの移動のスクリプトを停止
                playerMove.enabled = false;
                // プレイヤーの回転のスクリプトを停止
                playerMoveAngle.enabled = false;

                // 親の回転を代入
                q = gameObject.transform.rotation;
                q *= Quaternion.Euler(-180, 0, 0);
                // エフェクトを生成
                effect_1 = Instantiate(
                    effect_1, // エフェクトが入ったオブジェクト
                    transform.position, // 座標
                    q); // 回転
                // 親に引っ付ける
                effect_1.transform.parent = gameObject.transform;
                // 黒煙開始時処理終了にする
                GameOverStartFlg = false;
            }

            // 撃墜からの時間経過
            cnt += Time.deltaTime;
        }
    }
}
