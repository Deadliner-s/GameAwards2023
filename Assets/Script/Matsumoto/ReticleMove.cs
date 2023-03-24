using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReticleMove : MonoBehaviour
{
    public float speed = 5.0f;      // 移動スピード

    private Myproject InputActions; // InputSystem
    private Vector3 pos;            // 現在の位置
    private Vector3 nextPosition;   // 移動後の位置
    private float viewX;            // ビューポート座標のxの値
    private float viewY;            // ビューポート座標のyの値
    private GameObject player;     // プレイヤー


    private GameObject[] RockOnCnt = new GameObject[5];     // ロックオンできる数オブジェクト
    private GameObject RockOnCntPrefab;                     // ●のPrefab
    private float offsetY = 40.0f;

    void Awake()
    {
        // コントローラー
        InputActions = new Myproject();
        InputActions.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置
        player = GameObject.Find("Player");
        Vector3 playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        transform.position = playerPos;

        // 初期化
        pos = transform.position;
        nextPosition = pos;


        // ロックオンできる数分、●を生成する
        for (int i = 0; i < 5; i++)
        {
            RockOnCntPrefab = (GameObject)Resources.Load("RockOnCnt");
            RockOnCnt[i] = Instantiate(RockOnCntPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
        // 照準の下に表示させる
        Vector3 thisPos = this.transform.position;
        RockOnCnt[4].transform.position = new Vector3(thisPos.x - 25.0f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[3].transform.position = new Vector3(thisPos.x - 12.5f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[2].transform.position = new Vector3(thisPos.x, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[1].transform.position = new Vector3(thisPos.x + 12.5f, thisPos.y - offsetY, thisPos.z);
        RockOnCnt[0].transform.position = new Vector3(thisPos.x + 25.0f, thisPos.y - offsetY, thisPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        // 入力
        Vector3 move = InputActions.Player.Reticle.ReadValue<Vector2>();
        nextPosition = pos + move * speed;

        // 移動後のビューポート座標のxの値を取得
        viewX = Camera.main.ScreenToViewportPoint(nextPosition).x;
        viewY = Camera.main.ScreenToViewportPoint(nextPosition).y;

        // 移動後のビューポート座標が０から１の範囲ならば
        if (0.0f <= viewX && viewX <= 1.0f && 0.0f <= viewY && viewY <= 1.0f)
        {
            // 移動
            transform.position = nextPosition;

            pos = nextPosition;
        }

        // ロックオンしたターゲットタグが付いたオブジェクトをカウント
        GameObject[] tagObj = GameObject.FindGameObjectsWithTag("Target");

        // ロックオンした数によって色を変える
        if (tagObj.Length > 0)
        {
            for (int i = 0; i < tagObj.Length; i++)
            {
                Color color = RockOnCnt[i].GetComponent<Image>().color = Color.gray;
            }
        }
        // ロックオン解除されたら色を戻す
        for (int i = tagObj.Length; i < 5; i++)
        {
            Color color = RockOnCnt[i].GetComponent<Image>().color = Color.white;
        }
    }
}
