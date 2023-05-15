using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // シーンの名前
    public enum SCENE_NAME
    {
        E_STAGE1 = 0,       // ステージ1
        E_STAGE2,           // ステージ2
        E_STAGE3,           // ステージ3
        E_PROLOGUE,         // プロローグ
        E_STAGE2_EVENT,     // ステージ1と2の間のイベント
        E_STAGE3_EVENT,     // ステージ2と3の間のイベント
        E_EPILOGUE,         // エピローグ
        E_RESULT_COMPLETED, // リザルト(俺の勝ち)
        E_RESULT_FAILED,    // リザルト(僕の負け)

        E_SCENE_MAX         // シーンの最大
    }

    // 読み込み用
    private AsyncOperation[] async = new AsyncOperation[(int)SCENE_NAME.E_SCENE_MAX];

    // Start is called before the first frame update
    void Start()
    {
        // ステージ1
        if (async[(int)SCENE_NAME.E_STAGE1] == null)
        {
            async[(int)SCENE_NAME.E_STAGE1] = SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }
        // ステージ2
        if (async[(int)SCENE_NAME.E_STAGE2] == null)
        {
            async[(int)SCENE_NAME.E_STAGE2] = SceneManager.LoadSceneAsync("Stage2", LoadSceneMode.Additive);
        }
        // ステージ3
        if (async[(int)SCENE_NAME.E_STAGE3] == null)
        {
            async[(int)SCENE_NAME.E_STAGE3] = SceneManager.LoadSceneAsync("merge_2", LoadSceneMode.Additive);
        }
        // プロローグ
        if (async[(int)SCENE_NAME.E_PROLOGUE] == null)
        {
            async[(int)SCENE_NAME.E_PROLOGUE] = SceneManager.LoadSceneAsync("Prologue", LoadSceneMode.Additive);
        }
        // ステージ1と2の間のイベント
        if (async[(int)SCENE_NAME.E_STAGE2_EVENT] == null)
        {
            async[(int)SCENE_NAME.E_STAGE2_EVENT] = SceneManager.LoadSceneAsync("Stage2Event", LoadSceneMode.Additive);
        }
        // ステージ2と3の間のイベント
        if (async[(int)SCENE_NAME.E_STAGE3_EVENT] == null)
        {
            async[(int)SCENE_NAME.E_STAGE3_EVENT] = SceneManager.LoadSceneAsync("Stage3Event", LoadSceneMode.Additive);
        }
        // エピローグ
        if (async[(int)SCENE_NAME.E_EPILOGUE] == null)
        {
            async[(int)SCENE_NAME.E_EPILOGUE] = SceneManager.LoadSceneAsync("Epilogue", LoadSceneMode.Additive);
        }
        // リザルト(俺の勝ち)
        if (async[(int)SCENE_NAME.E_RESULT_COMPLETED] == null)
        {
            async[(int)SCENE_NAME.E_RESULT_COMPLETED] = SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }
        // リザルト(僕の負け)
        if (async[(int)SCENE_NAME.E_RESULT_FAILED] == null)
        {
            async[(int)SCENE_NAME.E_RESULT_FAILED] = SceneManager.LoadSceneAsync("Stage1", LoadSceneMode.Additive);
        }

        // 全てのロードフラグをfalseにする
        for (int i = 0; i < (int)SCENE_NAME.E_SCENE_MAX; i++)
        {
            async[i].allowSceneActivation = false;
        }
    }

    // シーン起動関数
    public void SceneStart(SCENE_NAME scene_name)
    {
        // 返り値でダメージを返すための分岐
        switch (scene_name)
        {
            // ステージ1
            case SCENE_NAME.E_STAGE1:
                async[(int)SCENE_NAME.E_STAGE1].allowSceneActivation = true;
                break;
            // ステージ2
            case SCENE_NAME.E_STAGE2:
                async[(int)SCENE_NAME.E_STAGE2].allowSceneActivation = true;
                break;
            // ステージ3
            case SCENE_NAME.E_STAGE3:
                async[(int)SCENE_NAME.E_STAGE3].allowSceneActivation = true;
                break;
            // プロローグ
            case SCENE_NAME.E_PROLOGUE:
                async[(int)SCENE_NAME.E_PROLOGUE].allowSceneActivation = true;
                break;
            // ステージ1と2の間のイベント
            case SCENE_NAME.E_STAGE2_EVENT:
                async[(int)SCENE_NAME.E_STAGE2_EVENT].allowSceneActivation = true;
                break;
            // ステージ2と3の間のイベント
            case SCENE_NAME.E_STAGE3_EVENT:
                async[(int)SCENE_NAME.E_STAGE3_EVENT].allowSceneActivation = true;
                break;
            // エピローグ
            case SCENE_NAME.E_EPILOGUE:
                async[(int)SCENE_NAME.E_EPILOGUE].allowSceneActivation = true;
                break;
            // リザルト(俺の勝ち)
            case SCENE_NAME.E_RESULT_COMPLETED:
                async[(int)SCENE_NAME.E_RESULT_COMPLETED].allowSceneActivation = true;
                break;
            // リザルト(僕の負け)
            case SCENE_NAME.E_RESULT_FAILED:
                async[(int)SCENE_NAME.E_RESULT_FAILED].allowSceneActivation = true;
                break;
        }
    }

    // シーンのアンロード
    public void SceneUnLoad(SCENE_NAME scene_name)
    {
        // 返り値でダメージを返すための分岐
        switch (scene_name)
        {
            // ステージ1
            case SCENE_NAME.E_STAGE1:
                async[(int)SCENE_NAME.E_STAGE1] = SceneManager.UnloadSceneAsync("Stage1");
                break;
            // ステージ2
            case SCENE_NAME.E_STAGE2:
                async[(int)SCENE_NAME.E_STAGE2] = SceneManager.UnloadSceneAsync("Stage2");
                break;
            // ステージ3
            case SCENE_NAME.E_STAGE3:
                async[(int)SCENE_NAME.E_STAGE3] = SceneManager.UnloadSceneAsync("Stage3");
                break;
            // プロローグ
            case SCENE_NAME.E_PROLOGUE:
                async[(int)SCENE_NAME.E_PROLOGUE] = SceneManager.UnloadSceneAsync("Prologue");
                break;
            // ステージ1と2の間のイベント
            case SCENE_NAME.E_STAGE2_EVENT:
                async[(int)SCENE_NAME.E_STAGE2_EVENT] = SceneManager.UnloadSceneAsync("Stage2Event");
                break;
            // ステージ2と3の間のイベント
            case SCENE_NAME.E_STAGE3_EVENT:
                async[(int)SCENE_NAME.E_STAGE3_EVENT] = SceneManager.UnloadSceneAsync("Stage3Event");
                break;
            // エピローグ
            case SCENE_NAME.E_EPILOGUE:
                async[(int)SCENE_NAME.E_EPILOGUE] = SceneManager.UnloadSceneAsync("Epilogue");
                break;
            // リザルト(俺の勝ち)
            case SCENE_NAME.E_RESULT_COMPLETED:
                async[(int)SCENE_NAME.E_RESULT_COMPLETED] = SceneManager.UnloadSceneAsync("Stage1");
                break;
            // リザルト(僕の負け)
            case SCENE_NAME.E_RESULT_FAILED:
                async[(int)SCENE_NAME.E_RESULT_FAILED] = SceneManager.UnloadSceneAsync("Stage1");
                break;
        }
    }
}
