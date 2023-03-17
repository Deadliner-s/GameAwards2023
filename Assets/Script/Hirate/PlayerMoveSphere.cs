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
    public float radius = 2.0f;

    // 座標
    float x;
    float y;
    float z;

    // 球面座標の移動速度
    public float thetaSpeed = 0.1f;
    public float phiSpeed = 0.1f;

    // 球面座標のシータとファイ
    private float radianTheta = 0;
    private float radianPhi = 0;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
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
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // 球面座標の更新
        radianTheta += thetaSpeed * inputMove.y * Mathf.Deg2Rad;
        radianPhi += phiSpeed * inputMove.x * Mathf.Deg2Rad;

        // 球面座標を直交座標に変換
        x = radius * Mathf.Cos(radianTheta) * Mathf.Cos(radianPhi);
        y = radius * Mathf.Sin(radianTheta);
        z = radius * Mathf.Cos(radianTheta) * Mathf.Sin(radianPhi);

        Debug.Log("theta:" + radianTheta);
        Debug.Log("phi:" + radianPhi);

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
}
