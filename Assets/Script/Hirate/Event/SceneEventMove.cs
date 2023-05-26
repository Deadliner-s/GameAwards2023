using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventMove : MonoBehaviour
{
    [SerializeField]
    private GameObject fade; // フェードオブジェクト

    public bool bTextEnd { get; set; } = false; // テキストの終了取得用
    public bool bAniEnd { get; set; } = false; // アニメーションの終了取得用

    // Update is called once per frame
    void Update()
    {
        //if (bTextEnd && bAniEnd)
        if (bAniEnd)
        {
            // SceneMoveManagerをタグ検索
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // シーンの開始
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();
        }
    }
}
