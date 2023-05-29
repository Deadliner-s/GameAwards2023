using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectCatch : MonoBehaviour
{
    // ���j���[�őI�񂾂��̂��擾
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
