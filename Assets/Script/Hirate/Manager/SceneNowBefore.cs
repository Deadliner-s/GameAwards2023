using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNowBefore : MonoBehaviour
{
    public SceneLoadStartUnload.SCENE_NAME sceneNowCatch { get; set; }
    public SceneLoadStartUnload.SCENE_NAME sceneBeforeCatch { get; set; }

    public static SceneNowBefore instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
