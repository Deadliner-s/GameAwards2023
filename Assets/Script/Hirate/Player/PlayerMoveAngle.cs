using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveAngle : MonoBehaviour
{
    private GameObject player;

    //---- インスペクターに見せる変数 ----
    // 回転
    [Header("回転")]
    [Tooltip("回転の最大値")]
    public float angleMax = 45; // 回転の最大値
    [Tooltip("回転量")]
    public Vector3 angleAdd = new Vector3(5,0,0);  // 回転量
    // 水平化
    [Header("水平")]
    [Tooltip("水平に戻る時の補正量")]
    public float horizon = 1; // 水平に戻る時の量
    //[Tooltip("回転量の補正\n(上向きになる時の補正用)")]
    //public float angleCorrection; // 回転量の補正

    //---- 変数宣言 ----
    private Vector3 angle;        // 回転
   // private Myproject InputActions; // 入力
    private Vector2 inputMove;
    private GameObject cameraObj;

    //void Awake()
    //{
    //    InputActions = new Myproject();
    //    InputActions.Enable();
    //    InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
    //}

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        cameraObj = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if (cameraObj == null)
        {
            cameraObj = GameObject.Find("Main Camera");
        }

        // 現在の値を代入
        // 回転
        angle = player.transform.eulerAngles;
        // そのままだと計算しにくいため、水平時を0とした数値にする
        if (angle.x >= 315) { angle.x = angle.x - 360; }

        // キー入力
        inputMove = InputManager.instance.OnMove();

        // 回転
        if (inputMove.y < 0.5f) {
            angle += angleAdd * Time.timeScale;
        }
        if (inputMove.y > -0.5f) {
            angle -= angleAdd * Time.timeScale;// * angleCorrection;
        }

        // カメラに対してずっと右を向き続ける
        if (cameraObj != null)
        {
            Transform cameraTransform = cameraObj.transform;
            Vector3 cameraAngle = cameraTransform.eulerAngles;
            angle.y = cameraAngle.y + 90.0f;
        }


        // 水平にする
        Horizon();

        // 回転の修正
        // 下方向の修正
        if (angle.x >= angleMax) { angle.x = angleMax; }
        // 上方向の修正
        if (angle.x <= -angleMax) {
            angle.x = -angleMax;
            angle.x = 360 + angle.x;
        }

        // 回転の代入
        player.transform.eulerAngles = angle;
    }

    //void FixedUpdate()
    //{

    //}

    //〜〜〜〜 水平にする関数 〜〜〜〜
    private void Horizon()
    {
        // 下
        if (angle.x >= 0) {
            angle.x = angle.x - horizon * Time.timeScale;
            if (angle.x <= 0) { angle.x = 0; }
        }
        // 上
        if (angle.x < 0) {
            angle.x = angle.x + horizon * Time.timeScale;
            if (angle.x >= 0) { angle.x = 0; }
        }
    }
}
