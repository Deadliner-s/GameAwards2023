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

    //private Myproject InputActions;

    private int Selected;
    private int ItemMax;
    [SerializeField]
    private GameObject Yes_Text;        // BGMのテキスト
    [SerializeField]
    private GameObject No_Text;

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

    [SerializeField]
    GameObject Window;

    private float Max_Height = 3.777086f;

    bool buttonflag = false;

    // Start is called before the first frame update
    void Start()
    {
        // マネージャオブジェクト取得
        ManagerObj = GameObject.Find("GameManager");
        ItemMax = 2;
        Selected = 0;
        // 初期選択を白に
        Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
        No_Text.GetComponent<TextMeshProUGUI>().color = Color.white;

        buttonflag = false;
    }

    void Update()
    {
        if(InputManager.instance.OnLeft() && buttonflag == false)
        {
            Selected--;
            Selected = (int)Mathf.Repeat(Selected, ItemMax);

            if (Yes_Text.GetComponent<TextMeshProUGUI>().color == Color.black)
            {
                Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                No_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                No_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
            }

            SoundManager.instance.PlaySE("Select");
        }
        if (InputManager.instance.OnRight() && buttonflag == false)
        {
            Selected--;
            Selected = (int)Mathf.Repeat(Selected, ItemMax);

            if (Yes_Text.GetComponent<TextMeshProUGUI>().color == Color.black)
            {
                Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                No_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                No_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            SoundManager.instance.PlaySE("Select");
        }
        if (InputManager.instance.OnBack())
        {
            // No
            // タイトルに戻る
            Selected = 0;
            //InputActions.Disable();
            SoundManager.instance.PlaySE("Decision");
            StartCoroutine("WindowScaleDown");
            buttonflag = true;
        }

        if(InputManager.instance.OnSelect() && buttonflag == false)
        {
            switch (Selected)
            {
                case (0):
                    // No
                    // タイトルに戻る
                    Selected = 0;
                    //InputActions.Disable();
                    SoundManager.instance.PlaySE("Decision");
                    StartCoroutine("WindowScaleDown");
                    break;
                case (1):
                    // Yes
                    //InputActions.Disable();
                    ManagerObj.GetComponent<GManager>().ReSetData();
                    // デバッグ用?
                    switch (stage)
                    {
                        case (Stage.STAGE_1):
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                            fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE);
                            break;
                        case (Stage.STAGE_2):
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                            fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT);
                            break;
                        case (Stage.STAGE_3):
                            //fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage3Event");
                            fade.GetComponent<Fade>().StartCoroutine(
                                "Color_FadeOut_Title",
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT);
                            break;
                        default:
                            break;
                    }
                    SoundManager.instance.PlaySE("Decision");
                    //
                    break;
            }
        }
    }

    private void OnEnable()
    {
        // ウィンドウ表示
        //Vector3 scale = Window.transform.localScale;
        //scale.y = 0;
        //Window.transform.localScale = scale;
        Window.transform.localScale = new Vector3(
            Window.transform.localScale.x,
            0.0f,
            Window.transform.localScale.z
            );
        StartCoroutine("WindowScaleUp");
    }

    IEnumerator WindowScaleUp()
    {
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y += Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator WindowScaleDown()
    {
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y -= Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }

        Selected = 0;
        Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
        No_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
        this.gameObject.SetActive(false);
        Menu.SetActive(true);
        buttonflag = false;
    }
}