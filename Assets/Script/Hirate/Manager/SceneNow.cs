using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNow : MonoBehaviour
{
    public SceneLoadStartUnload.SCENE_NAME sceneNowCatch { get; set; }

    public static SceneNow instance;

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
