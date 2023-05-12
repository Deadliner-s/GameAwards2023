using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // 読み込み用
    private AsyncOperation[] async;
    private AsyncOperation asyncStage1;
    private AsyncOperation asyncStage2;
    private AsyncOperation asyncStage3;
    private AsyncOperation asyncPrologue;
    private AsyncOperation asyncStage2Event;
    private AsyncOperation asyncStage3Event;
    private AsyncOperation asyncEpilogue;
    private AsyncOperation asyncResultCompleted;
    private AsyncOperation asyncResultFailed;





    // Start is called before the first frame update
    void Start()
    {
        // ステージ1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
        // ステージ2
        if (asyncStage2 == null)
        {
            asyncStage2 = SceneManager.LoadSceneAsync("Stage2");
            asyncStage2.allowSceneActivation = false;
        }
        // ステージ3
        if (asyncStage3 == null)
        {
            asyncStage3 = SceneManager.LoadSceneAsync("merge_2");
            asyncStage3.allowSceneActivation = false;
        }
        // プロローグ

        if (asyncPrologue == null)
        {
            asyncPrologue = SceneManager.LoadSceneAsync("Prologue");
            asyncPrologue.allowSceneActivation = false;
        }
        // ステージ1と2の間のイベント
        if (asyncStage2Event == null)
        {
            asyncStage2Event = SceneManager.LoadSceneAsync("Stage2Event");
            asyncStage2Event.allowSceneActivation = false;
        }
        // ステージ2と3の間のイベント
        if (asyncStage3Event == null)
        {
            asyncStage3Event = SceneManager.LoadSceneAsync("Stage3Event");
            asyncStage3Event.allowSceneActivation = false;
        }
        // ステージ1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
        // ステージ1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
        // ステージ1
        if (asyncStage1 == null)
        {
            asyncStage1 = SceneManager.LoadSceneAsync("Stage1");
            asyncStage1.allowSceneActivation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //async.allowSceneActivation = true;
    }
}
