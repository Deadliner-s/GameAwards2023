using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxInstance : MonoBehaviour
{
    // スカイボックスのマテリアル
    [Header("背景マテリアル設定")]
    [Tooltip("背景のマテリアル")]
    //public SkyBoxInstance instance;
    public Material skyInstance;

    // スカイボックスを生成して入れる
    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}

        // 設定したいスカイボックスのマテリアルをスカイボックスに代入
        RenderSettings.skybox = new Material(skyInstance);
        // 全てのシーンで使われるため、シーン切り替えで破棄されないようにする
        DontDestroyOnLoad(skyInstance);
    }
    // 生成したスカイボックスのマテリアルを返す
    public Material GetSky()
    {
        return skyInstance;
    }
}
