using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{
    private Myproject InputActions;
    
    private int Selected;
    private int ItemMax;

    [SerializeField]
    private GameObject fade;                // フェードオブジェクト

    [SerializeField]
    private GameObject title;

    [SerializeField]
    [Header("オプションメニュー")]
    private GameObject OptionMenu;
    private bool OptionMenuFlag = false;

    [SerializeField]
    [Header("データ削除確認")]
    private GameObject CheckMenu;
    private bool CheckMenuFlag = false;

    [Header("ゲームマネージャオブジェクト")]
    GameObject ManagerObj;

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
        ItemMax = transform.childCount; //- 1;  外しました

        OptionMenuFlag = false;
        CheckMenuFlag = false;

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
                title.SetActive(true);
                InputActions.Enable();
                OptionMenuFlag = false;
            }
        }

        // オプション画面が閉じられた場合、再度入力を受け付ける
        if (CheckMenuFlag == true)
        {
            if (CheckMenu.activeSelf == false)
            {
                title.SetActive(true);
                InputActions.Enable();
                CheckMenuFlag = false;
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
                
                //データ削除確認
                title.SetActive(false);
                CheckMenu.SetActive(true);
                CheckMenuFlag = true;
                InputActions.Disable();             // オプションメニューが開いているときは入力を受け付けない
                this.gameObject.SetActive(false);
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
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                        break;
                    case (1):
                        // ステージ2
                        InputActions.Disable();
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                        break;
                    case (2):
                        // ステージ3
                        InputActions.Disable();
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                        break;
                }
                SoundManager.instance.PlaySE("Decision");
                break;

            case (2):
                // OPTION
                title.SetActive(false);
                OptionMenu.SetActive(true);
                OptionMenuFlag = true;
                InputActions.Disable();             // オプションメニューが開いているときは入力を受け付けない
                this.gameObject.SetActive(false);
                SoundManager.instance.PlaySE("Decision");
                break;

            case (3):
                // EXIT
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
                break;
        }
    }
}
