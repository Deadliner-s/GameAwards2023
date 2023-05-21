using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : MonoBehaviour
{
    //[Header("オブジェクト設定")]
    //[Tooltip("プレイヤー")]
    private GameObject player;
    [Tooltip("残像")]
    [SerializeField] GameObject afterimageObj;
    private GameObject[] obj = new GameObject[5];
    private Vector3 pos; // 現在の座標
    private Quaternion quaternion;
    private Quaternion[] lateQuaternion = new Quaternion[5];
    private Vector3[] latePos = new Vector3[5]; // 直前の座標

    private GameObject playerManager;
    //private PlayerMove playerMove;
    private int saveObj;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");
        // PlayerMoveのスクリプトの中身を使用する用
        //playerMove = player.GetComponent<PlayerMove>();

        //// 現在の座標にプレイヤーの座標を代入
        //pos = player.transform.position;
        //// 現在の回転にプレイヤーの回転を代入
        //quaternion = player.transform.rotation;
        //// 残像を生成
        //for (int i = 0; i < 5; i++) {
        //    obj[i] = Instantiate(
        //        afterimageObj, // 残像のオブジェクト
        //        pos,           // 現在の座標
        //        quaternion);   // 回転
        //}
        // 演出用
        saveObj = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // マニューバ開始時
        if (playerManager.GetComponent<PlayerMove>().maneverFlg == true)
        {
            if (playerManager.GetComponent<PlayerMove>().inputMove.y >= 0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x >= 0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.y <= -0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x <= -0.5f)
            {
                //---- 生成
                // 残像を生成
                for (int i = 4; i >= 0; i--)
                {
                    if (obj[i] != null) { continue; } // nullではないなら飛ばす
                    obj[i] = Instantiate(
                    afterimageObj,                 // 残像のオブジェクト
                    new Vector3(9999, 9999, 9999), // 座標
                    quaternion);                   // 回転
                    obj[i].transform.parent = player.transform;
                }

                // 演出用
                saveObj = 0;
                // 現在の座標にプレイヤーの座標を代入
                pos = player.transform.position;
                // 現在の回転にプレイヤーの回転を代入
                quaternion = player.transform.rotation;
                // 直前の座標に現在の座標を代入
                latePos[5 - 1] = pos;
                // 直前の回転に現在の回転を代入
                lateQuaternion[5 - 1] = quaternion;
                //---- 残像を動かす
                for (int i = 0; i < 5; i++)
                {
                    if (i < 4)
                    {
                        // 座標
                        latePos[i] = latePos[i + 1];
                        // 回転
                        lateQuaternion[i] = lateQuaternion[i + 1];
                    }
                    // 直前の座標に現在の座標を代入
                    obj[i].transform.position = latePos[i];
                    // 直前の回転に現在の回転を代入
                    obj[i].transform.rotation = lateQuaternion[i];

                    //if ((obj[i].transform.position.y - obj[i + 1].transform.position.y) < 0.3f)
                    //{
                        //Debug.Log(i + "：" + obj[i].transform.position);
                    //}
                }
            }
        }

        // マニューバ終了時
        if (playerManager.GetComponent<PlayerMove>().maneverFlg == false && saveObj < 5) {
            // モデルの削除処理
            //Destroy(obj[saveObj]);
            // 残像を生成
            for (int i = 4; i >= 0; i--)
            {
                if (obj[i] == null) { continue; }
                obj[i].transform.position = new Vector3(9999, 9999, 9999);
            }
            // 演出用
            saveObj++;
        }
    }
}
