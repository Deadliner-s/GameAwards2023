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
    private GameObject fade;                // �t�F�[�h�I�u�W�F�N�g
   
    [Header("�Q�[���}�l�[�W���I�u�W�F�N�g")]
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
        // �}�l�[�W���I�u�W�F�N�g�擾
        ManagerObj = GameObject.Find("GameManager");
        
        ItemMax = transform.childCount - 1;
        
        // �����J�[�\���ʒu�ݒ�
        Selected = 0;
        // �����I���𔒂�
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void OnUp()
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

    private void OnDown()
    {
        // �O�I��������
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.black;
        //
        Selected++;
        Selected = (int)Mathf.Repeat(Selected, ItemMax);
        // ���I���𔒂�
        transform.GetChild(Selected).GetComponent<TextMeshProUGUI>().color = Color.white;

        SoundManager.instance.PlaySE("Select");
    }

    private void OnSelect()
    {
        // �I�����ꂽ���j���[�ɂ���ď�����ς���
        switch (Selected)
        {
            case (0):
                // CONTINUE
                int num = ManagerObj.GetComponent<GManager>().GetNowStage();
                InputActions.Disable();
                switch (num)
                {
                    case (0):
                        // �X�e�[�W1
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Prologue");
                        break;
                    case (1):
                        // �X�e�[�W2
                        fade.GetComponent<Fade>().StartCoroutine("Color_FadeOut", "Stage2Event");
                        break;
                    case (2):
                        // �X�e�[�W3
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
