using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public bool Stage1;
    public bool Stage2;

    private AsyncOperation stage1;
    private AsyncOperation stage2;
    private AsyncOperation stage3;
    private bool SceneLoadFlg = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Stage1 = false;
        Stage2 = false;

        //stage1 = SceneManager.LoadSceneAsync("Stage1");
        //stage2 = SceneManager.LoadSceneAsync("Stage2");
        //stage3 = SceneManager.LoadSceneAsync("merge_2");
        //stage1.allowSceneActivation = false;
        //stage2.allowSceneActivation = false;
        //stage3.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {

        // 非同期処理
        //if (SceneManager.GetActiveScene().name == "Title")
        //{
        //    if (SceneLoadFlg == false)
        //    {
        //        stage1 = null;
        //        stage1 = SceneManager.LoadSceneAsync("Stage1");
        //        stage1.allowSceneActivation = false;

        //        stage2 = null;
        //        stage2 = SceneManager.LoadSceneAsync("Stage2");
        //        stage2.allowSceneActivation = false;

        //        stage3 = null;
        //        stage3 = SceneManager.LoadSceneAsync("merge_2");
        //        stage3.allowSceneActivation = false;

        //        SceneLoadFlg = true;
        //    }
        //}

        //if (SceneManager.GetActiveScene().name != "Title")
        //{
        //    SceneLoadFlg = false;
        //}
    }

    public void ReSetData()
    {
        Stage1 = false;
        Stage2 = false;
    }

    // メニューのコンテニューで使用
    public int GetNowStage()
    {
        // 初期化
        int num = 0;

        // クリアフラグを確認して次のステージ選択
        if (Stage1 == false)
        {
            // ステージ1
            num = 0;
        }
        else if (Stage2 == false)
        {
            // ステージ2
            num = 1;
        }
        else
        {
            // ステージ3
            num = 2;
        }

        return num;
    }

    // ステージクリア時に
    public void SetClearFlg(int num)
    {
        switch (num)
        {
            case (1):
                Stage1 = true;
                break;
            case (2):
                Stage2 = true;
                break;
            default:
                break;
        }
    }

    public bool GetStage1ClearFlg()
    {
        return Stage1;
    }
    public bool GetStage2ClearFlg()
    {
        return Stage2;
    }

    public void SceneStage1()
    {
        //stage1.allowSceneActivation = true;
        SceneManager.LoadScene("Stage1");
    }
    public void SceneStage2() 
    {
        //stage2.allowSceneActivation = true;
        SceneManager.LoadScene("Stage2");
    }
    public void SceneStage3() 
    {
        //stage3.allowSceneActivation = true;
        SceneManager.LoadScene("merge_2");
    }
}
