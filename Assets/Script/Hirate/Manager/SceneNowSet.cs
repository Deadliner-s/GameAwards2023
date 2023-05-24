using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNowSet : MonoBehaviour
{
    // 現在のシーンの設定
    [Header("シーンセット")]
    [Tooltip("現在のシーンをセット")]
    [SerializeField] SceneLoadStartUnload.SCENE_NAME nowSceneSet;

    // Start is called before the first frame update
    void Start()
    {
        SceneNowBefore.instance.sceneNowCatch = nowSceneSet;
    }
}
