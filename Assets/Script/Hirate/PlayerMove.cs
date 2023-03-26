using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour
{
    // 入力
    private Myproject InputActions;
    private Vector2 inputMove;

    // 座標
    private Vector3 pos;            // 現在の位置
    private Vector3 nextPosition;   // 移動後の位置
    private float viewX;            // ビューポート座標のxの値
    private float viewY;            // ビューポート座標のyの値

    [Header("移動速度")]
    [Tooltip("移動速度")]
    public float speed = 0.1f;

    [Header("アタックフェイズ時の上下移動速度")]
    [Tooltip("上下移動速度")]
    public float AtkSpeed = 0.05f;

    // クイックマニューバ用変数
    [Header("マニューバ")]
    [Tooltip("マニューバの持続時間")]
    public float ManerverTime = 0.5f;   // 動く時間
    [Tooltip("クールタイム")]
    public float coolTime = 1.0f;       // 再使用できるようになる時間
    [Tooltip("速度")]
    public float ManeverSpeed = 0.5f;   // マニューバ時の速度

    // マニューバカウント
    private float elapsedTime;          // マニューバ経過時間
    private float coolTimeCnt;          // クールタイムの経過時間
    private bool maneverFlg = false;

    // フェイズ切り替え用
    [Header("フェイズ確認用オブジェクト")]
    [SerializeField] GameObject PhaseObj;
    private bool AtkPhaseFlg;


    [Header("SE関係")]
    public AudioClip MoveSE;
    public AudioClip ManeverSE;
    private AudioSource audioSource;
    private bool MoveSeFlg = false;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Manever.performed += context => OnManever();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        pos = transform.position;
        nextPosition = pos;

        // マニューバ変数初期化
        elapsedTime = ManerverTime + coolTime;
        coolTimeCnt = 0;

        // フェイズ取得
        AtkPhaseFlg = PhaseObj.activeSelf;

        // SE
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MoveSE;
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズ確認
        AtkPhaseFlg = PhaseObj.activeSelf;

        // キー入力
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // 座標計算
        if (maneverFlg == false)
        {
            if (AtkPhaseFlg == false)
            {
                // ハイスピードフェイズ
                // 縦方向の移動
                nextPosition.y = pos.y + inputMove.y * speed;
                // 横方向の移動
                nextPosition.x = pos.x + inputMove.x * speed;
            }
            else
            {
                // アタックフェイズ
                // 縦方向の移動
                nextPosition.y = pos.y + inputMove.y * AtkSpeed;
                // 横方向の移動
                nextPosition.x = pos.x + inputMove.x * speed;
            }
        }
        else
        {
            // クイックマニューバ
            // 縦方向の移動
            nextPosition.y = pos.y + inputMove.y * ManeverSpeed;
            // 横方向の移動
            nextPosition.x = pos.x + inputMove.x * ManeverSpeed;

            // マニューバ経過時間
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime > ManerverTime)
        {
            // マニューバできる時間が過ぎたら
            maneverFlg = false;
            // クールタイムのカウントを始める
            coolTimeCnt += Time.deltaTime;
        }

        // 移動後のビューポート座標のxの値を取得
        viewX = Camera.main.WorldToViewportPoint(nextPosition).x;
        viewY = Camera.main.WorldToViewportPoint(nextPosition).y;

        // 移動後のビューポート座標が０から１の範囲ならば
        if (0.0f <= viewX && viewX <= 1.0f && 0.0f <= viewY && viewY <= 1.0f)
        {
            // 移動更新
            transform.position = nextPosition;

            pos = nextPosition;
        }

        // 動いてる時に鳴るSE
        if (0.0f < inputMove.x)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f > inputMove.x)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f < inputMove.y)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f > inputMove.y)
        {
            if (MoveSeFlg == false)
            {
                audioSource.Play();
                MoveSeFlg = true;
            }
        }
        if (0.0f == inputMove.x && 0.0f == inputMove.y && MoveSeFlg == true)
        {
            audioSource.Stop();
            MoveSeFlg = false;
        }
    }

    private void OnManever()
    {
        // クールタイムが終わっていたら ＆アタックフェイズではなかったら
        if (coolTimeCnt > coolTime)
        {
            maneverFlg = true;
            elapsedTime = 0;
            coolTimeCnt = 0;
            audioSource.PlayOneShot(ManeverSE);
        }
    }
}
