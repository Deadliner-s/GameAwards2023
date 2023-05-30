using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxInstance : MonoBehaviour
{
    // スカイボックスのマテリアル
    [Header("背景マテリアル設定")]
    [Tooltip("背景のマテリアル")]
    public static SkyBoxInstance instance;
    public Material skyInstance;
    // 生成したマテリアルを代入する用
    private Material sky;

    // 回転
    private float rotation; // 現在の回転
    // 回転速度の取得とセット
    public float rotationSpeed { get; set; } // 回転速度

    // スカイボックスを生成して入れる
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // 設定したいスカイボックスのマテリアルをスカイボックスに代入
        sky = new Material(skyInstance);
        RenderSettings.skybox = sky;
        // 全てのシーンで使われるため、シーン切り替えで破棄されないようにする
        DontDestroyOnLoad(sky);
    }

    private void Update()
    {
        // 背景の回転
        rotation = Mathf.Repeat(sky.GetFloat("_Rotation") + rotationSpeed, 360f);
        // 処理後の回転を代入
        sky.SetFloat("_Rotation", rotation);
    }

    // 生成したスカイボックスのマテリアルを返す
    public Material GetSky()
    {
        return sky;
    }
}
