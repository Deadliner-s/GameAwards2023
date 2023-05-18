using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetSetSkyBox : MonoBehaviour
{

    private void Awake()
    {
        // シーン検索
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ルート内のオブジェクトを検索
        foreach (var sceneRootObj in scene.GetRootGameObjects())
        {
            SkyBoxInstance skyBoxInstance = sceneRootObj.GetComponent<SkyBoxInstance>();
            if (skyBoxInstance != null)
            {
                RenderSettings.skybox = skyBoxInstance.GetSky();
            }
        }
    }
}
