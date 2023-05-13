using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCursor : MonoBehaviour
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
    [Header("メニュー")]
    private GameObject Menu;            // メニューのオブジェクト

    [Header("ゲームマネージャオブジェクト")]
    GameObject ManagerObj;

    [SerializeField]
    private GameObject fade;

    [SerializeField]
    [Header("Debug用初期ステージ選択")]
    public Stage stage;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Left.performed += context => OnLeft();
        InputActions.UI.Right.performed += context => OnRight();
        InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        // マネージャオブジェクト取得
        ManagerObj = GameObject.Find("GameManager");

        ItemMax = transform.childCount - 1;

        Selected = 0;
        // 初期選択を白に
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color 
            = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.activeSelf == false)
        {
            InputActions.Enable();
        }
    }

    private void OnLeft()
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

    private void OnRight()
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

    private void OnSelect()
    {
        switch (Selected)
        {
            case (0):
                // No

                // タイトルに戻る
                Selected = 0;
                this.gameObject.SetActive(false);
                InputActions.Disable();
                Menu.SetActive(true);
                SoundManager.instance.PlaySE("Decision");
                break;
            case (1):
                // Yes

                InputActions.Disable();
                ManagerObj.GetComponent<GManager>().ReSetData();
                // デバッグ用?
                if (stage == Stage.STAGE_1)
                {
                    //SceneManager.LoadScene("Stage1");
                    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                }
                if (stage == Stage.STAGE_2)
                {
                    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                }
                if (stage == Stage.STAGE_3)
                {
                    fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                }
                SoundManager.instance.PlaySE("Decision");
                //
                break;
        }
    }
}
