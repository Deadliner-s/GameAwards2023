using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ボタンが押された場合、今回呼び出される関数
    public void OnClick()
    {
        //シーン移動
        SceneManager.LoadScene("Title");
    }
}
