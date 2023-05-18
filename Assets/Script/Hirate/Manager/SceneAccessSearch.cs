using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAccessSearch : MonoBehaviour
{
    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̃��[�h
    public static void SceneAccessCatchLoad(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
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

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̊J�n
    public static void SceneAccessCatchStart()
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
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

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̃A�����[�h
    public static void SceneAccessCatchUnload(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
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

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̊J�n
    public static void SceneAccessCatchLoadAll(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
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

    // �V�[�����[�h�}�l�[�W���[�̎擾 �� �V�[���̃A�����[�h
    public static void SceneAccessCatchUnloadAll(SceneLoadStartUnload.SCENE_NAME scene_name)
    {
        // �V�[������
        Scene scene = SceneManager.GetSceneByName("ManagerScene");
        // ���[�g���̃I�u�W�F�N�g������
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
