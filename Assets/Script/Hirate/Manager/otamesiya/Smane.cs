using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Smane : MonoBehaviour
{
    private static bool Loaded { get; set; }

    private void Awake()
    {
        if (Loaded) { return; }

        Loaded = true;
        SceneManager.LoadScene("SLpreMane", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SUncoad();
        }
    }

    public static void SUncoad()
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("SLpreMane");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SLpre sl = sceneRootObj.GetComponent<SLpre>();
            if (sl != null)
            {
                sl.SSta();
            }
        }
    }
}
