using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    public enum Stage
    {
        STAGE_1,
        STAGE_2,
        STAGE_3
    }

    private Myproject InputActions;
    
    private int Selected;
    private int ItemMax;

    [SerializeField]
    private GameObject fade;                // フェードオブジェクト

    [SerializeField]
    [Header("オプションメニュー")]
    private GameObject OptionMenu;
    private bool OptionMenuFlag = false;

    [Header("ゲームマネージャオブジェクト")]
    GameObject ManagerObj;

    [SerializeField]
    [Header("Debug用初期ステージ選択")]
    public Stage stage;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        // マネージャオブジェクト取得
        ManagerObj = GameObject.Find("GameManager");

        // メニュー数(タイトルロゴも入ってるため-1
        ItemMax = transform.childCount - 1;

        OptionMenuFlag = false;
        // 初期カーソル位置設定
        Selected = 0;
        // 初期選択を白に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {        
        // オプション画面が閉じられた場合、再度入力を受け付ける
        if(OptionMenuFlag == true)
        {
            if (OptionMenu.activeSelf == false)
            {
                InputActions.Enable();
                OptionMenuFlag = false;
            }
        }
    }

    private void OnUp()
    {
        // 前選択を黒に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
        // 
        Selected--;
        Selected = (int)Mathf.Repeat(Selected, ItemMax);
        // 現選択を白に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

        SoundManager.instance.PlaySE("Select");
    }

    private void OnDown()
    {
        // 前選択を黒に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
        //
        Selected++;
        Selected = (int)Mathf.Repeat(Selected, ItemMax);
        // 現選択を白に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

        SoundManager.instance.PlaySE("Select");
    }

    private void OnSelect()
    {
        // 選択されたメニューによって処理を変える
        switch (Selected)
        {
            case (0):
                // NEW GAME
                InputActions.Disable();

                ManagerObj.GetComponent<GManager>().ReSetData();

                // デバッグ用?
                if (stage == Stage.STAGE_1)
                {
                    //SceneManager.LoadScene("Stage1");
                    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut");
                }
                if(stage == Stage.STAGE_2)
                {
                    SceneManager.LoadScene("Stage2");
                }
                if (stage == Stage.STAGE_3)
                {
                    SceneManager.LoadScene("merge_2");
                }

                SoundManager.instance.PlaySE("Decision");



                break;
            case (1):
                // CONTINUE
                int num = ManagerObj.GetComponent<GManager>().GetNowStage();
                switch (num)
                {
                    case (0):
                        // ステージ1
                        InputActions.Disable();
                        SceneManager.LoadScene("Stage1");
                        break;
                    case (1):
                        // ステージ2
                        InputActions.Disable();
                        SceneManager.LoadScene("Stage2");
                        break;
                    case (2):
                        // ステージ3
                        InputActions.Disable();
                        SceneManager.LoadScene("merge_2");
                        break;
                }
                SoundManager.instance.PlaySE("Decision");
                break;
            case (2):
                OptionMenu.SetActive(true);
                OptionMenuFlag = true;
                InputActions.Disable();             // オプションメニューが開いているときは入力を受け付けない
                this.gameObject.SetActive(false);
                SoundManager.instance.PlaySE("Decision");
                break;

        }
    }
}
