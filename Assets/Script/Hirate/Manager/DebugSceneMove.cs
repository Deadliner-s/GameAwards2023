using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneMove : MonoBehaviour
{
    // 分岐用
    private SceneLoadStartUnload.SCENE_NAME sceneName;
    // デバッグ用(ダメージを0にします)
    private bool debug = false;

    private void Start()
    {
        // デバッグ用の設定
        debug = DebugCommandooo.instance.debugSceneSet;
    }

    // デバッグ用のステージ遷移
    private void Update()
    {
        if (!debug) { return; }

        // Pキーが押されたら
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 現在のシーンの取得
            sceneName = SceneNowBefore.instance.sceneNowCatch;

            // シーン遷移
            switch (sceneName)
            {
                // ステージ1
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE1):
                    // シーンの開始
                    gameObject.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT);
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // ステージ2
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE2):
                    // シーンの開始
                    gameObject.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // ステージ3
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE3):
                    // シーンの開始
                    gameObject.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE);
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // プロローグ
                case (SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE):
                    // シーンの開始
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // ステージ1と2の間
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT):
                    // シーンの開始
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
                // ステージ2と3の間
                case (SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT):
                    // シーンの開始
                    gameObject.GetComponent<SceneMoveManager>().SceneStartUnload();
                    break;
            }
        }

        // Lキーが押されたら
        if (!Input.GetKeyDown(KeyCode.L)) { return; }
        // NowEventSceneSetをタグ検索
        GameObject texEnd = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        if (texEnd == null) { return; }
        // テキストを飛ばす
        texEnd.GetComponent<SceneEventMove>().bTextEnd = true;
    }
}
