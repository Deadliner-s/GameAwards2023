using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSphere : MonoBehaviour
{
    // 入力
    private Myproject InputActions;
    private Vector2 inputMove;

    // 中心
    [SerializeField] GameObject center;

    // デバッグ用
#if _DEBUG
    private bool bInput = false;
    private bool bInputU = false;
    private bool bInputO = false;
#endif // _DEBUG

    // 球の半径
    [Header("周回時の半径")]
    [Tooltip("周回時の半径")]
    public float radius = 2.0f;

    // 座標
    private float x;
    private float y;
    private float z;

    // 球面座標の移動
    [Header("上下左右の移動速度")]
    [Tooltip("上下移動速度")]
    public float thetaSpeed = 0.01f;
    [Tooltip("左右移動速度")]
    public float phiSpeed = 0.01f;

    // 球面座標の通常時の移動速度
    [Header("通常時の上下左右全ての速度補正")]
    [Tooltip("通常時の速度補正")]
    public float DefaultSpeed = 1.0f;

    // 球面座標のシータとファイ
    private float radianTheta = 0;
    private float radianPhi = 0;
    private bool maneverFlg = false;

    // クイックマニューバ用変数
    [Header("マニューバ")]
    [Tooltip("マニューバの持続時間")]
    public float ManerverTime = 5.0f;   // 動く時間
    [Tooltip("クールタイム")]
    public float coolTime = 1.0f;       // 再使用できるようになる時間
    [Tooltip("速度")]
    public float ManeverSpeed = 0.5f;   // マニューバ時の速度

    // マニューバカウント
    private float elapsedTime;          // マニューバ経過時間
    private float coolTimeCnt;          // クールタイムの経過時間

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Boost.performed += context => OnBoost();
    }

    // Start is called before the first frame update
    void Start()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        z = this.transform.position.z;

        // 直交座標を球面座標に変換
        radius = Mathf.Sqrt(x * x + y * y + z * z);
        radianTheta = Mathf.Atan(Mathf.Sqrt(x * x + y * y) / z);
        radianPhi = Mathf.Atan(y / x);
        elapsedTime = ManerverTime + coolTime;
        coolTimeCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // 球面座標の更新
        if (maneverFlg == false)
        {
            radianTheta += thetaSpeed * inputMove.y * Mathf.Deg2Rad;
            radianPhi += phiSpeed * inputMove.x * Mathf.Deg2Rad * DefaultSpeed;
        }
        else
        {
            // クイックマニューバ
            radianTheta += ManeverSpeed * inputMove.y * Mathf.Deg2Rad;
            radianPhi += ManeverSpeed * inputMove.x * Mathf.Deg2Rad * DefaultSpeed;
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


        // 球面座標を直交座標に変換
        x = radius * Mathf.Cos(radianTheta) * Mathf.Cos(radianPhi);
        y = radius * Mathf.Sin(radianTheta);
        z = radius * Mathf.Cos(radianTheta) * Mathf.Sin(radianPhi);

        //Debug.Log("theta:" + radianTheta);
        //Debug.Log("phi:" + radianPhi);

        // 座標更新
        transform.position = new Vector3(x, y, z);
    }

    // X軸回転
    private Vector3 RotateAroundX(Vector3 pos, float angle, float radius)
    {
        Vector3 v = pos;
        v.z += radius;
        float a;
        float x;
        float y;
        float z;

        a = angle * Mathf.Deg2Rad;
        x = v.x;
        y = Mathf.Cos(a) * v.y - Mathf.Sin(a) * v.z;
        z = Mathf.Sin(a) * v.y - Mathf.Cos(a) * v.z;

        return new Vector3(x, y, z);
    }
    // Y軸回転
    private Vector3 RotateAroundY(Vector3 pos, float angle, float radius)
    {
        Vector3 v = pos;
        v.z += radius;
        float a;
        float x;
        float y;
        float z;

        a = angle * Mathf.Deg2Rad;
        x = Mathf.Cos(a) * v.x - Mathf.Sin(a) * v.z;
        y = v.y;
        z = -Mathf.Sin(a) * v.x - Mathf.Cos(a) * v.z;

        return new Vector3(x, y, z);
    }
    // Z軸回転
    private Vector3 RotateAroundZ(Vector3 pos, float angle, float radius)
    {
        Vector3 v = pos;
        v.y += radius;
        float a;
        float x;
        float y;
        float z;

        a = angle * Mathf.Deg2Rad;
        x = Mathf.Cos(a) * v.x - Mathf.Sin(a) * v.y;
        y = Mathf.Sin(a) * v.x + Mathf.Cos(a) * v.y;
        z = v.z;

        return new Vector3(x, y, z);
    }

    private void OnBoost()
    {
        // クールタイムが終わっていたら
        if (coolTimeCnt > coolTime)
        {
            maneverFlg = true;
            elapsedTime = 0;
            coolTimeCnt = 0;
        }
    }
}
