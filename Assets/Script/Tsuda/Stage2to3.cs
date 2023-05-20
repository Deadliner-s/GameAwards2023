using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage2to3 : MonoBehaviour
{
    private float Counttime;//時間を測る
    public float TimeLimit;//制限時間
    private Text TimeText;

    //private AsyncOperation async;
    private bool bSceneStart = false;

    [Header("ゲームマネージャオブジェクト")]
    GameObject ManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("Stage3Event");
        //async.allowSceneActivation = false;

        // マネージャオブジェクト取得
        ManagerObj = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//時間を足す

        if (Counttime > TimeLimit || Input.GetKeyDown(KeyCode.P))
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(2);

            if (bSceneStart) { return; }

            // SceneLoadManagerをタグ検索
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // シーンの開始
            //async.allowSceneActivation = true;
            obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();

            bSceneStart = true;
        }
    }
}
