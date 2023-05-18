using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAccessSearch : MonoBehaviour
{
    // シーンロードマネージャーの取得 と シーンのロード
    public static void SceneAccessCatchLoad(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SceneLoadStartUnload sceneLoad = sceneRootObj.GetComponent<SceneLoadStartUnload>();
            if (sceneLoad != null)
            {
                sceneLoad.SceneLoad(scene_name);
                break;
            }
        }
    }

    // シーンロードマネージャーの取得 と シーンの開始
    public static void SceneAccessCatchStart()
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SceneLoadStartUnload sceneLoad = sceneRootObj.GetComponent<SceneLoadStartUnload>();
            if (sceneLoad != null)
            {
                sceneLoad.SceneStart();
                break;
            }
        }
    }

    // シーンロードマネージャーの取得 と シーンのアンロード
    public static void SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SceneLoadStartUnload sceneLoad = sceneRootObj.GetComponent<SceneLoadStartUnload>();
            if (sceneLoad != null)
            {
                sceneLoad.SceneUnload(scene_name);
                break;
                //yield return new WaitForSeconds(0.0f);
            }
        }
    }

    // シーンロードマネージャーの取得 と シーンの開始
    public static void SceneAccessCatchLoadAll(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SceneLoadStartUnload sceneLoad = sceneRootObj.GetComponent<SceneLoadStartUnload>();
            if (sceneLoad != null)
            {
                //sceneLoad.SceneLoad(scene_name);
                sceneLoad.SceneStartAll(scene_name);
                break;
            }
        }
    }

    // シーンロードマネージャーの取得 と シーンのアンロード
    public static void SceneAccessCatchUnloadAll(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SceneLoadStartUnload sceneLoad = sceneRootObj.GetComponent<SceneLoadStartUnload>();
            if (sceneLoad != null)
            {
                sceneLoad.SceneUnloadAll(scene_name);
                break;
                //yield return new WaitForSeconds(0.0f);
            }
        }
    }
}
