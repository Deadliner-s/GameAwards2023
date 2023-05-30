using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3toE : MonoBehaviour
{
    private GameObject sceneMoveManager;
    private GameObject playerManager;
    private GameObject bossManager;
    private float time;

    public float startMoveTime = 2.0f;

    public float startSceneTime = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        bossManager = GameObject.Find("BossManager");
        sceneMoveManager = GameObject.FindGameObjectWithTag("SceneMoveManager");
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager == null)
        {
            playerManager = GameObject.Find("PlayerManager");
        }
        if (bossManager == null)
        {
            bossManager = GameObject.Find("BossManager");
        }
        if (sceneMoveManager == null)
        {
            sceneMoveManager = GameObject.FindGameObjectWithTag("SceneMoveManager");
        }

        if (bossManager.GetComponent<MainBossHp>().BreakFlag == true)
        {
            time += Time.deltaTime;

            if (time >= startMoveTime)
            {
                playerManager.GetComponent<MoveToInitialPosition>().enabled = true;
                if (time > startSceneTime)
                {
                    // シーンの開始
                    sceneMoveManager.GetComponent<SceneMoveManager>().SceneLoad(SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE);
                    sceneMoveManager.GetComponent<SceneMoveManager>().SceneStartUnload();
                }
            }

        }
    }
}
