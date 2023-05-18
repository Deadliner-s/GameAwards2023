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

    // Start is called before the first frame update
    void Start()
    {
        // 次のシーン読み込み
        SceneAccessSearch.SceneAccessCatchLoad(SceneNext);
    }

    // 次のシーンのロード と 現在のシーンのアンロード
    public void SceneLoadUnload()
    {
        // 次のシーンのロード
        SceneAccessSearch.SceneAccessCatchStart();
        // 現在のシーンのアンロード
        SceneAccessSearch.SceneAccessCatchUnload(SceneNow);
    }

    // 次のシーンのロード と 現在のシーンのアンロードの呼び出し
    public static void SceneLoadUnloadCall()
    {
        // SceneLoadManagerをタグ検索
        GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
        // シーンの開始
        obj.GetComponent<SceneMoveManager>().SceneLoadUnload();
    }
}
