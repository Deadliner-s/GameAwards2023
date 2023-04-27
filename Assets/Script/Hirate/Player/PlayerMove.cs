using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhaseManager;
using Effekseer;
using UnityEngine.InputSystem;

public class PlayerMove: MonoBehaviour
{
    // 入力
    private Myproject InputActions;
    public Vector2 inputMove { get; private set; }

    // 座標
    private Vector3 pos;            // 現在の位置
    private Vector3 nextPosition;   // 移動後の位置
    private float viewX;            // ビューポート座標のxの値
    private float viewY;            // ビューポート座標のyの値

    [Header("通常フェイズ時の移動速度")]
    [Tooltip("縦移動")]
    public float Normal_VerticalSpeed = 0.01f;
    [Tooltip("横移動")]
    public float Normal_HorizontalSpeed = 0.01f;

    [Header("ハイスピードフェイズ時の移動速度")]
    [Tooltip("縦移動")]
    public float Spd_VerticalSpeed = 0.005f;
    [Tooltip("横移動")]
    public float Spd_HorizontalSpeed = 0.01f;

    [Header("アタックフェイズ時の移動速度")]
    [Tooltip("縦移動")]
    public float Atk_VerticalSpeed = 0.01f;
    [Tooltip("横移動")]
    public float Atk_HorizontalSpeed = 0.01f;

    // クイックマニューバ用変数
    [Header("マニューバ時間")]
    [Tooltip("マニューバの持続時間")]
    public float ManerverTime = 0.5f;   // 動く時間
    [Tooltip("クールタイム")]
    public float coolTime = 1.0f;       // 再使用できるようになる時間
    [Header("マニューバ時の移動速度")]
    [Tooltip("縦移動")]
    public float Manever_VerticalSpeed = 0.03f;   // マニューバ時の速度
    [Tooltip("横移動")]
    public float Manever_HorizontalSpeed = 0.03f;

    // マニューバカウント
    private float elapsedTime;          // マニューバ経過時間
    private float coolTimeCnt;          // クールタイムの経過時間
    //private bool maneverFlg = false;
    public bool maneverFlg { get; private set; } = false; // マニューバのフラグ

    [Header("SE関係")]
    public AudioClip MoveSE;
    private AudioSource audioSource;
    private bool MoveSeFlg = false;

    [Header("エフェクト")]
    public EffekseerEffectAsset effect;     // 再生するエフェクト
    private EffekseerHandle handle;

    // 現在フェイズ
    private PhaseManager.Phase currentPhase;
    private PhaseManager.Phase nextPhase;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Manever.performed += context => OnManever();

        InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
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
        currentPhase = PhaseManager.instance.GetPhase();

        // SE
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MoveSE;

        // エフェクトを取得する。
        effect = Resources.Load<EffekseerEffectAsset>(effect.name);

        nextPhase = currentPhase;
    }

    // Update is called once per frame
    void Update()
    {
        // フェイズ取得
        currentPhase = PhaseManager.instance.GetPhase();

        // キー入力
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // 座標計算
        if (maneverFlg == false)
        {
            if(currentPhase == PhaseManager.Phase.Normal_Phase)
            {
                // 通常フェイズ
                // 縦方向の移動
                nextPosition.y = pos.y + inputMove.y * Normal_VerticalSpeed;
                // 横方向の移動
                nextPosition.x = pos.x + inputMove.x * Normal_HorizontalSpeed;
            }
            else if (currentPhase == PhaseManager.Phase.Speed_Phase)
            {
                // ハイスピードフェイズ
                // 縦方向の移動
                nextPosition.y = pos.y + inputMove.y * Spd_VerticalSpeed;
                // 横方向の移動
                nextPosition.x = pos.x + inputMove.x * Spd_HorizontalSpeed;
            }
            else if(currentPhase == PhaseManager.Phase.Attack_Phase)
            {
                // アタックフェイズ
                // 縦方向の移動
                nextPosition.y = pos.y + inputMove.y * Atk_VerticalSpeed;
                // 横方向の移動
                nextPosition.x = pos.x + inputMove.x * Atk_HorizontalSpeed;
            }
        }
        else
        {
            // クイックマニューバ
            // 縦方向の移動
            nextPosition.y = pos.y + inputMove.y * Manever_VerticalSpeed;
            // 横方向の移動
            nextPosition.x = pos.x + inputMove.x * Manever_HorizontalSpeed;

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

        // フェイズが変わった場合
        if (nextPhase != currentPhase)
        {
            nextPhase = currentPhase;
            // 通常フェイズ
            if (currentPhase == Phase.Normal_Phase)
            {
                
            }
            // ハイスピードフェイズ
            if (currentPhase == Phase.Speed_Phase)
            {
                // transformの位置でエフェクトを再生する
                handle = EffekseerSystem.PlayEffect(effect, transform.position);
                // transformの回転を設定する。
                // プレイヤーの傾きに影響されない
                Vector3 eulerAngles = this.transform.eulerAngles;
                Vector3 rot = new Vector3(0.0f, eulerAngles.y, eulerAngles.z);
                Quaternion quaternion = Quaternion.Euler(rot);
                handle.SetLocation(transform.position);
                handle.SetRotation(quaternion);
            }
            // アタックフェイズ
            if (currentPhase == Phase.Attack_Phase)
            {
                
            }
        }
        // エフェクトをプレイヤーに追従させる
        handle.SetLocation(transform.position);
    }



    private void OnManever()
    {
        // クールタイムが終わっていたら ＆アタックフェイズではなかったら
        if (coolTimeCnt > coolTime)
        {
            if (inputMove.y >= 0.5f ||
                inputMove.x >= 0.5f ||
                inputMove.y <= -0.5f ||
                inputMove.x <= -0.5f)
            {
                maneverFlg = true;
                elapsedTime = 0;
                coolTimeCnt = 0;
                SoundManager.instance.PlaySE("Manever");
            }
        }
    }
}
