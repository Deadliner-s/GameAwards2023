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
    private GameObject Yes_Text;        // BGM�̃e�L�X�g
    [SerializeField]
    private GameObject No_Text;

    [SerializeField]
    [Header("���j���[")]
    private GameObject Menu;            // ���j���[�̃I�u�W�F�N�g

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    [SerializeField]
    private GameObject fade;

    [SerializeField]    
    [Header("Debug�p�����X�e�[�W�I��")]
    public Stage stage;

    [SerializeField]
    GameObject Window;

    private float Max_Height = 6.560745f;

    // Start is called before the first frame update
    void Start()
    {
        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");

        ItemMax = 2;

        Selected = 0;
        // �����I���𔒂�
        Yes_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
        No_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    private void OnEnable()
    {
        Vector3 scale = Window.transform.localScale;
        scale.y = 0;
        Window.transform.localScale = scale;
        
        StartCoroutine("ScaleUp");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnLeft()
    {
        // 
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

    private void OnRight()
    {
        // 
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

    private void OnSelect()
    {
        switch (Selected)
        {
            case (0):
                // No

                // �^�C�g���ɖ߂�
                Selected = 0;
                InputActions.Disable();
                SoundManager.instance.PlaySE("Decision");

                StartCoroutine("ScaleDown");

                break;
            case (1):
                // Yes

                InputActions.Disable();
                ManagerObj.GetComponent<GManager>().ReSetData();
                // �f�o�b�O�p?
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

    IEnumerator ScaleUp()
    {
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y += 6.560745f * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }

        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Left.performed += context => OnLeft();
        InputActions.UI.Right.performed += context => OnRight();
        InputActions.UI.Select.performed += context => OnSelect();
    }
    IEnumerator ScaleDown()
    {
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y -= 6.560745f * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }

        this.gameObject.SetActive(false);
        Menu.SetActive(true);
    }
}