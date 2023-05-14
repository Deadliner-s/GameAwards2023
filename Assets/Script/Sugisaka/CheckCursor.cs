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
    [Header("���j���[")]
    private GameObject Menu;            // ���j���[�̃I�u�W�F�N�g

    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
    GameObject ManagerObj;

    [SerializeField]
    private GameObject fade;

    [SerializeField]
    [Header("Debug�p�����X�e�[�W�I��")]
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
        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");

        ItemMax = transform.childCount - 1;

        Selected = 0;
        // �����I���𔒂�
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
        // �O�I��������
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
        // 
        Selected--;
        Selected = (int)Mathf.Repeat(Selected, ItemMax);
        // ���I���𔒂�
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

        SoundManager.instance.PlaySE("Select");
    }

    private void OnRight()
    {
        // �O�I��������
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
        // 
        Selected--;
        Selected = (int)Mathf.Repeat(Selected, ItemMax);
        // ���I���𔒂�
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

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
                this.gameObject.SetActive(false);
                InputActions.Disable();
                Menu.SetActive(true);
                SoundManager.instance.PlaySE("Decision");
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
}
