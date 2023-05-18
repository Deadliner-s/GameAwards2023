using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    
    [SerializeField]
    bool Stage1;
    [SerializeField]
    bool Stage2;
    
    [SerializeField]
    bool FirstTime = true;

    //private AsyncOperation stage1;
    //private AsyncOperation stage2;
    //private AsyncOperation stage3;
    //private bool SceneLoadFlg = false;

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
        LoadData();

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

    void LoadData()
    {
        int stageNum = PlayerPrefs.GetInt("Stage_Num", 4);
        switch (stageNum)
        {
            case (0):
                Stage1 = false;
                Stage2 = false;
                break;
            case (1):
                Stage1 = true;
                Stage2 = false;
                break;
            case (2):
                Stage1 = true;
                Stage2 = true;
                break;
            default:
                Stage1 = false;
                Stage2 = false;
                break;
        }
    }


    public void ReSetData()
    {
        // ゲーム内フラグリセット
        Stage1 = false;
        Stage2 = false;

        // 
        PlayerPrefs.SetInt("Stage_Num", 0);
    }

    // メニューのコンテニューで使用
    public int GetNowStage()
    {
        // 初期化
        int Snum = 0;
        if (FirstTime){
            Snum = 4;
            return Snum;
        }

        // クリアフラグを確認して次のステージ選択
        if (Stage1 != true){
            // ステージ1
            Snum = 0;
        }
        else if (Stage2 != true){
            // ステージ2
            Snum = 1;
        }
        else{
            // ステージ3
            Snum = 2;
        }

        return Snum;
    }

    // ステージクリア時に
    public void SetClearFlg(int num)
    {
        switch (num)
        {
            case (1):
                Stage1 = true;
                Stage2 = false;
                PlayerPrefs.SetInt("Stage_Num", 1);
                break;
            case (2):
                Stage1 = true;
                Stage2 = true;
                PlayerPrefs.SetInt("Stage_Num", 2);
                break;
            default:
                break;
        }
    }

    void OnApplicationQuit()
    {
        // アプリケーション終了時に
        // 現在のクリア状況を保存
        if (Stage1 != true)
        {
            PlayerPrefs.SetInt("Stage_Num", 0);
        }
        else if(Stage2 != true)
        {
            PlayerPrefs.SetInt("Stage_Num", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Stage_Num", 2);
        }
    }

    public bool GetFirstTime()
    {
        if (FirstTime){
            // 初回
            FirstTime = false;
            return true;
        }
        else{
            // 2回目以降
            return false;
        }
    }
}
