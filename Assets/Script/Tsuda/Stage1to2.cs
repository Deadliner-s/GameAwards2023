using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1to2 : MonoBehaviour
{
    private float Counttime;//ŽžŠÔ‚ð‘ª‚é
    public float TimeLimit;//§ŒÀŽžŠÔ
    private Text TimeText;

    private AsyncOperation async;

    // Start is called before the first frame update
    void Start()
    {
        async = SceneManager.LoadSceneAsync("Stage2");
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        Counttime += Time.deltaTime;//ŽžŠÔ‚ð‘«‚·

        if (Counttime > TimeLimit)
        {
            async.allowSceneActivation = true;            
        }
    }
}
