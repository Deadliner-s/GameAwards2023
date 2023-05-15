using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor_Resu : MonoBehaviour
{
    private Myproject InputActions;
    
    private int Selected;
    private int ItemMax;

    [SerializeField]
    private GameObject fade;                // フェードオブジェクト
   
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
        
        ItemMax = transform.childCount - 1;
        
        // 初期カーソル位置設定
        Selected = 0;
        // 初期選択を白に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {        
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
                // CONTINUE
                int num = ManagerObj.GetComponent<GManager>().GetNowStage();
                InputActions.Disable();
                switch (num)
                {
                    case (0):
                        // ステージ1
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                        break;
                    case (1):
                        // ステージ2
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                        break;
                    case (2):
                        // ステージ3
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                        break;
                }
                SoundManager.instance.PlaySE("Decision");
                break;
            case (1):
                // Return to title
                InputActions.Disable();
                fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Title");
                SoundManager.instance.PlaySE("Decision");
                break;
        }
    }
}
