using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStartManager : MonoBehaviour
{
    // 始めるシーンの設定
    [Header("始めるシーン")]
    [Tooltip("始めるシーン")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME SceneName;

    // Start is called before the first frame update
    void Start()
    {
        // SceneLoadManagerをタグ検索
        GameObject obj = GameObject.FindGameObjectWithTag("SceneLoadManager");
        // シーンのロード
        obj.GetComponent<SceneLoadStartUnload>().SceneLoad(SceneName);
        // シーンの開始
        obj.GetComponent<SceneLoadStartUnload>().SceneStart();
    }
}
