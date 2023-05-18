using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetSetSkyBox : MonoBehaviour
{

    private void Awake()
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
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
