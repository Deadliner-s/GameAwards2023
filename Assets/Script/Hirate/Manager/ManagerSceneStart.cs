using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneStart : MonoBehaviour
{
    // �}�l�[�W���[�V�[�����ǂ̃V�[������ł����悤�ɂ���
    private static bool Loaded { get; set; }

    private void Awake()
    {
        if (Loaded) { return; }

        Loaded = true;
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }
}
