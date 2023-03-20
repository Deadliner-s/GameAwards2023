using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    // 背景の回転速度
    [Header("背景の回転速度")]
    [Tooltip("回転速度")]
    public float rotationSpeed;
    // 背景のオブジェクト
    [Header("背景オブジェクト設定")]
    [Tooltip("背景のオブジェクト")]
    public Material sky;

    // 回転
    private float rotation; // 現在の回転
    private float initRotation; // 回転の初期位置

    // Start is called before the first frame update
    void Start()
    {
        // 終了時に現在の回転が上書きされるため、最初に取得
        // 回転の初期位置
        initRotation = sky.GetFloat("_Rotation");
    }

    // Update is called once per frame
    void Update()
    {
        // 背景の回転
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
        // 処理後の回転を代入
        sky.SetFloat("_Rotation", rotation);
// デバッグ用
#if _DEBUG
        Debug.Log(rotation);
#endif // _DEBUG
    }
    // アプリケーション終了時の処理
    private void OnApplicationQuit()
    {
        // 現在の回転を初期位置に上書き
        sky.SetFloat("_Rotation", initRotation);
    }
}
