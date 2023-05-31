using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMove1to2 : MonoBehaviour
{
    // SceneMoveManagerタグ検索用
    GameObject obj;
    // 開始時のみ起動する用
    private bool bStart = false;

    void Start()
    {
        // テキスト終了時取得用
        obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
    }

    // Update is called once per frame
    void Update()
    {
        if (obj== null)
        {
            // テキスト終了時取得用
            obj = GameObject.FindGameObjectWithTag("NowEventSceneSet");
        }

        if (!bStart && obj.GetComponent<SceneEventMove>().bTextEnd)
        {
            bStart = true;
            // ボスを移動させる
            gameObject.transform.DOMove(new Vector3(0, -5.0f, 5.0f), 9.4f);
        }
    }
}
