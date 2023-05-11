using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneStart : MonoBehaviour
{
    // マネージャーシーンをどのシーンからでも作るようにする
    private static bool Loaded { get; set; }

    private void Awake()
    {
        if (Loaded) { return; }

        Loaded = true;
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }
}
