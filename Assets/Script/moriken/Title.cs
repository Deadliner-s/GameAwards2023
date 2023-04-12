using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        // 非同期処理でシーンの遷移実行(現在実行しているシーンのバックグラウンドで次のシーンの読み込みを事前に行う)
       async = SceneManager.LoadSceneAsync("Stage1");

        // シーンを読み込み終わってもシーン遷移は行わない状態にする
       async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        //シーン移動
//      SceneManager.LoadScene("merge_2");
        // 非同期処理でシーンの遷移実行(現在実行しているシーンのバックグラウンドで次のシーンの読み込みを事前に行う)
        async.allowSceneActivation = true;
    }
}
