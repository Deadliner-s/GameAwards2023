using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoveManager : MonoBehaviour
{
    // 現在のシーンの設定
    [Header("現在のシーン設定")]
    [Tooltip("現在のシーン")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNow;

    // 次のシーンの設定
    [Header("次のシーン設定")]
    [Tooltip("次のシーン")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneNext;

    // 次のシーンの事前ロードの設定
    [Header("次のシーンの事前ロード設定")]
    [Tooltip("ロードの有無")]
    [SerializeField] bool bLoad = true;

    // 事前ロードを渡す時用
    public bool bLoadGet { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // 事前ロードするかどうかの判定
        if (bLoad)
        {
            // 次のシーン読み込み
            SceneLoad(SceneNext);
        }

        // 事前ロードを渡せるように設定を代入
        bLoadGet = bLoad;
    }

    // 次のシーンのロード
    public void SceneLoad(SceneLoadStartUnload.SCENE_NAME loadscene)
    {
        // 次のシーン読み込み
        SceneAccessSearch.SceneAccessCatchLoad(loadscene);
    }

    // 次のシーンのロード と 現在のシーンのアンロード
    public void SceneStartUnload()
    {
        // 次のシーンの起動
        SceneAccessSearch.SceneAccessCatchStart();
        // 現在のシーンのアンロード
        SceneAccessSearch.SceneAccessCatchUnload(SceneNow);
    }
}
