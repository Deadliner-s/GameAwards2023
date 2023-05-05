using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1to2 : MonoBehaviour
{
    private float Counttime;//時間を測る
    public float TimeLimit;//制限時間
    private Text TimeText;

    private AsyncOperation async;

    [Header("ゲームマネージャオブジェクト")]
    GameObject ManagerObj;

    // Start is called before the first frame update
    void Start()
    {
        async = SceneManager.LoadSceneAsync("Stage2");
        async.allowSceneActivation = false;

        // マネージャオブジェクト取得
        ManagerObj = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//時間を足す

        if (Counttime > TimeLimit || Input.GetKeyDown(KeyCode.P))
        {
            ManagerObj.GetComponent<GManager>().SetClearFlg(1);

            async.allowSceneActivation = true;            
        }
    }
}
