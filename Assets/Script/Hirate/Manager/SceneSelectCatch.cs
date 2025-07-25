using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectCatch : MonoBehaviour
{
    // メニューで選んだものを取得
    public int selectCatch { get; set; }

    public static SceneSelectCatch instance;

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
            return;
        }
    }
}
