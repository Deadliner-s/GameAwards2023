using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    //private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        //async = SceneManager.LoadSceneAsync("mobScene");
        //async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("mobScene", LoadSceneMode.Additive);
            //async.allowSceneActivation = true;
        }
    }
}
