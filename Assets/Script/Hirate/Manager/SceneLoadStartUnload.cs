using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadStartUnload : MonoBehaviour
{
    // シーンの名前(enum)
    // ※「enum」と「文字列」の両方に同じ場所に追加すること！
    public enum SCENE_NAME
    {
        E_STAGE1 = 0,       // ステージ1
        E_STAGE2,           // ステージ2
        E_STAGE3,           // ステージ3
        E_TITLE,            // タイトル
        E_PROLOGUE,         // プロローグ
        E_STAGE2_EVENT,     // ステージ1と2の間のイベント
        E_STAGE3_EVENT,     // ステージ2と3の間のイベント
        E_EPILOGUE,         // エピローグ
        E_RESULT_COMPLETED, // リザルト(俺の勝ち)
        E_RESULT_FAILED,    // リザルト(僕の負け)
        E_DUMMY,            // ダミー

        E_SCENE_MAX         // シーンの最大
    }

    // シーン名(文字列)
    // ※「enum」と「文字列」の両方に同じ場所に追加すること！
    private string[] sSceneName = {
        "Stage1",      // ステージ1
        "Stage2",      // ステージ2
        "merge_2",     // ステージ3
        "Title",       // タイトル
        "Prologue",    // プロローグ
        "Stage2Event", // ステージ1と2の間のイベント
        "Stage3Event", // ステージ2と3の間のイベント
        "Epilogue",    // エピローグ
        "GameClear",   // リザルト(俺の勝ち)
        "GameOver",    // リザルト(僕の負け)
        "Dummy",       // ダミー

        "Scene_Max"    // シーンの最大
    };

    //---- 読み込み用 ----
    // 単品
    private AsyncOperation async = new AsyncOperation();
    // 全て
    private AsyncOperation[] asyncAll = new AsyncOperation[(int)SCENE_NAME.E_SCENE_MAX];

    // 読み込み終了時用フラグ
    public bool bLoad { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        // 一旦nullを代入
        async = null;
        for (int i = 0; i < (int)SCENE_NAME.E_SCENE_MAX; i++)
        {
            asyncAll[i] = null;
        }
        // シーン読み込み
        //SceneLoadAll();
    }

    //～～～～ 一つのシーンのみロード、開始、アンロード ～～～～
    // シーンのロード
    public void SceneLoad(SCENE_NAME scene_name)
    {
        // 引数に入力されたシーンをロード
        async = SceneManager.LoadSceneAsync(sSceneName[(int)scene_name], LoadSceneMode.Additive);
        //async.completed += SceneLoadFinish;
        async.allowSceneActivation = false;
    }

    // シーン起動関数
    public void SceneStart()
    {
        // シーンのNULLチェック
        if (async == null)
        {
            Debug.Log("SceneStart：NULLです");
            return;
        }

        // シーンを起動
        async.allowSceneActivation = true;
    }

    // シーンの起動(ロードと起動同時用)
    private void SceneStart(AsyncOperation obj)
    {
        // シーンのNULLチェック
        if (async == null)
        {
            Debug.Log("SceneStart：NULLです");
            return;
        }

        // シーンを起動
        async.allowSceneActivation = true;
        //do
        //{
        //    // シーンを起動
        //    async.allowSceneActivation = true;
        //    // ロード完了フラグを建てる
        //    bLoad = false;
        //}
        //while (!bLoad);
    }

    // シーンのロードと起動
    public void SceneLoadStart(SCENE_NAME scene_name)
    {
        // 引数に入力されたシーンをロード
        async = SceneManager.LoadSceneAsync(sSceneName[(int)scene_name], LoadSceneMode.Additive);
        // ロード完了後に起動
        async.completed += SceneStart;
    }

    // シーンのアンロード
    public void SceneUnload(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (async == null)
        {
            Debug.Log("SceneUnload：シーンがNULLです");
            return;
        }
        // 引数に入力されたシーンをアンロード
        async = SceneManager.UnloadSceneAsync(sSceneName[(int)scene_name]);
    }

    // シーンロードマネージャーの取得 と シーンの開始
    public void SceneLoadCatchLoad(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (async == null)
        {
            Debug.Log("このシーンは現在のシーン、もしくはNULLです");
            return;
        }
        // シーンロードマネージャーの取得
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // シーンの開始
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneStart();
    }

    // シーンロードマネージャーの取得 と シーンのアンロード
    public void SceneLoadCatchUnLoad(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (async == null)
        {
            Debug.Log("このシーンは現在のシーン、もしくはNULLです");
            return;
        }

        // シーンロードマネージャーの取得
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // シーンのアンロード
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneUnload(scene_name);
    }

    // ロード完了時にフラグを建てる
    private void SceneLoadFinish(AsyncOperation obj)
    {
        // ロード完了フラグを建てる
        bLoad = true;
    }

    // デバッグ用
    private void SceneLoad_completed(AsyncOperation obj)
    {
        Debug.Log("finish");
    }




    //～～～～ 全シーンのロード、開始、アンロード ～～～～
    // シーンのロード
    // ※現在のシーンの場合はNULLのままにします
    public void SceneLoadAll()
    {
        // 全てのシーンをロード、ロードフラグをfalseする
        for (int i = 0; i < (int)SCENE_NAME.E_SCENE_MAX; i++)
        {
            // ロード
            asyncAll[i] = SceneManager.LoadSceneAsync(sSceneName[i], LoadSceneMode.Additive);
            // falseにする
            asyncAll[i].allowSceneActivation = false;
        }
    }

    // シーン起動関数
    public void SceneStartAll(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("SceneStart：このシーンは現在のシーン、もしくはNULLです");
            return;
        }
        // 引数に入力されたシーンを起動
        asyncAll[(int)scene_name].allowSceneActivation = true;
    }

    // シーンのアンロード
    public void SceneUnloadAll(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("このシーンは現在のシーン、もしくはNULLです");
            return;
        }
        // 引数に入力されたシーンをアンロード
        asyncAll[(int)scene_name] = SceneManager.UnloadSceneAsync(sSceneName[(int)scene_name]);
        //async[(int)SCENE_NAME.E_PROLOGUE].completed += SceneLoad_completed;
    }

    // シーンロードマネージャーの取得 と シーンの開始
    public void SceneLoadCatchLoadAll(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("このシーンは現在のシーン、もしくはNULLです");
            return;
        }
        // シーンロードマネージャーの取得
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // シーンの開始
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneStartAll(scene_name);
    }

    // シーンロードマネージャーの取得 と シーンのアンロード
    public void SceneLoadCatchUnLoadAll(SCENE_NAME scene_name)
    {
        // シーンのNULLチェック
        if (asyncAll[(int)scene_name] == null)
        {
            Debug.Log("このシーンは現在のシーン、もしくはNULLです");
            return;
        }
        // シーンロードマネージャーの取得
        GameObject SceneLoadManager;
        SceneLoadManager = GameObject.FindWithTag("SceneLoadManager");
        // シーンのアンロード
        SceneLoadManager.GetComponent<SceneLoadStartUnload>().SceneUnloadAll(scene_name);
    }
}
