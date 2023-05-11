using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneMove : MonoBehaviour
{
    // シーン遷移用
    //private enum Scene
    //{
    //    Scene1 = 1,
    //    Scene2,
    //    Scene3,

    //    SceneMax = 99,
    //}

    // デバッグ用のステージ遷移
    void Update()
    {
        // 通常ステージ
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // ステージ1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("Stage1");
            }
            // ステージ2
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene("Stage2");
            }
            // ステージ3
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene("merge_2");
            }
            // プロローグ
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene("Prologue");
            }
            // ステージ1と2の間のイベント
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneManager.LoadScene("Stage2Event");
            }
            // ステージ2と3の間のイベント
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene("Stage3Event");
            }
        }
    }
}
