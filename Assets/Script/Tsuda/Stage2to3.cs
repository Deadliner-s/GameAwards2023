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

    private GameObject playerManager;
    public float startPlayerInitPos = 153.0f;

    public float lastNormalTime = 147.0f;
    private bool startPlayerInitFlg = false;

    //private AsyncOperation async;
    private bool bSceneStart = false;

    [Header("ゲームマネージャオブジェクト")]
    GameObject ManagerObj;

    private GameObject phaseManager;

    // Start is called before the first frame update
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("Stage3Event");
        //async.allowSceneActivation = false;

        // マネージャオブジェクト取得
        ManagerObj = GameObject.Find("GameManager");
        phaseManager = GameObject.Find("PhaseManagerObj");
        playerManager = GameObject.Find("PlayerManager");
        startPlayerInitFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//時間を足す

        if (Counttime > TimeLimit)
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(2);

            if (bSceneStart) { return; }

            // SceneMoveManagerをタグ検索
            GameObject obj = GameObject.FindGameObjectWithTag("SceneMoveManager");
            // シーンの開始
            //async.allowSceneActivation = true;
            obj.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
            obj.GetComponent<SceneMoveManager>().SceneStartUnload();

            bSceneStart = true;
        }

        // プレイヤーが中央に移動
        if (Counttime >= startPlayerInitPos && startPlayerInitFlg == false)
        {
            startPlayerInitFlg = true;
            playerManager.GetComponent<MoveToInitialPosition>().enabled = true;
        }
        if (Counttime >= lastNormalTime)
        {
            phaseManager.GetComponent<PhaseManager>().SetLastNormalFlg(true);
        }
    }
}
