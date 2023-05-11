using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxInstance : MonoBehaviour
{
    // スカイボックスのマテリアル
    [Header("背景マテリアル設定")]
    [Tooltip("背景のマテリアル")]
    public Material skyInstance;

    // スカイボックスを生成して入れる
    void Awake()
    {
        RenderSettings.skybox = new Material(skyInstance);
    }

    // オブジェクトが破棄された時実行
    private void OnDestroy()
    {
        if (skyInstance != null)
        {
            Destroy(skyInstance);
            skyInstance = null;
        }
    }
}
