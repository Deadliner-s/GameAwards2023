using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReticleMove : MonoBehaviour
{
    public float speed = 5.0f;      // 移動スピード

    private Myproject InputActions; // InputSystem
    private Vector3 pos;            // 現在の位置
    private Vector3 nextPosition;   // 移動後の位置
    private float viewX;            // ビューポート座標のxの値
    private float viewY;            // ビューポート座標のyの値
    private GameObject player;     // プレイヤー

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
    }
}
