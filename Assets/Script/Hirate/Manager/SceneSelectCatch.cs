using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectCatch : MonoBehaviour
{
    // ƒƒjƒ…[‚Å‘I‚ñ‚¾‚à‚Ì‚ğæ“¾
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
